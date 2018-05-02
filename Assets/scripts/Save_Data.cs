using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Data : MonoBehaviour
{

	public int Stars_LVL_1 = 0;
	public int Stars_LVL_2 = 0;
	public int Stars_LVL_3 = 0;
	public int Stars_LVL_4 = 0;
	public int Stars_LVL_5 = 0;
	public int Stars_LVL_6 = 0;
	public int Stars_LVL_7 = 0;
	public int Stars_LVL_8 = 0;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int Get_nb_Stars(int x)
	{
		if (x == 1)
		{
			return Stars_LVL_1;
		}
		else if (x == 2)
		{
			return Stars_LVL_2;
		}
		else if (x == 3)
		{
			return Stars_LVL_3;
		}
		else if (x == 4)
		{
			return Stars_LVL_4;
		}
		else if (x == 5)
		{
			return Stars_LVL_5;
		}
		else if (x == 6)
		{
			return Stars_LVL_6;
		}
		else if (x == 7)
		{
			return Stars_LVL_7;
		}
		else if (x == 8)
		{
			return Stars_LVL_8;
		}
		else
		{
			return 0;
		}
	}
}
