using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingByXYZ : MonoBehaviour {
	public float xAxis = 0;
	public float yAxis = 0;
	public float zAxis = 0;
	public float angle = 0;
	public float rotationSpeed = 3;
	public bool doIt = false;

	private bool isAlligned = true;
	private Quaternion desiredRotation;
	private float angleHandle;
	private Vector3 mainAxis;
	// Use this for initialization
	void Start () {
		//doIt = false;
		isAlligned = true;
		angleHandle = angle;
		mainAxis = new Vector3 (xAxis, yAxis, zAxis).normalized;
		desiredRotation = Quaternion.AngleAxis (angleHandle, mainAxis);
	}
	
	// Update is called once per frame
	void Update () {
		if (doIt) {
			desiredRotation = Quaternion.AngleAxis (angleHandle, mainAxis);
			isAlligned = false;
			doIt = false;
		}
		if (!isAlligned) {
			//transform.rotation = Quaternion.Slerp (transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
			transform.rotation = Quaternion.RotateTowards(transform.rotation,desiredRotation,rotationSpeed*Time.deltaTime*50);
			if (Quaternion.Angle(transform.rotation,desiredRotation) <= 1)// when it is close enough to be alligned(1 degree), it jumps to the exact rotation(for precision)
			{
				transform.rotation = desiredRotation;
				isAlligned = true;// object is alligned now
				angleHandle += angle;
			}
		}
	}
}
