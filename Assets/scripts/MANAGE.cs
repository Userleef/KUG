using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MANAGE : MonoBehaviour
{
    public void PLAY(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void exit()
    {
        Application.Quit();
    }
}
