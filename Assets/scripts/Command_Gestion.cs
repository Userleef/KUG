using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Command_Gestion : MonoBehaviour
{
	public Text Display_Score;
	public int Score = 0;

	public RawImage Slot_command_1;
	public RawImage Slot_command_2;
	public RawImage Slot_command_3;
	public RawImage Slot_command_4;

	public float intervalle = 15;
	private float Timer_Command = 5;

	public RawImage[] Slots;

	public Texture Ingredient1;
	public Texture Ingredient2;
	public Texture Ingredient3;
	
	public List<string> recette_Tomato3 = new List<string>{"FC Tomato","FC Tomato","FC Tomato"};
	public List<string> recette_Carot3 = new List<string>{"FC Carot","FC Carot","FC Carot"};
	public List<string> recette_Carot2tomate = new List<string> {"FC Carot", "FC Carot", "FC Tomato"};
	public List<string> recette_Tomates2carot = new List<string>{"FC Tomato","FC Tomato","FC Carot"};

	public List<Texture> Ingr;
	
	// Use this for initialization
	void Start () {
		Slots = new RawImage[4]{Slot_command_1,Slot_command_2,Slot_command_3,Slot_command_4};
		if (Ingredient1 != null)
		{
			Ingr.Add(Ingredient1);
		}
		if (Ingredient2 != null)
		{
			Ingr.Add(Ingredient2);
		}
		if (Ingredient3 != null)
		{
			Ingr.Add(Ingredient3);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Timer_Command > 0 && Choose_Slot() != null)
			Timer_Command -= Time.deltaTime;
		else
		{
			Timer_Command = intervalle;
		}
	}

	public void Update_Score()
	{
		Display_Score.GetComponent<Text>().text = "Score : " + Score + " $";
	}
	
	private void OnGUI()
	{
		if (Timer_Command > 0)
		{
		}
		else
        {
			Timer_Command = intervalle;
			Run_Generation_Command();
		}
	}

	private RawImage Choose_Slot()
	{
		int i = 0;
		while (i < 4)
		{
			if (Slots[i].GetComponent<Generate_Command>().free)
			{
				return Slots[i];
			}
			i++;
		}

		return null;
	}
	
	private void Run_Generation_Command()
	{
		RawImage current_slot = Choose_Slot();
		if (current_slot != null)
		{
			current_slot.GetComponent<Generate_Command>().generate_Command(Ingr);
			current_slot.gameObject.SetActive(true);
			current_slot.GetComponent<Generate_Command>().free = false;
		}
	}
}
