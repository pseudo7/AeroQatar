using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public Text degValueText;
    public Transform playerTransform;
    public Transform compassRingTransform;

    void Update()
    {
        float deg = playerTransform.rotation.eulerAngles.y;
        compassRingTransform.rotation = Quaternion.Euler(0, 0, deg);
        degValueText.text = (deg % 90).ToString("0");
    }
}
