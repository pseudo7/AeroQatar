using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Rigidbody playerRB;
    public Text velocityText, altitudeText;
    public Transform groundTransform;
    public Image gaugeBG;

    Image rotationGauge;

    void Start()
    {
        rotationGauge = gaugeBG.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        velocityText.text = string.Format("{0} Km/h", (playerRB.velocity.sqrMagnitude * .3f).ToString("0"));
        altitudeText.text = string.Format("{0} Meters", ((int)playerRB.transform.position.y).ToString("0"));
        rotationGauge.transform.rotation = Quaternion.Euler(0, 0, playerRB.rotation.eulerAngles.z);
    }
}
