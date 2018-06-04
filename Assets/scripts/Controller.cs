using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controller : Photon.MonoBehaviour
{

	private Transform t;
	
	public float SpeedM = 5;
	public bool is_placard;
	public bool is_taken;
	public bool is_food;
	public bool is_table;
	public bool is_cook;
	public bool is_cutting;
	public bool is_poubelle;
	public bool is_rendu;
	public bool is_lavabo;
	
	public GameObject Food;
	public GameObject Table;
	public GameObject Placard;
	public GameObject Cooking_place;
	public GameObject Cutting_place;
	public GameObject Rendu;
	public GameObject Lavabo;
	
	public Transform Hand;
	private Transform Aliment;

	private Collider Col;

	public GameObject grab_object;
	public Animator walk;

	// Use this for initialization
	void Start()
	{
		t = gameObject.GetComponent<Transform>();
		walk = GetComponent<Animator>();
	}

	// Update is called once per frame

	public static void Movment(int angle, Vector3 v1, Vector3 v2, ref Vector3 dir, Transform transform)
	{
		dir = v1 + v2;
		transform.rotation = Quaternion.Euler(0, angle, 0);
	}

	void Update()
	{
		
		
		Vector3 dir = Vector3.zero;
		if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.S))
		{
			Movment(315 - 90, Vector3.left, Vector3.back, ref dir, transform);
			walk.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.Z))
		{
			Movment(45 -90, Vector3.left, Vector3.forward, ref dir, transform);
			walk.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Z))
		{
			Movment(135 - 90, Vector3.forward, Vector3.right, ref dir, transform);
			walk.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
		{
			Movment(225 - 90, Vector3.right, Vector3.back, ref dir, transform);
			walk.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.Q))
		{
			Movment(0 - 90, Vector3.left, Vector3.zero, ref dir, transform);
			walk.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.Z))
		{
			Movment(90 - 90, Vector3.forward, Vector3.zero, ref dir, transform);
			walk.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			Movment(180 - 90, Vector3.right, Vector3.zero, ref dir, transform);
			walk.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			Movment(270 - 90, Vector3.zero, Vector3.back, ref dir, transform);
			walk.SetBool("walk", true);
		}
		else
			walk.SetBool("walk", false);
		t.position += dir * Time.deltaTime * SpeedM;



		//Ramasser un objet du sol
		if (Input.GetKeyDown(KeyCode.Space) && Food.transform.parent == null && !is_taken && is_food)
		{
			Debug.Log("Prendre objet sol");
			int id1 = PhotonNetwork.AllocateViewID();
			PhotonView photonView = this.GetComponent<PhotonView>();
			photonView.RPC("Ramasser", PhotonTargets.AllBuffered,id1);
		}
		else if (is_cook)
		{
			//Prendre Marmitte
			if (Input.GetKeyDown(KeyCode.Space) && !is_taken)
			{
				Debug.Log("Prendre Marmitte");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PrendreMarmitte", PhotonTargets.AllBuffered, id1);
			}
			//Poser Marmitte four
			else if (Input.GetKeyDown(KeyCode.Space) && is_taken && grab_object.name == "Casserole" && Cooking_place.GetComponent<Cook>().marmitte == null)
			{
				Debug.Log("Poser Marmitte four");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PoserMarmitteFour", PhotonTargets.AllBuffered, id1);
			}
			//Ajouter un aliment dans la casserole
			else if (Cooking_place.GetComponent<Cook>().marmitte != null && !Cooking_place.GetComponent<Cook>().Is_full() && Input.GetKeyDown(KeyCode.Space) 
			                                                  && is_taken && grab_object.tag[0] == 'F' && grab_object.tag[1] == 'C' && Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Cooking_Pot>().sauce.tag != "Burned" )
			{
				Debug.Log("Ajouter aliment casserole");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("Cook", PhotonTargets.AllBuffered, id1);
			}
			//Remplir assiette en main
			else if (is_taken && grab_object.tag == "assiette" && Cooking_place.GetComponent<Cook>().marmitte != null && Cooking_place.GetComponent<Cook>().Is_full() && Input.GetKeyDown(KeyCode.Space) 
			         && Cooking_place.GetComponent<Cook>().marmitte.name == "Casserole" && !grab_object.GetComponent<Assiette>().is_full && grab_object.GetComponent<Assiette>().Is_clean 
			         && Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Cooking_Pot>().sauce.tag != "Burned" && Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Cooking_Pot>().Is_Ready)
			{
				Debug.Log("Remplir une assiette en main");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("RemplirAssietteEnMainFour", PhotonTargets.AllBuffered, id1 );
			}
			//Remplir assiette en main poele
			else if (is_taken && grab_object.tag == "assiette" && Cooking_place.GetComponent<Cook>().marmitte != null && Cooking_place.GetComponent<Cook>().Is_full() && Input.GetKeyDown(KeyCode.Space) 
			         && Cooking_place.GetComponent<Cook>().marmitte.name == "Stove" && !grab_object.GetComponent<Assiette>().is_full && grab_object.GetComponent<Assiette>().Is_clean 
			         && Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Stove>().food_on.tag != "Burned" && Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Stove>().Is_ready)
			{
				Debug.Log("Remplir une assiette en main Poele");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("RemplirAssietteEnMainFourPoele", PhotonTargets.AllBuffered, id1 );
			}
			//Ajouter un aliment dans la poele
			else if (Cooking_place.GetComponent<Cook>().marmitte != null && Cooking_place.GetComponent<Cook>().marmitte.name == "Stove" && !Cooking_place.GetComponent<Cook>().Is_full() && Input.GetKeyDown(KeyCode.Space) 
			         && is_taken && (grab_object.tag == "F Steak" || grab_object.tag == "F Fish"))
			{
				Debug.Log("Ajouter aliment Poele");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("Cook", PhotonTargets.AllBuffered, id1);
			}
			//Poser Poele four
			else if (Input.GetKeyDown(KeyCode.Space) && is_taken && grab_object.name == "Stove" && Cooking_place.GetComponent<Cook>().marmitte == null)
			{
				Debug.Log("Poser Marmitte four");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PoserMarmitteFour", PhotonTargets.AllBuffered, id1);
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
			else if (!is_taken && Input.GetKeyDown(KeyCode.Space) && Cutting_place.GetComponent<Is_food_on_cutting_place>().have_food && !Cutting_place.GetComponent<Is_food_on_cutting_place>().is_cutting)
			{
				Debug.Log("Prendre objet planche à découper");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PrendreObjetCuttingPlace", PhotonTargets.AllBuffered, id1);
			}
			//Couper un objet sur planche à découper
			else if (!is_taken && Input.GetKeyDown(KeyCode.E) && Cutting_place.GetComponent<Is_food_on_cutting_place>().have_food && Cutting_place.GetComponent<Is_food_on_cutting_place>().food_on.tag[1] == ' ')
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
			//Remplir assiette
			else if (is_taken &&  Input.GetKeyDown(KeyCode.Space) && Table.GetComponent<Is_food_on>().have_food && grab_object.name == "Casserole" && grab_object.GetComponent<Cooking_Pot>().Is_full() && Table.GetComponent<Is_food_on>().food_on.tag == "assiette" && !Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().is_full && Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().Is_clean && grab_object.GetComponent<Cooking_Pot>().sauce.tag != "Burned" && grab_object.GetComponent<Cooking_Pot>().Is_Ready)
			{
				Debug.Log("Remplir une assiette");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("RemplirAssiette", PhotonTargets.AllBuffered, id1 );
			}
			//Remplir assiette Poele
			else if (is_taken &&  Input.GetKeyDown(KeyCode.Space) && Table.GetComponent<Is_food_on>().have_food && grab_object.name == "Stove" && grab_object.GetComponent<Stove>().Is_full() && Table.GetComponent<Is_food_on>().food_on.tag == "assiette" && !Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().is_full && Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().Is_clean && grab_object.GetComponent<Stove>().food_on.tag != "Burned" && grab_object.GetComponent<Stove>().Is_ready)
			{
				Debug.Log("Remplir une assiette");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("RemplirAssiettePoele", PhotonTargets.AllBuffered, id1 );
			}
			//Remplir assiette en main
			else if (is_taken && Input.GetKeyDown(KeyCode.Space) && grab_object.tag == "assiette" && Table.GetComponent<Is_food_on>().have_food && Table.GetComponent<Is_food_on>().food_on.name == "Casserole" && Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().Is_full()  && !grab_object.GetComponent<Assiette>().is_full &&  grab_object.GetComponent<Assiette>().Is_clean && Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().sauce.tag != "Burned" && Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().Is_Ready)
			{
				Debug.Log("Remplir une assiette en main");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("RemplirAssietteEnMain", PhotonTargets.AllBuffered, id1 );
			}
			//Remplir assiette en main Poele
			else if (is_taken && Input.GetKeyDown(KeyCode.Space) && grab_object.tag == "assiette" && Table.GetComponent<Is_food_on>().have_food && Table.GetComponent<Is_food_on>().food_on.name == "Stove" && Table.GetComponent<Is_food_on>().food_on.GetComponent<Stove>().Is_full()  && !grab_object.GetComponent<Assiette>().is_full &&  grab_object.GetComponent<Assiette>().Is_clean && Table.GetComponent<Is_food_on>().food_on.GetComponent<Stove>().food_on.tag != "Burned" && Table.GetComponent<Is_food_on>().food_on.GetComponent<Stove>().Is_ready)
			{
				Debug.Log("Remplir une assiette en main Poele");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("RemplirAssietteEnMainPoele", PhotonTargets.AllBuffered, id1 );
			}
			//remplir casserole posée sur table
			else if (is_taken && Table.GetComponent<Is_food_on>().have_food && Input.GetKeyDown(KeyCode.Space) &&
			         grab_object.tag[0] == 'F' && grab_object.tag[1] == 'C' &&
			         Table.GetComponent<Is_food_on>().food_on.name == "Casserole"
			         && !Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().Is_full() &&
			         Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().sauce.tag != "Burned")
			{
				Debug.Log("remplir casserole posée sur table");
				int id1 = PhotonNetwork.AllocateViewID();
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("remplircasseroleposéesurtable", PhotonTargets.AllBuffered, id1 );
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
		else if (is_poubelle)
		{
			// jeter une assiette
			if (Input.GetKeyDown(KeyCode.Space) && is_taken && grab_object.tag == "assiette")
			{
				
				Debug.Log("Jeter une assiette");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("JeterAssiette", PhotonTargets.AllBuffered, id1);
			}
			// jeter une marmitte
			else if (Input.GetKeyDown(KeyCode.Space) && is_taken && grab_object.name == "Casserole")
			{
				
				Debug.Log("Jeter une marmitte");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("JeterMarmitte", PhotonTargets.AllBuffered,  id1);
			}
			// jeter un Aliment
			else if (Input.GetKeyDown(KeyCode.Space) && is_taken && grab_object.tag[0] == 'F')
			{
				
				Debug.Log("Jeter un aliment");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("JeterAliment", PhotonTargets.AllBuffered,  id1);
			}
			// jeter une poele
			else if (Input.GetKeyDown(KeyCode.Space) && is_taken && grab_object.name == "Stove")
			{
				
				Debug.Log("Jeter une poele");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("JeterPoele", PhotonTargets.AllBuffered,  id1);
			}
			
		}
		else if (is_rendu)
		{
			//rendre assiette
			if (Input.GetKeyDown(KeyCode.Space) && is_taken && !Rendu.GetComponent<Is_food_on>().have_food && grab_object.tag == "assiette" && grab_object.GetComponent<Assiette>().is_full)
			{
				
				Debug.Log("Rendre une assiette");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("RendreAssiette", PhotonTargets.AllBuffered,  id1);
			}
			//reprendre assiette sale
			else if (Input.GetKeyDown(KeyCode.Space) && !is_taken && Rendu.GetComponent<Is_food_on>().have_food)
			{
				
				Debug.Log("Prendre assiette rendu");
				
				int id1 = PhotonNetwork.AllocateViewID();
 
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("PrendreAssietteRendu", PhotonTargets.AllBuffered,  id1);
			}
		}
		else if (is_lavabo)
		{
			//Laver assiette
			if (Input.GetKeyDown(KeyCode.Space) && is_taken && grab_object.tag == "assiette" &&
			    !grab_object.GetComponent<Assiette>().Is_clean)
			{
				Debug.Log("Laver Assiette");
				
				int id1 = PhotonNetwork.AllocateViewID();
				
				PhotonView photonView = this.GetComponent<PhotonView>();
				photonView.RPC("LaverAssiette", PhotonTargets.AllBuffered, id1);
			}
		}
		//Lacher un objet
		else if (is_taken && Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Lacher objet");
			int id1 = PhotonNetwork.AllocateViewID();
			PhotonView photonView = this.GetComponent<PhotonView>();
			photonView.RPC("Lacher", PhotonTargets.AllBuffered, id1);
		}


		is_food = false;
		is_placard = false;
		is_table = false;
		is_cook = false;
		is_cutting = false;
		is_poubelle = false;
		is_rendu = false;
		is_lavabo = false;
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
		PhotonView[] nViews = Cutting_place.GetComponent<Is_food_on_cutting_place>().food_on.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		
		Cutting_place.GetComponent<Is_food_on_cutting_place>().food_on.GetComponent<Syncro>().enabled = true;
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

		Cutting_place.GetComponent<Is_food_on_cutting_place>().food_on.GetComponent<Syncro>().enabled = true;
		Cutting_place.GetComponent<Is_food_on_cutting_place>().Run_Cut();
	}
	
	[PunRPC]
	void PoserObjetTable(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object.transform.parent = Table.transform;
		if (grab_object.tag == "assiette")
			grab_object.transform.position = Table.transform.position + Vector3.up * 0.75f;
		else
			grab_object.transform.position = Table.transform.position + Vector3.up * 1.2f;
		Table.GetComponent<Is_food_on>().have_food = true;
		Table.GetComponent<Is_food_on>().food_on = grab_object;
		is_taken = false;
	}
	
	[PunRPC]
	void PrendreObjetTable(int id1)
	{
		PhotonView[] nViews = Table.GetComponent<Is_food_on>().food_on.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Syncro>().enabled = true;
		grab_object = Table.GetComponent<Is_food_on>().food_on;
		Table.GetComponent<Is_food_on>().have_food = false;
		Table.GetComponent<Is_food_on>().food_on = null;
		grab_object.transform.parent = t;
		grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
		is_taken = true;
	}
	
	
	[PunRPC]
	void RemplirAssiette(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().is_full = true;
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().recette_inside = grab_object.GetComponent<Cooking_Pot>().aliment_inside;
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().sauce_assiette.SetActive(true);
		if (grab_object.name == "Casserole")
		{
			Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().sauce_assiette.GetComponent<Renderer>().material
					.color =
				grab_object.GetComponent<Cooking_Pot>().sauce.GetComponent<Renderer>().material.color;
		}

		grab_object.GetComponent<Cooking_Pot>().empty_aliment();
	}
	
	
	[PunRPC]
	void RemplirAssiettePoele(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().is_full = true;
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().recette_inside = new List<string>(){grab_object.GetComponent<Stove>().food_on.tag};
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().Special_food = grab_object.GetComponent<Stove>().food_on;
		grab_object.GetComponent<Stove>().food_on.transform.parent = Table.GetComponent<Is_food_on>().food_on.transform;
		grab_object.GetComponent<Stove>().food_on.transform.position =
			Table.GetComponent<Is_food_on>().food_on.transform.position + Vector3.up * 0.1f;
		
		grab_object.GetComponent<Stove>().empty_aliment();
	}
	
	
	[PunRPC]
	void remplircasseroleposéesurtable(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().Add_aliment(grab_object);
		grab_object.transform.parent = Cooking_place.transform;
		is_taken = false;
	}
	
	[PunRPC]
	void RemplirAssietteEnMain(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		grab_object.GetComponent<Assiette>().is_full = true;
		grab_object.GetComponent<Assiette>().recette_inside = Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().aliment_inside;
		grab_object.GetComponent<Assiette>().sauce_assiette.SetActive(true);
		if (Table.GetComponent<Is_food_on>().food_on.name == "Casserole")
		{
			grab_object.GetComponent<Assiette>().sauce_assiette.GetComponent<Renderer>().material
					.color =
				Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().sauce.GetComponent<Renderer>().material.color;
		}

		Table.GetComponent<Is_food_on>().food_on.GetComponent<Cooking_Pot>().empty_aliment();
	}
	
	[PunRPC]
	void RemplirAssietteEnMainPoele(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object.GetComponent<Assiette>().is_full = true;
		grab_object.GetComponent<Assiette>().recette_inside = new List<string>(){Table.GetComponent<Is_food_on>().food_on.tag};
		grab_object.GetComponent<Assiette>().Special_food =
			Table.GetComponent<Is_food_on>().food_on.GetComponent<Stove>().food_on;
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Stove>().food_on.transform.parent = grab_object.transform;
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Stove>().food_on.transform.position =
			grab_object.transform.position + Vector3.up * 0.1f;
		Table.GetComponent<Is_food_on>().food_on.GetComponent<Stove>().empty_aliment();
	}
	
	[PunRPC]
	void RemplirAssietteEnMainFour(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		grab_object.GetComponent<Assiette>().is_full = true;
		grab_object.GetComponent<Assiette>().recette_inside = Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Cooking_Pot>().aliment_inside;
		grab_object.GetComponent<Assiette>().sauce_assiette.SetActive(true);
		if (Cooking_place.GetComponent<Cook>().marmitte.name == "Casserole")
		{
			grab_object.GetComponent<Assiette>().sauce_assiette.GetComponent<Renderer>().material
					.color =
				Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Cooking_Pot>().sauce.GetComponent<Renderer>().material.color;
		}

		Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Cooking_Pot>().empty_aliment();
	}
	
	[PunRPC]
	void RemplirAssietteEnMainFourPoele(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		grab_object.GetComponent<Assiette>().is_full = true;
		grab_object.GetComponent<Assiette>().recette_inside = new List<string>(){Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Stove>().food_on.tag};
		grab_object.GetComponent<Assiette>().Special_food =
			Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Stove>().food_on;
		Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Stove>().food_on.transform.parent = grab_object.transform;
		Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Stove>().food_on.transform.position =
			grab_object.transform.position + Vector3.up * 0.1f;
		Cooking_place.GetComponent<Cook>().marmitte.GetComponent<Stove>().empty_aliment();
	}
	
	[PunRPC]
	void PoserObjetPlacard(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Placard.GetComponent<Spawn_aliment>().HaveFood = true;
		Placard.GetComponent<Spawn_aliment>().FoodOn = grab_object;
		grab_object.transform.parent = Placard.transform;
		if (grab_object.tag == "assiette")
			grab_object.transform.position = Placard.transform.position + Vector3.up * 0.75f;
		else
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
		
		grab_object.transform.parent = Cooking_place.transform;
		Cooking_place.GetComponent<Cook>().Add_aliment(grab_object);
		is_taken = false;
	}
	
	[PunRPC]
	void PrendreMarmitte(int id1)
	{
		PhotonView[] nViews = Cooking_place.GetComponent<Cook>().marmitte.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;
		
		Cooking_place.GetComponent<Cook>().Take();
		grab_object = Cooking_place.GetComponent<Cook>().marmitte.gameObject;
		grab_object.GetComponent<Syncro>().enabled = true;
		Cooking_place.GetComponent<Cook>().marmitte = null;
		grab_object.transform.parent = t;
		grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
		is_taken = true;
	}
	
	
	[PunRPC]
	void PoserMarmitteFour(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;

		Cooking_place.GetComponent<Cook>().marmitte = grab_object;
		Cooking_place.GetComponent<Cook>().Put();
		grab_object.transform.parent = Cooking_place.transform;
		if(grab_object.name == "Casserole")
			grab_object.transform.position = Cooking_place.transform.position + Vector3.up * 1.2f;
		if(grab_object.name == "Stove")
			grab_object.transform.position = Cooking_place.transform.position + Vector3.up * 0.85f;
		grab_object.transform.rotation = Cooking_place.transform.rotation;
		is_taken = false;
	}
	
	[PunRPC]
	void JeterAssiette(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;

		grab_object.GetComponent<Assiette>().Clean();
	}
	
	
	[PunRPC]
	void JeterMarmitte(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;

		grab_object.GetComponent<Cooking_Pot>().empty_aliment();
	}
	
	[PunRPC]
	void JeterPoele(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;

		grab_object.GetComponent<Stove>().food_on.transform.parent = null;
		grab_object.GetComponent<Stove>().food_on.SetActive(false);
		grab_object.GetComponent<Stove>().empty_aliment();
	}
	
	[PunRPC]
	void JeterAliment(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;

		grab_object.transform.parent = null;
		is_taken = false;
		Destroy(grab_object);
	}
	
	[PunRPC]
	void RendreAssiette(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;

		grab_object.transform.parent = Rendu.transform;
		grab_object.transform.position = Rendu.transform.position + Vector3.up * 0.75f;
		Rendu.GetComponent<Is_food_on>().have_food = true;
		Rendu.GetComponent<Is_food_on>().food_on = grab_object;
		is_taken = false;
		Rendu.GetComponent<Deliver_Command>().Deliver();
		Rendu.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().rendre_assiette();
	}
	
	[PunRPC]
	void PrendreAssietteRendu(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;

		grab_object = Rendu.GetComponent<Is_food_on>().food_on;
		Rendu.GetComponent<Is_food_on>().have_food = false;
		grab_object.GetComponent<Syncro>().enabled = true;
		grab_object.transform.parent = t;
		grab_object.transform.position = Hand.position + Vector3.down * 0.9f;
		is_taken = true;
	}
	
	[PunRPC]
	void LaverAssiette(int id1)
	{
		PhotonView[] nViews = grab_object.GetComponentsInChildren<PhotonView>(); 
		nViews[0].viewID = id1;

		Lavabo.GetComponent<Lavabo>().Run_Wash();
		
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
