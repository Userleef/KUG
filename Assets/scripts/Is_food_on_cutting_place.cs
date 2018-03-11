using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Is_food_on_cutting_place : Photon.MonoBehaviour
{

	public bool have_food = false;
	public GameObject food_on;
	
	public GameObject Cutting_Tomato;
	public GameObject Cutting_Carot;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CutAliment()
	{
		
		GameObject Aliment_a_couper = food_on;
		
		if (food_on.tag == "F Tomato")
		{
			Aliment_a_couper = Cutting_Tomato;
		}
		if (food_on.tag == "F Carot")
		{
			Aliment_a_couper = Cutting_Carot;
		}
		
		Destroy(food_on);

		//Quaternion rot = new Quaternion(gameObject.GetComponent<Transform>().rotation.x - 15f, gameObject.GetComponent<Transform>().rotation.y, gameObject.GetComponent<Transform>().rotation.z, 0f);
		Transform cutting_aliment = PhotonView.Instantiate(Aliment_a_couper.transform, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
		food_on = cutting_aliment.gameObject;
		have_food = true;
		
			
		cutting_aliment.transform.parent = gameObject.GetComponent<Transform>();
		if(gameObject.GetComponent<Transform>().rotation == Quaternion.Euler(0f,-90,0f))
			cutting_aliment.transform.position = gameObject.GetComponent<Transform>().position + Vector3.up * 0.9f + Vector3.right * 0.3f;
		if(gameObject.GetComponent<Transform>().rotation == Quaternion.Euler(0f,0,0f))
			cutting_aliment.transform.position = gameObject.GetComponent<Transform>().position + Vector3.up * 0.9f + Vector3.back * 0.3f;
		if(gameObject.GetComponent<Transform>().rotation == Quaternion.Euler(0f,90,0f))
			cutting_aliment.transform.position = gameObject.GetComponent<Transform>().position + Vector3.up * 0.9f + Vector3.left * 0.3f;
		if(gameObject.GetComponent<Transform>().rotation == Quaternion.Euler(0f,180,0f))
			cutting_aliment.transform.position = gameObject.GetComponent<Transform>().position + Vector3.up * 0.9f + Vector3.forward * 0.3f;
	}
}
