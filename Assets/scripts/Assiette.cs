using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assiette : MonoBehaviour
{

	public GameObject sauce_assiette;
	public bool is_full;
	public List<string> recette_inside;
	public bool Is_clean = true;
	public Material sale;
	public GameObject Special_food;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void empty_assiette()
	{
		is_full = false;
		sauce_assiette.SetActive(false);
		recette_inside = new List<string>();
	}

	public void rendre_assiette()
	{
		recette_inside = new List<string>();
		is_full = false;
		Is_clean = false;
		sauce_assiette.SetActive(true);
		sauce_assiette.GetComponent<MeshRenderer>().material = sale;
		if (Special_food != null)
		{
			Special_food.SetActive(false);
			Special_food = null;
		}
	}
	
	public void Clean()
	{
		is_full = false;
		Is_clean = false;
		sauce_assiette.SetActive(true);
		sauce_assiette.GetComponent<MeshRenderer>().material = sale;
		Special_food.SetActive(false);
		Special_food = null;
	}
	
}
