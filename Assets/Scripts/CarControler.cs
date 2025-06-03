using UnityEngine;

public class CarControler : MonoBehaviour
{
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    public float motorForce = 1500f;
    public float maxSteerAngle = 30f;

    void FixedUpdate()
    {
        float motor = Input.GetAxis("Vertical") * motorForce;
        float steer = Input.GetAxis("Horizontal") * maxSteerAngle;

        // Direção
        frontLeft.steerAngle = steer;
        frontRight.steerAngle = steer;

        // Tração
        rearLeft.motorTorque = motor;
        rearRight.motorTorque = motor;

        UpdateWheels();

        if (Input.GetKey(KeyCode.Space))
        {
            rearLeft.brakeTorque = 3000f;
            rearRight.brakeTorque = 3000f;
        }
        else
        {
            rearLeft.brakeTorque = 0f;
            rearRight.brakeTorque = 0f;
        }

    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontLeft, frontLeftTransform);
        UpdateSingleWheel(frontRight, frontRightTransform);
        UpdateSingleWheel(rearLeft, rearLeftTransform);
        UpdateSingleWheel(rearRight, rearRightTransform);
    }

    void UpdateSingleWheel(WheelCollider collider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        trans.position = pos;
        trans.rotation = rot;
    }

}
