using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networking : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Connect();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Connect() {
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }

    void OnGUI() {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby() {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed() {
        Debug.Log("Failed To Join");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom() {
        Debug.Log("We Joined the room!");
        GameObject mPlayer = (GameObject) PhotonNetwork.Instantiate("spaceship1", new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, 0);

       ((MonoBehaviour) mPlayer.GetComponent("PlayerShip")).enabled = true;
    }
}
