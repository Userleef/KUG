using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generate_Command : MonoBehaviour
{
	public Camera MainCamera;
		
	public bool free = true;
	public RawImage Slot_ingredient1;
	public RawImage Slot_ingredient2;
	public RawImage Slot_ingredient3;
	public RawImage platImage;
	
	public Texture platfullcarotte;
	public Texture platfulltomate;
	public Texture platcarottes2tomate;
	public Texture plattomates2carottes;
	public Texture platvide;
	
	public float Timer_Command;
	public float Intervalle;
	public Image timeBarGreen;

	public List<string> recette_commande = new List<string>();
	
	/*public List<string> recette_Carot2tomateVS2;
	public List<string> recette_Tomates2carotVS2;
	
	public List<string> recette_Carot2tomateVS3;
	public List<string> recette_Tomates2carotVS3;*/
	
	// Use this for initialization
	void Start () {
		/*Debug.Log(recette_Carot3.Count);
		recette_Carot2tomate = new List<string>{"FC Carot","FC Carot","FC Tomato"};
		recette_Tomates2carot = new List<string>{"FC Tomato","FC Tomato","FC Carot"};
		recette_Tomato3 = new List<string>{"FC Tomato","FC Tomato","FC Tomato"};
		recette_Carot3 = new List<string>{"FC Carot","FC Carot","FC Carot"};
		
		Debug.Log(recette_Carot3.Count);*/
	}
	
	// Update is called once per frame
	void Update () {
		if(Timer_Command > 0)
			Timer_Command -= Time.deltaTime;
	}

	public void Validate_Commande()
	{
		MainCamera.GetComponent<Command_Gestion>().Score += 10;
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
				MainCamera.GetComponent<Command_Gestion>().Score -= 5;
				MainCamera.GetComponent<Command_Gestion>().Update_Score();
			}
			kill_command();
		}
	}
	
	/*public List<string> Sort(List<string> L)
	{
		bool sorted = false;

		while (!sorted)
		{
			sorted = true;
			for (int i = 1; i < L.Count; i++)
			{
				if (L[i][3] > L[i - 1][3])
				{
					sorted = false;
					string cur = L[i];
					L[i] = L[i - 1];
					L[i - 1] = cur;
				}
			}
		}

		return L;
	}*/
	public bool IsTheSame(List<string> L1, List<string> L2)
	{
		int L1Size = L1.Count;
		if (L1Size != L2.Count)
		{
			return false;
		}
		
		L1.Sort();
		L2.Sort();
		//L1 = Sort(L1);
		//L2 = Sort(L2);
		
		for (int i = 0; i < L1Size; i++)
		{
			if (L1[i] != L2[i])
			{
				return false;
			}
		}
		return true;
	}

	public void generate_Command(List<Texture> Ingr)
	{
		Timer_Command = Intervalle;
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
			platImage.GetComponent<RawImage>().texture = platvide;
		}
	}
}
