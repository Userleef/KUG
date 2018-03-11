using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    public void PLAY(string name)
    {
        if(name[0] == 'm')
            PhotonNetwork.Disconnect ();
        SceneManager.LoadScene(name);
    }
    public void exit()
    {
        Application.Quit();
    }
}
