using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private HingeJoint2D hingeJoint2D;
    [SerializeField]
    private JointMotor2D jointMotor;

    [SerializeField]
    private KeyCode keyCode;

    // Start is called before the first frame update
    void Start()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
        jointMotor = hingeJoint2D.motor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(keyCode))
        {
            // set motor speed to max
            jointMotor.motorSpeed = -speed;
            hingeJoint2D.motor = jointMotor;
            Debug.Log("AAAA");
        }
        else
        {
            // snap the motor back again
            jointMotor.motorSpeed = speed;
            hingeJoint2D.motor = jointMotor;
        }
    }
}
