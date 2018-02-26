﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		PhotonNetwork.offlineMode = false;
	}
	
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
		GameObject p = PhotonNetwork.Instantiate("Player 1", Vector3.zero + Vector3.up * 5, Quaternion.identity, 0);
		p.GetComponent<Controller>().enabled = true;
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Ransom join failed");
		PhotonNetwork.CreateRoom("room");
	}
}
