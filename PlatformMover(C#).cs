using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {
	private Vector3 inPos;//initial position of the platform
	public Vector3 offset = new Vector3(100,100,100);//platform gonna move to inpos + offset
	private bool pozDir = true;//pozitive direction
	public float speed = 5;
	public float maxDist;
	private Vector3 target;
	private Vector3 curPos;
	private Vector3 playerOff;
	//private bool isColliding = false;
	private GameObject player = null;
	private Vector3 lastPos; // position of platform from last frame;
	public Vector3 instantSpeed;// the speed the player gets when jumps off platform
	public float iSpAmplifier = 2000;// the amplifier of the speed above
	// Use this for initialization
	void Start () {
		inPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		curPos = transform.position;
		Vector3 relOffset = transform.rotation * offset;
		if (Vector3.Distance(transform.position,target) < 0.1f) {
			pozDir = !pozDir;
		}
		if (pozDir) {
			target = inPos + relOffset;
			//maxDist = speed * Vector3.Distance (curPos, target);
		} else {
			target = inPos;
			//maxDist = speed * Vector3.Distance (curPos, target);
		}
		maxDist = speed / (Vector3.Distance(inPos + relOffset/2,curPos)+5);
		transform.position = Vector3.MoveTowards (curPos, target, maxDist);
		if (player) {
			player.transform.position += transform.position - lastPos;
			instantSpeed =(transform.position - lastPos);
			lastPos = transform.position;
		}
		/*DEPRECATED - kept for reference
		if (isColliding) {
			//playerOff = transform.position - curPos;
			//player.transform.position += playerOff;
			isColliding = false;
			player.transform.SetParent (transform);
		}
		*/
	}

	private void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.CompareTag ("Player")) {// we detect when the player touches the platform so we can store his last location
			player = coll.gameObject;
			lastPos = transform.position;
		}
	}
	private void OnCollisionExit(Collision coll)
	{
		if (coll.gameObject.CompareTag ("Player")) {
			player.GetComponent<Rigidbody> ().AddForce (instantSpeed*iSpAmplifier);// player gets speed when jumps off platform
			player = null;// no player is touching the platform now
		}
	}

		/*
	private void OnCollisionStay(Collision coll)
	{
		if(coll.gameObject.CompareTag("Player"))
		{
			//Vector3 off = coll.transform.position - transform.position;
			//coll.transform.position = Vector3.MoveTowards (coll.transform.position,target + off,speed);
			isColliding = true;
		}
	}
	*/
}
