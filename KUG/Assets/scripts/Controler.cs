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
		{
			is_inside = true;
			Food = Col.gameObject.GetComponent<Spawn_aliment>().Food_inside;
		}


		if (Col.gameObject.tag == "Food")
		{
			is_food = true;
			Food = Col.gameObject;
		}

		else if (Col.gameObject.tag == "Table")
		{
			is_table = true;
			Table = Col.gameObject;
		}
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
		
		//Prendre un objet d'un placard
		if (Input.GetKeyDown(KeyCode.Space) && is_inside && !is_taken)
		{
			Debug.Log("Prendre objet placard");
			Transform Tom = Instantiate(Food.transform, transform.position, Quaternion.identity);
			Tom.GetComponent<Rigidbody>().isKinematic = true;
			Tom.GetComponent<Collider>().enabled = false;
			Tom.parent = t;
			Tom.position = Hand.position + Vector3.down * 0.9f;
			Tom.rotation = t.transform.rotation;
			is_taken = true;
		}

		//Poser un objet sur un plan de travail
		else if (is_table && is_taken && Input.GetKeyDown(KeyCode.Space) && !Table.GetComponent<Is_food_on>().have_food)
		{
			Debug.Log("Poser objet plan de travail");
			Food.transform.parent = Table.transform;
			Food.transform.position = Table.transform.position + Vector3.up * 1.2f;
			Table.GetComponent<Is_food_on>().have_food = true;
			Table.GetComponent<Is_food_on>().food_on = Food;
			is_taken = false;
		}
		
		
		//Prendre un objet sur un plan de travail
		else if (is_table && !is_taken && Input.GetKeyDown(KeyCode.Space) && Table.GetComponent<Is_food_on>().have_food)
		{
			Debug.Log("Prendre objet plan de travail");
			Food = Table.GetComponent<Is_food_on>().food_on;
			Food.transform.parent = t;
			Food.transform.position = Hand.position + Vector3.down * 0.9f;
			Table.GetComponent<Is_food_on>().have_food = false;
			Table.GetComponent<Is_food_on>().food_on = null;
			is_taken = true;
		}
		
		//Ramasser un objet du sol
		else if (Input.GetKeyDown(KeyCode.Space) && !is_taken && is_food)
		{
			Debug.Log("Prendre objet sol");
			Food.GetComponent<Rigidbody>().isKinematic = true;
			Food.GetComponent<Collider>().enabled = false;
			Food.transform.parent = t;
			Food.transform.position = Hand.position + Vector3.down * 0.9f;
			is_taken = true;
		}
		
		//Lacher un objet
		else if (is_taken && Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Lacher objet");
			Food.GetComponent<Rigidbody>().isKinematic = false;
			Food.GetComponent<Collider>().enabled = true;
			Food.transform.parent = null;
			is_taken = false;
		}


		is_food = false;
		is_inside = false;
		is_table = false;
	}
}
