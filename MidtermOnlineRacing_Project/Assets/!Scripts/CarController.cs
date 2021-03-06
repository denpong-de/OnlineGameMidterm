using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private Vector2 moveVec;
    private float currentSteerAngle;
    public static bool isBreaking;
    private float currentBreakForce;

    public static bool isMoving;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;
    [SerializeField] private WheelCollider rearRightCollider;

    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearLeftTransform;
    [SerializeField] private Transform rearRightTransform;

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleMotor()
    {
        rearLeftCollider.motorTorque = moveVec.y * motorForce;
        rearRightCollider.motorTorque = moveVec.y * motorForce;
        ApplyBreaking();
        if (isBreaking)
        {
            currentBreakForce = breakForce;

            WheelFrictionCurve lfFriction = rearLeftCollider.forwardFriction;
            lfFriction.stiffness = 0.5f;
            rearLeftCollider.forwardFriction = lfFriction;
            WheelFrictionCurve lsFriction = rearLeftCollider.sidewaysFriction;
            lsFriction.stiffness = 0.5f;
            rearLeftCollider.sidewaysFriction = lsFriction;

            WheelFrictionCurve rfFriction = rearRightCollider.forwardFriction;
            rfFriction.stiffness = 0.5f;
            rearRightCollider.forwardFriction = rfFriction;
            WheelFrictionCurve rsFriction = rearRightCollider.sidewaysFriction;
            rsFriction.stiffness = 0.5f;
            rearRightCollider.sidewaysFriction = rsFriction;
        }
        else
        {
            currentBreakForce = 0;

            WheelFrictionCurve lfFriction = rearLeftCollider.forwardFriction;
            lfFriction.stiffness = 1f;
            rearLeftCollider.forwardFriction = lfFriction;
            WheelFrictionCurve lsFriction = rearLeftCollider.sidewaysFriction;
            lsFriction.stiffness = 1f;
            rearLeftCollider.sidewaysFriction = lsFriction;

            WheelFrictionCurve rfFriction = rearRightCollider.forwardFriction;
            rfFriction.stiffness = 1f;
            rearRightCollider.forwardFriction = rfFriction;
            WheelFrictionCurve rsFriction = rearRightCollider.sidewaysFriction;
            rsFriction.stiffness = 1f;
            rearRightCollider.sidewaysFriction = rsFriction;
        }
    }

    private void ApplyBreaking()
    {
        frontLeftCollider.brakeTorque = currentBreakForce;
        frontRightCollider.brakeTorque = currentBreakForce;
        rearLeftCollider.brakeTorque = currentBreakForce;
        rearRightCollider.brakeTorque = currentBreakForce;
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

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * moveVec.x;
        frontLeftCollider.steerAngle = currentSteerAngle;
        frontRightCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftTransform);
        UpdateSingleWheel(frontRightCollider, frontRightTransform);
        UpdateSingleWheel(rearLeftCollider, rearLeftTransform);
        UpdateSingleWheel(rearRightCollider, rearRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
