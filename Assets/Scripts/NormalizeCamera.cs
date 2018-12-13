using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizeCamera : MonoBehaviour
{
    public float delay = 3f;
    public Transform lookTransform, mainCameraTransform;
    public Vector3 offset;

    float countdown;
    bool startNormalization;

    void Update()
    {
        if (countdown > delay)
        {
            countdown = 0;
            startNormalization = true;
            Invoke("StopNormalization", 3);
        }
        if (!startNormalization)
            countdown += Time.deltaTime;
        // transform.position = lookTransform.position + offset;
    }

    void LateUpdate()
    {
        if (startNormalization)
            Normalize();
    }

    void StopNormalization()
    {
        startNormalization = false;
    }
    void Normalize()
    {
        Vector3 currentRotation = mainCameraTransform.rotation.eulerAngles;
        mainCameraTransform.rotation = Quaternion.Slerp(mainCameraTransform.rotation, Quaternion.LookRotation(lookTransform.forward + Vector3.down / 3), .3f);
    }
}
