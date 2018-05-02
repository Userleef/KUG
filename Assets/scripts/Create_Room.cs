using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_Room : MonoBehaviour {
	
	public string roomName;
	public string Password;
	public int nb_player;
	public bool IsConnected = false;
	public InputField Input_Name;
	public InputField Input_Pass;
	public Dropdown Input_nb_player;

	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		//PhotonNetwork.offlineMode = false;
		roomName = "Room " + Random.Range(0,999);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void create()
	{
		roomName = Input_Name.GetComponent<InputField>().text;
		Password = Input_Pass.GetComponent<InputField>().text;
		nb_player = Input_nb_player.GetComponent<Dropdown>().value;
		PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
		Debug.Log("Room created");
	}
	
	void OnJoinedLobby()
	{
		Debug.Log("Joined lobby");
	}
	
	void OnJoinedRoom()
	{
		Debug.Log("Joined room");
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Random join failed");
		PhotonNetwork.CreateRoom("room");
	}
	
	void OnGUI()
	{
		if (IsConnected)
		{
			Debug.Log("OnGUI");
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500));
			roomName = GUILayout.TextField(roomName);

			if (GUILayout.Button("Create"))
			{
				PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
			}

			foreach (RoomInfo game in PhotonNetwork.GetRoomList())
			{
				if (GUILayout.Button(game.name + " " + game.playerCount + "/" + game.maxPlayers))
				{
					PhotonNetwork.JoinOrCreateRoom(game.name, null, null);
				}
			}
			GUILayout.EndArea();
		}
	}
}
