using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

	public GameObject player;
	
	private void OnTriggerStay(Collider Col)
	{
		if (Col.gameObject.tag[0] == 'F' || Col.gameObject.name == "Casserole" || Col.gameObject.tag == "assiette")
		{
			player.GetComponent<Controller>().is_food = true;
			player.GetComponent<Controller>().Food = Col.gameObject;
		}
		else if (Col.gameObject.tag == "Cooking_place")
		{
			player.GetComponent<Controller>().is_cook = true;
			player.GetComponent<Controller>().Cooking_place = Col.gameObject;
		}
		else if (Col.gameObject.tag == "Placard")
		{
			player.GetComponent<Controller>().is_placard = true;
			player.GetComponent<Controller>().Placard = Col.gameObject;
			player.GetComponent<Controller>().Food = Col.gameObject.GetComponent<Spawn_aliment>().Food_inside;
		}
		else if (Col.gameObject.tag == "Cutting_place")
		{
			player.GetComponent<Controller>().is_cutting = true;
			player.GetComponent<Controller>().Cutting_place = Col.gameObject;
		}
		else if (Col.gameObject.tag == "Table")
		{
			player.GetComponent<Controller>().is_table = true;
			player.GetComponent<Controller>().Table = Col.gameObject;
		}
		else if (Col.gameObject.tag == "poubelle")
		{
			player.GetComponent<Controller>().is_poubelle = true;
		}
		else if (Col.gameObject.tag == "rendu")
		{
			player.GetComponent<Controller>().is_rendu = true;
			player.GetComponent<Controller>().Rendu = Col.gameObject;
		}
		else if (Col.gameObject.tag == "lavabo")
		{
			player.GetComponent<Controller>().is_lavabo = true;
			player.GetComponent<Controller>().Lavabo = Col.gameObject;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
