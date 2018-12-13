using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public float changeFrequency = 10;
    public Material[] skyboxes;

    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(ChangeSkyBox());
    }

    IEnumerator ChangeSkyBox()
    {
        while (gameObject.activeInHierarchy)
        {
            mainCamera.GetComponent<Skybox>().material = skyboxes[Random.Range(0, skyboxes.Length)];
            yield return new WaitForSeconds(changeFrequency);
        }
    }

}
