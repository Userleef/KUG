using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	
	private Transform t;
	public float SpeedM;
	private bool is_inside;
	private bool is_taken;
	private bool is_food;
	private bool is_table;
	private bool on_floor;
	private GameObject Food;
	private GameObject Table;
	private GameObject Placard;
	public Transform Hand;

	public GameObject grab_object;
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

	/*private void OnCollisionEnter()
	{
		if (!on_floor)
		{
			on_floor = true;
			t.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | 
			                                          RigidbodyConstraints.FreezeRotationX | 
													  RigidbodyConstraints.FreezeRotationZ;
			Debug.Log("Y position freeze");
		}
	}*/

	private void OnTriggerStay(Collider Col)
	{
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
		else if (Col.gameObject.tag == "Placard")
		{
			is_inside = true;
			Placard = Col.gameObject;
			Food = Col.gameObject.GetComponent<Spawn_aliment>().Food_inside;
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

		if (is_table)
		{
			//Poser un objet sur un plan de travail
			if (is_taken && Input.GetKeyDown(KeyCode.Space) && !Table.GetComponent<Is_food_on>().have_food)
			{
				Debug.Log("Poser objet plan de travail");
				grab_object.transform.parent = Table.transform;
				grab_object.transform.position = Table.transform.position + Vector3.up * 1.2f;
				Table.GetComponent<Is_food_on>().have_food = true;
				Table.GetComponent<Is_food_on>().food_on = grab_object;
				is_taken = false;
			}
			//Prendre un objet sur un plan de travail
			else if (!is_taken && Input.GetKeyDown(KeyCode.Space) && Table.GetComponent<Is_food_on>().have_food)
			{
				Debug.Log("Prendre objet plan de travail");
				grab_object = Table.GetComponent<Is_food_on>().food_on;
				Table.GetComponent<Is_food_on>().have_food = false;
				Table.GetComponent<Is_food_on>().food_on = null;
				grab_object.transform.parent = t;
				grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
				is_taken = true;
			}
			
		}
		else if (is_inside)
		{
			//Poser un objet sur placard
			if (Input.GetKeyDown(KeyCode.Space)&& is_taken && !Placard.GetComponent<Spawn_aliment>().HaveFood)
			{
				Debug.Log("Poser objet placard");
				Placard.GetComponent<Spawn_aliment>().HaveFood = true;
				Placard.GetComponent<Spawn_aliment>().FoodOn = grab_object;
				grab_object.transform.parent = Placard.transform;
				grab_object.transform.position = Placard.transform.position + Vector3.up * 1.2f;
				is_taken = false;
			} 
			//Prendre un objet POSE sur un placard
			else if (!is_taken && Input.GetKeyDown(KeyCode.Space) && Placard.GetComponent<Spawn_aliment>().HaveFood)
			{
				Debug.Log("Prendre objet posé sur un placard");
				grab_object = Placard.GetComponent<Spawn_aliment>().FoodOn;
				Placard.GetComponent<Spawn_aliment>().HaveFood = false;
				Placard.GetComponent<Spawn_aliment>().FoodOn = null;
				grab_object.transform.parent = t;
				grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
				is_taken = true;
			}
			//Prendre un objet d'un placard
			else if (Input.GetKeyDown(KeyCode.Space)&& !is_taken)
			{
				Debug.Log("Prendre objet placard");
				string food = Food.name;
				GameObject Aliment = PhotonNetwork.Instantiate(food, transform.position, Quaternion.identity, 0);
				Aliment.GetComponent<Rigidbody>().isKinematic = true;
				Aliment.GetComponent<Collider>().enabled = false;
				Aliment.transform.parent = t;
				Aliment.transform.position = Hand.position + Vector3.down * 0.9f;
				is_taken = true;
				grab_object = Aliment.gameObject;
			} 
			
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
