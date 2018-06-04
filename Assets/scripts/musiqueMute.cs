using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class musiqueMute : MonoBehaviour {

	public AudioSource audio;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void mute()
	{
		audio.mute = !audio.mute;
	}
	
	

	
}
