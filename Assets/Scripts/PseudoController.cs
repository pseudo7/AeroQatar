using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PseudoController : MonoBehaviour
{
    public float delay = 1f;

    Scrollbar liftScrollbar;
    Coroutine coroutine;
    bool coroutineEnded = true;
    void Start()
    {
        liftScrollbar = GetComponent<Scrollbar>();
    }


    public void OnPointerUp()
    {
        if (coroutineEnded)
        {
            coroutine = StartCoroutine(Normalize());
            coroutineEnded = false;
        }
        else
        {
            StopCoroutine(coroutine);
            coroutineEnded = true;
            coroutine = StartCoroutine(Normalize());
        }
    }

    IEnumerator Normalize()
    {
        yield return new WaitForSeconds(delay);
        if (liftScrollbar.value > .5f)
            while ((liftScrollbar.value -= Time.deltaTime) > .5f)
                yield return new WaitForEndOfFrame();
        else
            while ((liftScrollbar.value += Time.deltaTime) < .5f)
                yield return new WaitForEndOfFrame();
        liftScrollbar.value = .5f;
        coroutineEnded = true;
    }
}
