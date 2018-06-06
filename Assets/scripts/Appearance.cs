using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : Photon.MonoBehaviour
{
	public Vector3 spawn_position;
	
	// Use this for initialization
	void Start ()
	{
		//PhotonNetwork.ConnectUsingSettings("0.1");
		//PhotonNetwork.offlineMode = false;
		//GameObject p = PhotonNetwork.Instantiate("gododo", spawn_position + Vector3.up * 0.1f, Quaternion.identity, 0);
		//p.GetComponent<Controller>().enabled = true;
	}
	/*
	
	// Update is called once per frame
	//void Update ()
	//{
	//	if (Input.GetKeyDown(KeyCode.P))
	//		Instantiate(Resources.Load("Player 1"), Vector3.zero + Vector3.up * 5, Quaternion.identity);
	//}

	void OnJoinedLobby()
	{
		Debug.Log("Joined lobby");
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnJoinedRoom()
	{
		Debug.Log("Joined room");
		GameObject p = PhotonNetwork.Instantiate("gododo", spawn_position + Vector3.up * 0.1f, Quaternion.identity, 0);
		p.GetComponent<Controller>().enabled = true;
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Random join failed");
		PhotonNetwork.CreateRoom("room");
	}*/
}
