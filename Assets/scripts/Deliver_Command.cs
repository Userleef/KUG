using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deliver_Command : MonoBehaviour {
	
	
	public RawImage Slot_command_1;
	public RawImage Slot_command_2;
	public RawImage Slot_command_3;
	public RawImage Slot_command_4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	public bool IsTheSame(List<string> L1, List<string> L2)
	{
		int L1Size = L1.Count;
		if (L1Size != L2.Count)
		{
			return false;
		}
		
		for (int i = 0; i < L1Size; i++)
		{
			if (L1[i] != L2[i])
			{
				return false;
			}
		}
		return true;
	}

	public void Deliver()
	{
		Debug.Log("Deliver_command");
		
		List<string> recette_assiette = gameObject.GetComponent<Is_food_on>().food_on.GetComponent<Assiette>().recette_inside;

		if (IsTheSame(recette_assiette , Slot_command_1.GetComponent<Generate_Command>().recette_commande))
		{
			Slot_command_1.GetComponent<Generate_Command>().Validate_Commande();
		}
		else if (IsTheSame(recette_assiette , Slot_command_2.GetComponent<Generate_Command>().recette_commande))
		{
			Slot_command_2.GetComponent<Generate_Command>().Validate_Commande();
		}
		else if (IsTheSame(recette_assiette , Slot_command_3.GetComponent<Generate_Command>().recette_commande))
		{
			Slot_command_3.GetComponent<Generate_Command>().Validate_Commande();
		}
		else if (IsTheSame(recette_assiette , Slot_command_4.GetComponent<Generate_Command>().recette_commande))
		{
			Slot_command_4.GetComponent<Generate_Command>().Validate_Commande();
		}
	}
}
