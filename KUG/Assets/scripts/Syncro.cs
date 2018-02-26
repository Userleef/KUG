using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syncro : MonoBehaviour
{

	private Vector3 correctPos;
	private Quaternion correctRot;
	private PhotonView view;
	
	// Use this for initialization
	void Start ()
	{
		view = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!view.isMine)
		{
			transform.position = Vector3.Lerp(transform.position, this.correctPos, Time.deltaTime * 20);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctRot, Time.deltaTime * 20);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			this.correctPos = (Vector3) stream.ReceiveNext();
			this.correctRot = (Quaternion) stream.ReceiveNext();
		}
	}
}
