﻿using System;
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
	private bool is_cutting;
	private bool on_floor;
	private GameObject Food;
	private GameObject Table;
	private GameObject Placard;
	private GameObject Cooking_place;
	private GameObject Cutting_place;
	public Transform Hand;
	
	private Transform Aliment;

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
		else if (Col.gameObject.tag == "Cooking_place")
		{
			is_cook = true;
			Cooking_place = Col.gameObject;
		}
		else if (Col.gameObject.tag == "Placard")
		{
			is_placard = true;
			Placard = Col.gameObject;
			Food = Col.gameObject.GetComponent<Spawn_aliment>().Food_inside;
		}
		else if (Col.gameObject.tag == "Cutting_place")
		{
			is_cutting = true;
			Cutting_place = Col.gameObject;
		}
		else if (Col.gameObject.tag == "Table")
		{
			is_table = true;
			Table = Col.gameObject;
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


		if (is_cook)
		{
			//Ajouter un aliment dans la casserole
			if (!Cooking_place.GetComponent<Cook>().Is_full() && Input.GetKeyDown(KeyCode.Space) && is_taken && grab_object.tag[0] == 'F' && grab_object.tag[1] == 'C')
			{
				Debug.Log("Ajouter aliment casserole");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("Cook", PhotonTargets.AllBuffered, id1);
			}
		}
		else if (is_cutting)
		{
			//poser un objet sur planche à découper
			if (is_taken && Input.GetKeyDown(KeyCode.Space) && !Cutting_place.GetComponent<Is_food_on_cutting_place>().have_food)
			{
				Debug.Log("Poser objet cutting place");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PoserObjetCuttingPlace", PhotonTargets.AllBuffered, id1);
			}
			//Prendre un objet sur planche à découper
			else if (!is_taken && Input.GetKeyDown(KeyCode.Space) && Cutting_place.GetComponent<Is_food_on_cutting_place>().have_food)
			{
				Debug.Log("Prendre objet plan de travail");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PrendreObjetCuttingPlace", PhotonTargets.AllBuffered, id1);
			}
			//Couper un objet sur planche à découper
			if (!is_taken && Input.GetKeyDown(KeyCode.E) && Cutting_place.GetComponent<Is_food_on_cutting_place>().have_food && Cutting_place.GetComponent<Is_food_on_cutting_place>().food_on.tag[1] == ' ')
			{
				Debug.Log("Couper objet cutting place");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("Couper", PhotonTargets.AllBuffered, id1);
			}
		}
		else if (is_table)
		{
			//Poser un objet sur un plan de travail
			if (is_taken && Input.GetKeyDown(KeyCode.Space) && !Table.GetComponent<Is_food_on>().have_food)
			{
				Debug.Log("Poser objet plan de travail");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PoserObjetTable", PhotonTargets.AllBuffered, id1);
			}
			//Prendre un objet sur un plan de travail
			else if (!is_taken && Input.GetKeyDown(KeyCode.Space) && Table.GetComponent<Is_food_on>().have_food)
			{
				Debug.Log("Prendre objet plan de travail");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PrendreObjetTable", PhotonTargets.AllBuffered, id1);
			}

		}
		else if (is_placard)
		{
			//Poser un objet sur placard
			if (Input.GetKeyDown(KeyCode.Space) && is_taken && !Placard.GetComponent<Spawn_aliment>().HaveFood)
			{
				Debug.Log("Poser objet placard");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PoserObjetPlacard", PhotonTargets.AllBuffered, id1);
			}
			//Prendre un objet POSE sur un placard
			else if (!is_taken && Input.GetKeyDown(KeyCode.Space) && Placard.GetComponent<Spawn_aliment>().HaveFood)
			{
				Debug.Log("Prendre objet posé sur un placard");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PrendreObjetPosePlacard", PhotonTargets.AllBuffered, id1);
			}
			//Prendre un objet d'un placard
			else if (Input.GetKeyDown(KeyCode.Space) && !is_taken)
			{
				Debug.Log("Prendre objet placard");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PrendreObjetPlacard", PhotonTargets.AllBuffered, transform.position, transform.rotation, id1);
			}

		}
		//Ramasser un objet du sol
		else if (Input.GetKeyDown(KeyCode.Space) && !is_taken && is_food)
		{
			Debug.Log("Prendre objet sol");
			int id1 = PhotonNetwork.AllocateViewID();
			PhotonView photonView = this.GetComponent<PhotonView>();
			photonView.RPC("Ramasser", PhotonTargets.AllBuffered,id1);
		}
		//Lacher un objet
		else if (is_taken && Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Lacher objet");
			int id1 = PhotonNetwork.AllocateViewID();
			PhotonView photonView = this.GetComponent<PhotonView>();
			photonView.RPC("Lacher", PhotonTargets.AllBuffered,id1);
		}


		is_food = false;
		is_placard = false;
		is_table = false;
		is_cook = false;
		is_cutting = false;
	}

	
	[PunRPC]
	void PoserObjetCuttingPlace(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object.transform.parent = Cutting_place.transform;
		grab_object.transform.position = Cutting_place.transform.position + Vector3.up * 1.2f;
		Cutting_place.GetComponent<Is_food_on_cutting_place>().have_food = true;
		Cutting_place.GetComponent<Is_food_on_cutting_place>().food_on = grab_object;
		is_taken = false;
	}
	
	[PunRPC]
	void PrendreObjetCuttingPlace(int id1)
	{
		PhotonView[] nViews = Food.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object = Cutting_place.GetComponent<Is_food_on_cutting_place>().food_on;
		Cutting_place.GetComponent<Is_food_on_cutting_place>().have_food = false;
		Cutting_place.GetComponent<Is_food_on_cutting_place>().food_on = null;
		grab_object.GetComponent<Collider>().enabled = false;
		grab_object.transform.parent = t;
		grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
		is_taken = true;
	}
	
	[PunRPC]
	void Couper(int id1)
	{
		PhotonView[] nViews = Food.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Cutting_place.GetComponent<Is_food_on_cutting_place>().CutAliment();
	}
	
	[PunRPC]
	void PoserObjetTable(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object.transform.parent = Table.transform;
		grab_object.transform.position = Table.transform.position + Vector3.up * 1.2f;
		Table.GetComponent<Is_food_on>().have_food = true;
		Table.GetComponent<Is_food_on>().food_on = grab_object;
		is_taken = false;
	}
	
	[PunRPC]
	void PrendreObjetTable(int id1)
	{
		PhotonView[] nViews = Food.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object = Table.GetComponent<Is_food_on>().food_on;
		Table.GetComponent<Is_food_on>().have_food = false;
		Table.GetComponent<Is_food_on>().food_on = null;
		grab_object.transform.parent = t;
		grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
		is_taken = true;
	}
	
	[PunRPC]
	void PoserObjetPlacard(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Placard.GetComponent<Spawn_aliment>().HaveFood = true;
		Placard.GetComponent<Spawn_aliment>().FoodOn = grab_object;
		grab_object.transform.parent = Placard.transform;
		grab_object.transform.position = Placard.transform.position + Vector3.up * 1.2f;
		is_taken = false;
	}


	[PunRPC]
	void PrendreObjetPosePlacard(int id1)
	{
		PhotonView[] nViews = Food.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object = Placard.GetComponent<Spawn_aliment>().FoodOn;
		Placard.GetComponent<Spawn_aliment>().HaveFood = false;
		Placard.GetComponent<Spawn_aliment>().FoodOn = null;
		grab_object.transform.parent = t;
		grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
		is_taken = true;
	}

	[PunRPC]
	void PrendreObjetPlacard(Vector3 pos, Quaternion rot, int id1)
	{
		Aliment = Instantiate(Food.transform, pos, rot);
		// Set player's PhotonView
		PhotonView[] nViews = Aliment.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Aliment.GetComponent<Rigidbody>().isKinematic = true;
		Aliment.GetComponent<Collider>().enabled = false;
		Aliment.transform.parent = t;
		//photonView.RPC("Parent_t",  PhotonTargets.AllBuffered, Aliment.gameObject, t);
		Aliment.transform.position = Hand.position + Vector3.down * 0.9f;
		is_taken = true;
		grab_object = Aliment.gameObject;
	}

	[PunRPC]
	void Cook(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Cooking_place.GetComponent<Cook>().Add_aliment(grab_object);
		grab_object.transform.parent = null;
		//grab_object.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		grab_object.transform.position = Cooking_place.transform.position + Cooking_place.GetComponent<Cook>().Slots[Cooking_place.GetComponent<Cook>().aliment_inside.Count - 1];
		is_taken = false;
		Cooking_place.GetComponent<Cook>().run_cook();
	}

	[PunRPC]
	void Ramasser(int id1)
	{
		PhotonView[] nViews = Food.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Food.GetComponent<Rigidbody>().isKinematic = true;
		Food.GetComponent<Collider>().enabled = false;
		Food.transform.parent = t;
		Food.transform.position = Hand.position + Vector3.down * 0.9f;
		is_taken = true;
		grab_object = Food;
	}

	[PunRPC]
	void Lacher(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object.GetComponent<Rigidbody>().isKinematic = false;
		grab_object.GetComponent<Collider>().enabled = true;
		grab_object.transform.parent = null;
		is_taken = false;
	}
}
