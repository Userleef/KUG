using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Display_Star : MonoBehaviour
{
	public Camera MainCamera;
	
	public int palier1;
	public int palier2;
	public int palier3;
	
	public RawImage Star1;
	public RawImage Star2;
	public RawImage Star3;
	public AudioSource for3stars;
	public AudioSource for2stars;
	public AudioSource for1stars;
	//public AudioSource for0stars;

	public Text Display_score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void display_star()
	{
		int score = MainCamera.GetComponent<Command_Gestion>().Score;

		if (score >= palier1)
		{
			Star1.gameObject.SetActive(true);
		}
		else
		{
			Star1.gameObject.SetActive(false);
		}
		if (score >= palier2)
		{
			Star2.gameObject.SetActive(true);
		}
		else
		{
			Star2.gameObject.SetActive(false);
		}
		if (score >= palier3)
		{
			Star3.gameObject.SetActive(true);
		}
		else
		{
			Star3.gameObject.SetActive(false);
		}

		if (score >= palier3)
		{
			for3stars.mute = false;
		}
		else if (score >= palier2)
		{
			for3stars.mute = false;
		}
		else if (score >= palier1)
		{
			for2stars.mute = false;
		}
		else
		{
			for1stars.mute = false;
		}

		Display_score.GetComponent<Text>().text = "Score : " + score + " $";
	}
}
