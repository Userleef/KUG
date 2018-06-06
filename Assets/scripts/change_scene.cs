using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    public void play(int n)
    {
        //if(name[0] == 'm')
            //PhotonNetwork.Disconnect ();
        //SceneManager.LoadScene(name);
        PhotonNetwork.LoadLevel(n);
    }
    
    public void exit()
    {
        Application.Quit();
    }
}
