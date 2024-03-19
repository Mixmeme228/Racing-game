using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Physics;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float EngineTorque = 600.0f;
    public float MaxEngineRPM = 1000.0f;
    public float MinEngineRPM = 500.0f;
    private float EngineRPM = 0.0f;
    public float[] GearRatio;
    private int CurrentGear = 0;
    private int AppropriateGear;
    private float Offmotor=1;
    [SerializeField]
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    public float slipThreshold = 0.2f;
    public float maxTorque = 500f;
    public float maxSteerAngle = 45f;
    public float driftFactor = 0.95f;
    private float currentAngle;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;
    public float maxSpeedForMaxSteering = 30f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().maxAngularVelocity = 10000;
        GetInput();
        HandleMotor();
        GearMach();
        Drive();
        HandleSteering();
        UpdateWheels();
    }

    private void Drive()
    {
        //крутящий момент передних колес 
        frontLeftWheelCollider.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
        frontRightWheelCollider.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
        WheelHit hit;
        rearLeftWheelCollider.GetGroundHit(out hit);

        float slip = Mathf.Abs(hit.sidewaysSlip);
        if (10 * Input.GetAxis("Horizontal") <= 7f&& slip < slipThreshold)
        {
            //крутящий момент задних колес вне время дрифта
            rearLeftWheelCollider.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
            rearRightWheelCollider.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
            //боковое сопротивление задних колес вне время дрифта
            WheelFrictionCurve myWfc;
            myWfc = rearLeftWheelCollider.sidewaysFriction;
            myWfc.extremumSlip = 0.2f;
            rearLeftWheelCollider.sidewaysFriction = myWfc;
            rearRightWheelCollider.sidewaysFriction = myWfc;
        }
        else
        {
            //крутящий момент задних колес во время дрифта
            rearLeftWheelCollider.motorTorque = 0;
            rearRightWheelCollider.motorTorque = 0;
            //боковое сопротивление задних колес во время дрифта
            WheelFrictionCurve myWfc;
            myWfc = rearLeftWheelCollider.sidewaysFriction;
            myWfc.extremumSlip = 0.5f;
            rearLeftWheelCollider.sidewaysFriction = myWfc;
            rearRightWheelCollider.sidewaysFriction = myWfc;
        }

    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void GearMach()
    {
        GetComponent<Rigidbody>().drag = GetComponent<Rigidbody>().velocity.magnitude / 250;

        // Compute the engine RPM based on the average RPM of the two wheels, then call the shift gear function.
        EngineRPM = (frontLeftWheelCollider.rpm + frontRightWheelCollider.rpm) / 2 * GearRatio[CurrentGear];
        ShiftGears();

        // Set the audio pitch to the percentage of RPM to the maximum RPM plus one.
        float pitch = Mathf.Abs(EngineRPM / MaxEngineRPM) + 1.0f;
        // Ensure that the pitch does not reach a value higher than is desired.
        pitch = Mathf.Clamp(pitch, 1.0f, 2.0f);
        //GetComponent<AudioSource>().pitch = pitch;

        // Apply the values to the wheels. The torque applied is divided by the current gear and
        // multiplied by the user input variable.
        frontLeftWheelCollider.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
        frontRightWheelCollider.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");

        // The steer angle is an arbitrary value multiplied by the user input.
        frontLeftWheelCollider.steerAngle = 10 * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = 10 * Input.GetAxis("Horizontal");
        Debug.Log((CurrentGear+1)+";"+ EngineRPM);
        
    }
    void MyFunction()
    {
        CurrentGear= AppropriateGear;
        Offmotor = 1;
    }
    private void ShiftGears()
    {
        // This function shifts the gears of the vehicle. It loops through all the gears, checking which will make
        // the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
        if (EngineRPM >= MaxEngineRPM)
        {
             AppropriateGear = CurrentGear;

            for (int i = 0; i < GearRatio.Length; i++)
            {
                if (frontLeftWheelCollider.rpm * GearRatio[i] < MaxEngineRPM)
                {
                    AppropriateGear = i;
                    break;
                }
            }
            Offmotor = 1f* AppropriateGear/ GearRatio.Length;
            Invoke("MyFunction", 0.75f);
        }

        if (EngineRPM <= MinEngineRPM)
        {
             AppropriateGear = CurrentGear;

            for (int j = GearRatio.Length - 1; j >= 0; j--)
            {
                if (frontLeftWheelCollider.rpm * GearRatio[j] > MinEngineRPM)
                {
                    AppropriateGear = j;
                    break;
                }
            }

            CurrentGear = AppropriateGear;
            Offmotor = 1;
        } 
    }
    private void HandleSteering()
    {
        float currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        float currentMaxSteering = Mathf.Lerp(1f, maxSteeringAngle, maxSpeedForMaxSteering/currentSpeed);
        steerAngle = currentMaxSteering * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce*Offmotor;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce*Offmotor;

        brakeForce = isBreaking ? 6000f : 0f;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }
}
