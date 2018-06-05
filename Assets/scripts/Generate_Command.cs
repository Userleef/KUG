using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Generate_Command : NetworkBehaviour
{
	public Camera MainCamera;
		
	public bool free = true;
	public RawImage Slot_ingredient1;
	public RawImage Slot_ingredient2;
	public RawImage Slot_ingredient3;
	public RawImage platImage;
	
	public Texture platfullcarotte;
	public Texture platfulltomate;
	public Texture platfulloignon;
	public Texture platcarottes2tomate;
	public Texture plattomates2carottes;
	public Texture platvide;
	
	public float Timer_Command;
	public float Intervalle;
	public Image timeBarGreen;

	public List<string> recette_commande = new List<string>();
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Timer_Command > 0)
			Timer_Command -= Time.deltaTime;
	}

	public void Validate_Commande()
	{
		
		MainCamera.GetComponent<Command_Gestion>().Score += 30;
		MainCamera.GetComponent<Command_Gestion>().Update_Score();
		kill_command();
	}

	private void kill_command()
	{
		recette_commande = new List<string>();
		timeBarGreen.fillAmount = 1;
		gameObject.SetActive(false);
		free = true;
	}

	private void OnGUI()
	{
		if (Timer_Command > 0)
		{
			timeBarGreen.fillAmount = Timer_Command / Intervalle;
		}
		else
		{
			if (MainCamera.GetComponent<Command_Gestion>().Score >= 5)
			{
				MainCamera.GetComponent<Command_Gestion>().Score -= 10;
				MainCamera.GetComponent<Command_Gestion>().Update_Score();
			}
			kill_command();
		}
	}
	public bool IsTheSame(List<string> L1, List<string> L2)
	{
		int L1Size = L1.Count;
		if (L1Size != L2.Count)
		{
			return false;
		}
		
		L1.Sort();
		L2.Sort();
		
		for (int i = 0; i < L1Size; i++)
		{
			if (L1[i] != L2[i])
			{
				return false;
			}
		}
		return true;
	}

	public void generate_Command(List<Texture> Ingr, List<Texture> Ingr_Spe)
	{
		Timer_Command = Intervalle;
		
		int i =  Random.Range(0,2);

		if (i == 0 || Ingr_Spe.Count == 0)
		{
			
			Slot_ingredient1.gameObject.SetActive(true);
			Slot_ingredient2.gameObject.SetActive(true);
			Slot_ingredient3.gameObject.SetActive(true);
			
			int place = Random.Range(0,Ingr.Count);
			Slot_ingredient1.GetComponent<RawImage>().texture = Ingr[place];
			recette_commande.Add(Ingr[place].name);
		
			place = Random.Range(0,Ingr.Count);
			Slot_ingredient2.GetComponent<RawImage>().texture = Ingr[place];
			recette_commande.Add(Ingr[place].name);
		
			place = Random.Range(0,Ingr.Count);
			Slot_ingredient3.GetComponent<RawImage>().texture = Ingr[place];
			recette_commande.Add(Ingr[place].name);
			//recette_commande = Sort(recette_commande);
			recette_commande.Sort();
			if (IsTheSame(recette_commande, MainCamera.GetComponent<Command_Gestion>().recette_Carot3))
			{
				platImage.GetComponent<RawImage>().texture = platfullcarotte;
			}
			else if (IsTheSame(recette_commande, MainCamera.GetComponent<Command_Gestion>().recette_Tomato3))
			{
				platImage.GetComponent<RawImage>().texture = platfulltomate;
			}
			else if (IsTheSame(recette_commande, MainCamera.GetComponent<Command_Gestion>().recette_Tomates2carot))
			{
				platImage.GetComponent<RawImage>().texture = plattomates2carottes;
			}
			else if (IsTheSame(recette_commande, MainCamera.GetComponent<Command_Gestion>().recette_Carot2tomate))
			{
				platImage.GetComponent<RawImage>().texture = platcarottes2tomate;
			}
			else
			{
				platImage.GetComponent<RawImage>().texture = platfulloignon;
				//platImage.GetComponent<RawImage>().texture = platvide;
			}
		}
		else
		{
			int place = Random.Range(0,Ingr_Spe.Count);

			platImage.GetComponent<RawImage>().texture = Ingr_Spe[place];
			recette_commande = new List<string>(){Ingr_Spe[place].name};

			Slot_ingredient1.gameObject.SetActive(false);
			Slot_ingredient2.gameObject.SetActive(false);
			Slot_ingredient3.gameObject.SetActive(false);

		}
	}
}
