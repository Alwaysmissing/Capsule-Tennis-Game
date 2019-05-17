using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TG
{
    public class NetworkconnectionManager : MonoBehaviourPunCallbacks
    {
        public Button Btnconroom;
        public Button Btnconmaster;
        public string username;

        public bool TriesToconroom;
        public bool TriesToconmaster;
        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            TriesToconroom = false;
            TriesToconmaster = false;
            username = PlayerPrefs.GetString("PlayerName");
        }

        // Update is called once per frame
        void Update()
        {
            if (Btnconmaster != null)
                Btnconmaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToconmaster);
            if (Btnconroom != null)
                Btnconroom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToconmaster && !TriesToconroom);
        }

        public void OnClickConnectToMaster()
        {

            //Settings
            PhotonNetwork.OfflineMode = false;           //true 会模拟网络模式
            PhotonNetwork.NickName = username;       //设置一个玩家名
            //PhotonNetwork.AutomaticallySyncScene = true; //所有房间里的人会自动刷新主机的场景
            PhotonNetwork.GameVersion = "1.0";            //只有同一个版本号的玩家才能一起玩
            TriesToconmaster = true;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            TriesToconmaster = false;
            TriesToconroom = false;
            Debug.Log(cause);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            TriesToconmaster = false;
            Debug.Log("1");
        }

        public void OnClickConnectToRoom()
        {
            if (!PhotonNetwork.IsConnected)
                return;

            TriesToconroom = true;
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            TriesToconroom = false;
            Debug.Log("2");
            SceneManager.LoadScene(4);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            //no room available
            //create a room (null作为名字意思是不重要)
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            Debug.Log(message);
            base.OnCreateRoomFailed(returnCode, message);
            TriesToconroom = false;
        }
    }
}
