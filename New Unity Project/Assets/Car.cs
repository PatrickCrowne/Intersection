using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	
	public GameObject bumper;
	public Rigidbody rb;
	
	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public bool frontWheelDrive;
	
	public WheelCollider wheelRR;
	public WheelCollider wheelRL;
	public bool rearWheelDrive;
	
	public float acceleration = 100.0f;
	public float deceleration = 100.0f;
	
	public float speed = 0;
	public float brake = 0;
	public float maxVelocity = 10;
	
	private static int ACCELERATING = 0;
	private static int BRAKING = 1;
	
	private int status = ACCELERATING;
	
    // Start is called before the first frame update
    void Start()
    {

		
    }

	void determineStatus()
	{
	
		int layerMask = 1 << 8;
        layerMask = ~layerMask;
	
		RaycastHit hit;
		if (Physics.Raycast(bumper.transform.position,
		transform.TransformDirection(Vector3.forward), out hit, rb.velocity.magnitude*3.0f + 3.0f, layerMask))
		{
			
			
			status = BRAKING;
			
		}
		else
		{
			
			status = ACCELERATING;
			
		}
		
	}

    // Update is called once per frame
    void Update()
    {
        
		determineStatus();
		
		if (status == ACCELERATING)
		{
			if (rb.velocity.magnitude < maxVelocity)
			{
				speed = acceleration;
				brake = 0;
			}
			else
			{
				speed = 0;
				brake = 0;
			}
			
		}
		
		if (status == BRAKING)
		{
			
			if (rb.velocity.magnitude > 0)
			{
				speed = 0;
				brake = deceleration;
			}
			else
			{
				speed = 0;
				brake = deceleration;
			}
			
		}
		
		if (frontWheelDrive) {
			wheelFR.motorTorque = speed;
			wheelFL.motorTorque = speed;
		}
		
		if (rearWheelDrive) {
			wheelRR.motorTorque = speed;
			wheelRL.motorTorque = speed;
		}
		
		wheelFR.brakeTorque = brake;
		wheelFL.brakeTorque = brake;
		wheelRR.brakeTorque = brake;
		wheelRL.brakeTorque = brake;
		
		
    }
}
