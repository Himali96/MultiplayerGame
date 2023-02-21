using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    GameObject controller;

    int kills, deaths;
    public bool isGameOver = false;

    void Awake ()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController ()
    {
        Transform spawnpoint = SpawnManager._instance.GetSpawnpoints();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position, spawnpoint.rotation, 
            0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();

        deaths++;

        Hashtable hash = new Hashtable();
        hash.Add("deaths", deaths);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public void GetKill()
    {
        PV.RPC(nameof(RPC_GetKill), PV.Owner);
    }

    [PunRPC]
    void RPC_GetKill ()
    {
        kills++;

        Hashtable hash = new Hashtable();
        hash.Add("kills", kills);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        if(kills >= 10)
        {
            DisplayGameOver();
        }
    }

    public void DisplayGameOver()
    {
        PV.RPC(nameof(DisplayGameOverRpc), RpcTarget.All);
    }

    [PunRPC]
    void DisplayGameOverRpc()
    {
        if (PV.IsMine) 
            ScoreboardItem._instance.usernameTxt.text = ScoreboardItem._instance.usernameTxt.text + " Wins";

        if (Scoreboard._instance.canvasGroup.alpha == 1) 
            return;
        Scoreboard._instance.canvasGroup.alpha = 1;
        isGameOver = true;

        StartCoroutine(LoadLevelAfterDelay(15));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
    }
}
