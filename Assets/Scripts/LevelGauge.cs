using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGauge : MonoBehaviour
{
    public Transform playerTransform;
    public Transform levelGaugeTransform;

    void Update()
    {
        levelGaugeTransform.rotation = Quaternion.Euler(0, 0, playerTransform.rotation.eulerAngles.x);
    }
}
