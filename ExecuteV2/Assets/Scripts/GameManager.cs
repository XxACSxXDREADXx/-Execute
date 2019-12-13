using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Com.Anch.Execute
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        public GameObject door;
        public GameObject Enemy;
        public int numEnemies = 5;
        public static GameManager Instance;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;
        private GameObject playerCam;
        private bool doorUnlocked;
        private EndGame endGameObj;

        #region Photon Callbacks



        void Start()
        {
            Instance = this;
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                    playerPrefab.GetComponent<FPSController>().enabled = true;
                    playerCam = playerPrefab.transform.Find("Camera").gameObject;
                    playerCam.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }

            endGameObj = GameObject.FindGameObjectWithTag("EndGame").GetComponent<EndGame>();
        }

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion


        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("ExecuteMain");
            SpawnEnemies();
        }


        #endregion

        #region Photon Callbacks


        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        public void SpawnEnemies()
        {
            for(int i = 0; i < numEnemies; i++)
            {

                Transform loc = null;
                loc.position = new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5));
                Instantiate(Enemy, loc);
            }
            
        }

        public void CheckEndgame()
        {
            int unlockedCount = 0;
            GameObject[] cyclers = GameObject.FindGameObjectsWithTag("Cycle");
            foreach(GameObject c in cyclers)
            {
               if (c.GetComponent<cycle>().isUnlocked)
               {
                   unlockedCount++;
               }
                
            }
            if(unlockedCount == cyclers.Length)
            {
                doorUnlocked = true;
            }
        }

        public void Update()
        {
            CheckEndgame();
            if (doorUnlocked)
            {
                if(door.transform.position.y < 10)
                {
                    door.transform.position += new Vector3(0f, 0.001f, 0f);
                }
            }

            if (endGameObj.gameOver)
            {
                OnLeftRoom();
            }
        }
        #endregion
    }
}