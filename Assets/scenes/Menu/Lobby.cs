using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{

	public static string RoomName = "New Room";

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void nameRoom(string name)
	{
		RoomName = name;
	}
	
	void OnJoinedLobby()
	{
		RoomOptions RO = new RoomOptions(){isVisible = false,maxPlayers = 4};
		PhotonNetwork.JoinOrCreateRoom(RoomName, RO, TypedLobby.Default);
	}
}
