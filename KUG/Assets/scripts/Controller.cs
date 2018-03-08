using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controller : Photon.MonoBehaviour
{

	private Transform t;
	public float SpeedM;
	private bool is_placard;
	private bool is_taken;
	private bool is_food;
	private bool is_table;
	private bool is_cook;
	private bool on_floor;
	private GameObject Food;
	private GameObject Table;
	private GameObject Placard;
	private GameObject Cooking_place;
	public Transform Hand;
	
	private GameObject Aliment;

	public GameObject grab_object;

	// Use this for initialization
	void Start()
	{
		t = gameObject.GetComponent<Transform>();
	}

	// Update is called once per frame

	public static void Movment(int angle, Vector3 v1, Vector3 v2, ref Vector3 dir, Transform transform)
	{
		dir = v1 + v2;
		transform.rotation = Quaternion.Euler(0, angle, 0);
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
		if (Col.gameObject.tag[0] == 'F')
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
			is_placard = true;
			Placard = Col.gameObject;
			Food = Col.gameObject.GetComponent<Spawn_aliment>().Food_inside;
		}
		else if (Col.gameObject.tag == "Cooking_place")
		{
			is_cook = true;
			Cooking_place = Col.gameObject;
		}
	}


	void Update()
	{
		Vector3 dir = Vector3.zero;
		if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.S))
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
				//grab_object.transform.parent = Table.transform;
				Parent_t(grab_object, Table);
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
				//grab_object.transform.parent = t;
				Parent_t(grab_object, t.gameObject);
				grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
				is_taken = true;
			}

		}
		else if (is_placard)
		{
			//Poser un objet sur placard
			if (Input.GetKeyDown(KeyCode.Space) && is_taken && !Placard.GetComponent<Spawn_aliment>().HaveFood)
			{
				Debug.Log("Poser objet placard");
				Placard.GetComponent<Spawn_aliment>().HaveFood = true;
				Placard.GetComponent<Spawn_aliment>().FoodOn = grab_object;
				//grab_object.transform.parent = Placard.transform;
				Parent_t(grab_object, Placard);
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
				//grab_object.transform.parent = t;
				Parent_t(grab_object, t.gameObject);
				grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
				is_taken = true;
			}
			//Prendre un objet d'un placard
			else if (Input.GetKeyDown(KeyCode.Space) && !is_taken)
			{
				Debug.Log("Prendre objet placard");
				string food = Food.name;
				InstatantiateFood();
				//GameObject Aliment = PhotonNetwork.Instantiate(food, transform.position, Quaternion.identity, 0);
				Aliment.GetComponent<Rigidbody>().isKinematic = true;
				Aliment.GetComponent<Collider>().enabled = false;
				//Aliment.transform.parent = t;
				Parent_t(Aliment, t.gameObject);
				Aliment.transform.position = Hand.position + Vector3.down * 0.9f;
				is_taken = true;
				grab_object = Aliment.gameObject;
			}

		}
		else if (is_cook)
		{
			//Ajouter un aliment dans la casserole
			if (!Cooking_place.GetComponent<Cook>().Is_full() && Input.GetKeyDown(KeyCode.Space) && is_taken)
			{
				Debug.Log("Ajouter aliment casserole");
				Cooking_place.GetComponent<Cook>().Add_aliment(grab_object);
				//grab_object.transform.parent = null;
				Parentnull(grab_object);
				grab_object.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
				grab_object.transform.position = Cooking_place.transform.position + Cooking_place.GetComponent<Cook>()
					                                 .Slots[Cooking_place.GetComponent<Cook>().aliment_inside.Count - 1];
				is_taken = false;
				Cooking_place.GetComponent<Cook>().run_cook();
			}
		}
		//Ramasser un objet du sol
		else if (Input.GetKeyDown(KeyCode.Space) && !is_taken && is_food)
		{
			Debug.Log("Prendre objet sol");
			Food.GetComponent<Rigidbody>().isKinematic = true;
			Food.GetComponent<Collider>().enabled = false;
			//Food.transform.parent = t;
			Parent_t(Food, t.gameObject);
			Food.transform.position = Hand.position + Vector3.down * 0.9f;
			is_taken = true;
			grab_object = Food;
		}
		//Lacher un objet
		else if (is_taken && Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Lacher objet");
			Food.GetComponent<Rigidbody>().isKinematic = false;
			Food.GetComponent<Collider>().enabled = true;
			//Food.transform.parent = null;
			Parentnull(Food);
			is_taken = false;
		}


		is_food = false;
		is_placard = false;
		is_table = false;
		is_cook = false;

	}

	[PunRPC]
	void InstatantiateFood()
	{
		Aliment = PhotonNetwork.Instantiate(Food.name, transform.position, Quaternion.identity, 0);
	}
	
	[PunRPC]
	void Parentnull(GameObject ob)
	{
		ob.transform.parent = null;
	}
	
	[PunRPC]
	void Parent_t(GameObject ob, GameObject sur)
	{
		ob.transform.parent = sur.transform;
	}

}
