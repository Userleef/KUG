using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
	
	private Transform t;
	public float SpeedM;
	private bool is_inside;
	private bool is_taken;
	private bool is_food;
	private bool is_table;
	private GameObject Food;
	private GameObject Table;
	public Transform Hand;
	public Transform tom;
	// Use this for initialization
	void Start ()
	{
		t = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame

	public static void Movment(int angle, Vector3 v1, Vector3 v2, ref Vector3 dir, Transform transform)
	{
		dir = v1 + v2;
		transform.rotation = Quaternion.Euler(0, angle,0);
	}

	private void OnTriggerStay(Collider Col)
	{
		if (Col.gameObject.tag == "Placard")
			is_inside = true;
		else
			is_inside = false;
		
		if (Col.gameObject.tag == "Food")
		{
			is_food = true;
			Food = Col.gameObject;
		}
		else
			is_food = false;

		if (Col.gameObject.tag == "Table")
		{
			is_table = true;
			Table = Col.gameObject;
		}
		else
			is_table = false;
	}

	
	void Update ()
	{
		Vector3 dir = Vector3.zero;
		if(Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.S))
			Movment(315, Vector3.left, Vector3.back, ref dir, transform);
		else if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.Z))
			Movment(45, Vector3.left, Vector3.forward, ref dir, transform);
		else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Z))
			Movment(135, Vector3.forward, Vector3.right, ref dir, transform);
		else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
			Movment(225, Vector3.right, Vector3.back, ref dir, transform);
		else if (Input.GetKey(KeyCode.Q))
			Movment(0, Vector3.left, Vector3.zero, ref dir, transform);
		else if (Input.GetKey(KeyCode.Z))
			Movment(90, Vector3.forward, Vector3.zero, ref dir, transform);
		else if (Input.GetKey(KeyCode.D))
			Movment(180, Vector3.right, Vector3.zero, ref dir, transform);
		else if (Input.GetKey(KeyCode.S))
			Movment(270, Vector3.zero, Vector3.back, ref dir, transform);
		t.position += dir * Time.deltaTime * SpeedM;
		
		if (Input.GetKeyDown(KeyCode.Space) && is_inside && !is_taken)
		{
			Transform Tom = Instantiate(tom, transform.position, Quaternion.identity);
			Tom.GetComponent<Rigidbody>().isKinematic = true;
			Tom.GetComponent<Collider>().enabled = !tom.GetComponent<Collider>().enabled;
			Tom.parent = t;
			Tom.position = Hand.position + Vector3.down * 0.9f;
			is_taken = true;
		}

		if (Input.GetKeyDown(KeyCode.Space) && !is_taken && is_food)
		{
			Food.GetComponent<Rigidbody>().isKinematic = true;
			Food.GetComponent<Collider>().enabled = !Food.GetComponent<Collider>().enabled;
			Food.transform.parent = t;
			Food.transform.position = Hand.position + Vector3.down * 0.9f;
			is_taken = true;
		}

		if (is_taken && Input.GetKeyDown(KeyCode.Space))
		{
			Food.GetComponent<Rigidbody>().isKinematic = false;
			Food.transform.parent = null;
			is_taken = false;
		}

		if (is_table && is_taken && Input.GetKeyDown(KeyCode.Space))
		{
			Food.transform.parent = Table.transform;
			is_taken = false;
		}
	}
}
