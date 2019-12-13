using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandler : MonoBehaviour
{
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        

        spawn = GameObject.Find("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();

    }

    void OnPhotonJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);

    }

    void OnJoinedRoom()
    {
        Spawn();
    }

    void Spawn()
    {
        GameObject player = (GameObject)PhotonNetwork.Instantiate("Player", spawn.transform.position, spawn.transform.rotation, 0);
        player.transform.Find("Camera").gameObject.SetActive(true);
        player.GetComponent<FPSController>().enabled = true;
        player.transform.Find("Body").gameObject.SetActive(false);
    }
}
