using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float smooth = .4f, senstivity = 6;
    // public Text accText;
    Rigidbody playerRB;
    Vector3 currentPosition, lastPosition, movingDir;
    float rotX, rotY, rotZ;
    bool justStart;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        lastPosition = currentPosition = transform.position;
    }

    void Update()
    {
        movingDir = transform.InverseTransformDirection(transform.position - lastPosition);
        // accText.text = movingDir.sqrMagnitude.ToString();
        // ApplyBrake();
        // if (CrossPlatformInputManager.GetButton("BRAKE"))
        //     Debug.Log("Fire");
        if (CrossPlatformInputManager.GetAxisRaw("Brake") == 1)
            ApplyBrake();

        // if ((Input.acceleration.x < -.5f || Input.acceleration.x > .5f))
        //     ApplyBrake();
        // Vector3 rot = transform.rotation.eulerAngles;
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rot.y, rot.z), .1f);
        // transform.Rotate(0, 0, -CrossPlatformInputManager.GetAxis("Mouse X") * .1f);
        playerRB.AddForce(transform.forward * 10 * CrossPlatformInputManager.GetAxis("Vertical"), ForceMode.Acceleration);
        // playerRB.AddTorque(-transform.forward * CrossPlatformInputManager.GetAxis("Horizontal") / 30f, ForceMode.Impulse);

        // Debug.Log(Input.acceleration);
        // playerRB.AddTorque(-transform.forward * Input.acceleration.x, ForceMode.Acceleration);
        if (transform.position.y > 5)
            transform.Rotate(0, 0, Input.acceleration.x / -2);

        // playerRB.AddForce(-transform.up * 2 * CrossPlatformInputManager.GetAxis("Lift_Right"), ForceMode.Acceleration);
        // transform.Rotate(CrossPlatformInputManager.GetAxis("Lift_Right"), 0, 0);
        if (playerRB.velocity.sqrMagnitude > 250)
        {
            playerRB.AddTorque(transform.right * CrossPlatformInputManager.GetAxis("Lift_Right") / 30, ForceMode.Acceleration);
            playerRB.AddForce(-transform.up * 5 * CrossPlatformInputManager.GetAxis("Lift_Left"), ForceMode.Acceleration);
        }
        // playerRB.AddTorque(transform.right * CrossPlatformInputManager.GetAxis("Lift_Left") / 30, ForceMode.Acceleration);
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name.StartsWith("G"))
    //    {
    //        playerRB.constraints = RigidbodyConstraints.None;
    //        CrossPlatformInputManager.SetAxisZero("Vertical");
    //    }
    //}

    void OnCollisionExit(Collision collision)
    {
        //if (/*collision.gameObject.name.StartsWith("G") && */transform.position.y > 5)
            playerRB.constraints = RigidbodyConstraints.None;
    }

    void Banking(float bankValue)
    {
        transform.Rotate(0, 0, -bankValue * 4);

        if ((Input.acceleration.x < -.4f || Input.acceleration.x > .4f))
            ApplyBrake();
    }

    public void ApplyBrake()
    {
        Debug.Log("Break Applied");
        playerRB.AddRelativeForce(-movingDir * 10, ForceMode.VelocityChange);
        playerRB.AddTorque(-playerRB.angularVelocity * 10, ForceMode.Acceleration);
    }

    public void Throttle()
    {
        Debug.Log("Full Throttle");
        playerRB.AddForce(transform.forward * 50, ForceMode.Acceleration);
    }
}
