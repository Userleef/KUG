using UnityEngine;
using System.Collections;

public class musique : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		GameObject[] sound = GameObject.FindGameObjectsWithTag("Musique");
		if(sound.Length > 1)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
