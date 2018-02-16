using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_and_drop : MonoBehaviour {

	private bool is_inside = false;
	public Transform Hand;
	private bool is_taken = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 HandPos = Hand.transform.position;

		float dist = Vector3.Distance(gameObject.transform.position,HandPos);
		
		if(dist <= 2)
			is_inside = true;
		else
		{
			is_inside = false;
		}
		
		Debug.Log(dist);

		if (Input.GetKeyDown(KeyCode.Space) && is_inside && !is_taken)
		{
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
			transform.parent = Hand;
			transform.position = HandPos;
			is_taken = true;
		}

	}
}
