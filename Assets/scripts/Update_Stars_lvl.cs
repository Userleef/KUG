using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_Stars_lvl : MonoBehaviour
{
	public int ID;
	public GameObject Save_Data;
	public int nb_Stars;

	public RawImage Star1;
	public RawImage Star2;
	public RawImage Star3;
	
	// Use this for initialization
	void Start ()
	{
		nb_Stars = Save_Data.GetComponent<Save_Data>().Get_nb_Stars(ID);
		
		
		if (nb_Stars >= 1)
		{
			Star1.gameObject.SetActive(true);
		}
		else
		{
			Star1.gameObject.SetActive(false);
		}
		if (nb_Stars >= 2)
		{
			Star2.gameObject.SetActive(true);
		}
		else
		{
			Star2.gameObject.SetActive(false);
		}
		if (nb_Stars >= 3)
		{
			Star3.gameObject.SetActive(true);
		}
		else
		{
			Star3.gameObject.SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
