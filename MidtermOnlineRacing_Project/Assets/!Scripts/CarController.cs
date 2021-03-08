using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private Vector2 moveVec;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;

    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;
    [SerializeField] private WheelCollider rearRightCollider;

    private void Awake()
    {
        
    }

    void FixedUpdate()
    {
        //HandleMotor();
        //if (Keyboard.current.spaceKey.isPressed)
        //{
        //    //ApplyBreaking();
        //    Debug.Log("isBereaking");
        //}
        Debug.Log(isBreaking);
    }

    private void HandleMotor()
    {
        frontLeftCollider.motorTorque = moveVec.y * motorForce;
        frontRightCollider.motorTorque = moveVec.y * motorForce;

    }

    private void ApplyBreaking()
    {
        frontLeftCollider.brakeTorque = breakForce;
        frontRightCollider.brakeTorque = breakForce;
        rearLeftCollider.brakeTorque = breakForce;
        rearRightCollider.brakeTorque = breakForce;
    }

    float breakButtonValue;
    public void OnBreak(InputValue value)
    {
        breakButtonValue = value.Get<float>();

        if(breakButtonValue == 1)
        {
            isBreaking = true;
        }
        else
        {
            isBreaking = false;
        }
    }

    public void OnMove(InputValue value)
    {
        moveVec = value.Get<Vector2>();
    }
}
