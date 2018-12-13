using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    public float blinkFrequency = 1f;

    Transform[] childrenTransform;
    int childrenCount;
    int currentLightIndex = 0;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        childrenCount = transform.childCount;
        childrenTransform = new Transform[childrenCount];
        for (int i = 0; i < childrenCount; i++)
            childrenTransform[i] = transform.GetChild(i);
        StartCoroutine(LightCoroutine());
    }

    IEnumerator LightCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < 2; i++)
                childrenTransform[2 * currentLightIndex + i].GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(blinkFrequency);
            for (int i = 0; i < 2; i++)
                childrenTransform[2 * currentLightIndex + i].GetChild(0).gameObject.SetActive(false);
            currentLightIndex++;
            currentLightIndex %= (childrenCount / 2);
        }
    }

}
