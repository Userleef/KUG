using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView PhotonView;
    private int PlayersInGame = 0;
    private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private PlayerMovement CurrentPlayer;
    private Coroutine m_pingCoroutine;

    // Use this for initialization
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();

        PlayerName = "Player#" + Random.Range(1000, 9999);

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name[0] == 'N')
        {
            PhotonNetwork.masterClient.SetScore(scene.buildIndex);
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
        else
        {
            PlayersInGame = 0;
        }
    }
	
    private void MasterLoadedGame()
    {
        Debug.Log("Le master rejoind la game");
        
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        Debug.Log("Un client rejoind la game");
        
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(PhotonNetwork.masterClient.GetScore());
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        //PlayerManagement.Instance.AddPlayerStats(photonPlayer);

        PlayersInGame++;
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game scene.");
            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }

    public void NewHealth(PhotonPlayer photonPlayer, int health)
    {
        PhotonView.RPC("RPC_NewHealth", photonPlayer, health);
    }

    [PunRPC]
    private void RPC_NewHealth(int health)
    {
        if (CurrentPlayer == null)
            return;

        if (health <= 0)
            PhotonNetwork.Destroy(CurrentPlayer.gameObject);
        else
            CurrentPlayer.Health = health;
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        float randomValue = Random.Range(0f, 5f);
        GameObject obj = PhotonNetwork.Instantiate("gododo", new Vector3(-0.5f,0,2) + Vector3.up * 0.1f, Quaternion.identity, 0);
        obj.GetComponent<Controller>().enabled = true;
        //GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "NewPlayer"), Vector3.up * randomValue, Quaternion.identity, 0);
        CurrentPlayer = obj.GetComponent<PlayerMovement>();
        PhotonNetwork.masterClient.SetScore(0);
    }
    
    private IEnumerator C_SetPing()
    {
        while (PhotonNetwork.connected)
        {
            m_playerCustomProperties["Ping"] = PhotonNetwork.GetPing();
            PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);

            yield return new WaitForSeconds(5f);
        }

        yield break;
    }


    //When connected to the master server (photon).
    private void OnConnectedToMaster()
    {
        if (m_pingCoroutine != null)
            StopCoroutine(m_pingCoroutine);
        m_pingCoroutine = StartCoroutine(C_SetPing());
    }

}


