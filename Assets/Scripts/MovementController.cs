using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{

    [Range(-.5f, .5f)]
    public float xAcc;
    public float forwardSpeed, turnSpeed;
    public Transform pointer, canvasTransform;
    public GameObject player, glider;
    // public RectTransform centralRect, reticle;
    // public Camera barrelCam;
    // public Text speedText, altitude;
    // public Image gazebooster, airBrake;

    Rigidbody playerRB;
    float timer, boostTime = 2f, initialVelocity;
    bool gazedAt, applyBrake;
    //Vector3 mouseNear, mouseFar, barrelFar, barrelNear;
    Vector3 canvasRotation, previousPos, movingDir;
    int movementHex;

    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
        canvasRotation = player.transform.rotation.eulerAngles;
        previousPos = canvasTransform.position;
        StartCoroutine(NormalizeRotation());
    }
    void Update()
    {
        if (gazedAt)
        {
            timer += Time.deltaTime;
            if (timer > boostTime)
            {
                // playerRB.AddRelativeForce((reticle.position - barrelCam.transform.position).normalized * forwardSpeed , ForceMode.VelocityChange);
                timer = 0;
            }
            // gazebooster.fillAmount = timer / boostTime;
        }
        movingDir = player.transform.InverseTransformDirection(playerRB.velocity);
        // speedText.text = movingDir.magnitude.ToString("#");
        //startCount.text = movingDir.ToString();
        // altitude.text = player.transform.position.y.ToString("#");
        //previousPos = canvasTransform.position;
        //Debug.DrawRay(barrelCam.transform.position, pointer.forward - barrelNear, Color.red);
    }

    void FixedUpdate()
    {
        if (applyBrake)
            AirBrake();
        // else
            // playerRB.AddRelativeForce((reticle.position - barrelCam.transform.position).normalized * forwardSpeed, ForceMode.Acceleration);
    }

    void LateUpdate()
    {
        Movement(movementHex);
        playerRB.AddForce(Vector3.right * Input.acceleration.x * forwardSpeed, ForceMode.Acceleration);
      
        Banking(Input.acceleration.x);
        canvasRotation += Vector3.up * Input.acceleration.x;
    }

    IEnumerator NormalizeRotation()
    {
        while (true)
        {
            // glider.transform.rotation = Quaternion.Slerp(glider.transform.rotation, Quaternion.LookRotation(centralRect.position - glider.transform.position, Vector3.up), .1f);
            yield return new WaitForEndOfFrame();
        }
    }

    void Banking(float bankValue)
    {
        glider.transform.Rotate(0, 0, -bankValue * 4);

        if ((Input.acceleration.x < -.4f || Input.acceleration.x > .4f))
            playerRB.AddRelativeForce(-movingDir, ForceMode.Acceleration);

    }

    void AirBrake()
    {
        float mag = movingDir.magnitude;
        // airBrake.fillAmount = 1 - (mag / initialVelocity);
        if (mag <= 0)
            return;
        playerRB.AddRelativeForce(-movingDir, ForceMode.Acceleration);
        if (mag < 10)
        {
            playerRB.isKinematic = true;
            playerRB.isKinematic = false;
        }
    }

    #region hexMovement

    void Movement(int movementHex)
    {
        switch (movementHex)
        {
            case 0x0000:
                //canvasTransform.rotation = Quaternion.LookRotation(centralRect.position - barrelNear);
                break;
            case 0x1000:
                canvasRotation += new Vector3(-1, 0, 0) * turnSpeed;
                playerRB.AddRelativeForce(Vector3.up * forwardSpeed, ForceMode.Acceleration);

                //canvasController.rotation = Quaternion.Euler(-1f, 0, 0);
                print("up");
                break;
            case 0x0100:
                canvasRotation += new Vector3(1, 0, 0) * turnSpeed;
                //canvasController.rotation = Quaternion.Euler(1f, 0, 0);
                print("down");
                playerRB.AddRelativeForce(Vector3.down * forwardSpeed, ForceMode.Acceleration);
                break;
            case 0x0010:
                //canvasController.rotation = Quaternion.Euler(0, 1f, 0);
                canvasRotation += new Vector3(0, 1, 0) * turnSpeed;
                print("right");
                playerRB.AddRelativeForce(Vector3.right * forwardSpeed, ForceMode.Acceleration);
                break;
            case 0x0001:
                canvasRotation += new Vector3(0, -1, 0) * turnSpeed;
                //canvasController.rotation = Quaternion.Euler(0, -1f, 0);
                print("left");
                playerRB.AddRelativeForce(Vector3.left * forwardSpeed, ForceMode.Acceleration);
                break;
        }
        // Mathf.Clamp(canvasRotation.y, -60, 60)
        canvasRotation = new Vector3(Mathf.Clamp(canvasRotation.x, -60, 60), canvasRotation.y, 0);
        canvasTransform.rotation = Quaternion.Slerp(canvasTransform.rotation, Quaternion.Euler(canvasRotation), .1f);
    }

    #endregion

    #region movement value setters
    public void UpEnter()
    {
        movementHex = 0x1000;
    }
    public void DownEnter()
    {
        movementHex = 0x0100;
    }
    public void RightEnter()
    {
        movementHex = 0x0010;
    }
    public void LeftEnter()
    {
        movementHex = 0x0001;
    }
    public void PointerExit()
    {
        movementHex = 0x0000;
    }
    public void GazeBoostEnter()
    {
        gazedAt = true;
    }
    public void GazeBoostExit()
    {
        gazedAt = false;
    }
    public void AirBrakeEnter()
    {
        applyBrake = true;
        initialVelocity = movingDir.magnitude;
    }
    public void AirBrakeExit()
    {
        applyBrake = false;
        initialVelocity = 0;
        // airBrake.fillAmount = 0;
    }
    #endregion
}
