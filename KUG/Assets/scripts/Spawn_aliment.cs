using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Spawn_aliment : Photon.MonoBehaviour {

    public GameObject Food_inside;
	
	private bool have_food = false;
	private GameObject food_on;
    
    // Use this for initialization
    void Start () {
	    
	}

	// Update is called once per frame
	void Update ()
	{
	}

	public bool HaveFood
	{
		get { return have_food; }
		set { have_food = value; }
	}

	public GameObject FoodOn
	{
		get { return food_on; }
		set { food_on = value; }
	}

	public GameObject spawn(GameObject Food, Transform t, Transform Hand)
	{
		/*string food = Food.name;
		GameObject Aliment = PhotonNetwork.Instantiate(food, transform.position, Quaternion.identity, 0);
		Aliment.GetComponent<Rigidbody>().isKinematic = true;
		Aliment.GetComponent<Collider>().enabled = false;
		Aliment.transform.parent = t;
		Aliment.transform.position = Hand.position + Vector3.down * 0.9f;
		
		return Aliment;*/
		throw new Exception("xfgn");
	}
	
	
}
