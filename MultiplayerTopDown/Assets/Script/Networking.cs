using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networking : MonoBehaviour {

    public float timer;


	//if show GUi = 1
	int showGUI = 1;

	Vector3 scale;
	float originalWidth = 800f;
	float originalHeight = 600f;

	GameObject mPlayer;

	// Use this for initialization
	void Start () {
       
	}

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
		  
        if (timer >= 10.0f)
        {
            timer = 0;
            if (PhotonNetwork.isMasterClient == true) {
                PhotonNetwork.Instantiate("EnemyShip", new Vector3(0.0f, 0.0f, 0.7f), Quaternion.identity, 0);
            }
        }
    }

    void Connect() {
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }

    void OnGUI() {
		//Scaling the Start Game Button to fit any screen
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.width / originalHeight;
		scale.z = 1;

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);


        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		//Show splash screen
		if (showGUI == 1) {

			//Increase Entering Name Font Size
			GUI.skin.textField.fontSize = 30;

			//The UI panel to allow  the players to add name
			GUI.Label(new Rect(100f, 120f, 150f, 40f), "Username:");
			PhotonNetwork.playerName = GUI.TextField(new Rect(280f, 120f, 250f, 40f), PhotonNetwork.playerName, 15);

		// left, top, width, length also, this is to start the game
		if(GUI.Button(new Rect(280f,200f,250f,40f),"Start Game")) {
				showGUI = 2;
			Connect();
			}
			//Quit game Button.
			if (GUI.Button(new Rect(280f, 260f, 250f, 40f), "Quit Game"))
			{
				Application.Quit();
			}
		}
		//Start Game Scene
		if (showGUI == 2) {
			//First Button
			if (GUI.Button(new Rect(20f, 50f, 50f, 40f), "B1"))
			{
				mPlayer.GetComponent<PlayerShip>().colorShip.GetComponent<MeshRenderer>().material.color = new Color(255f / 255f, 0, 0);
			}
			// Second Button
			if (GUI.Button(new Rect(20f, 100f, 50f, 40f), "B2"))
			{
				mPlayer.GetComponent<PlayerShip>().colorShip.GetComponent<MeshRenderer>().material.color = new Color(0, 255f / 255f, 0);
			}
			//Third Button
			if (GUI.Button(new Rect(20f, 150f, 50f, 40f), "B3"))
			{
				mPlayer.GetComponent<PlayerShip>().colorShip.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255f / 255f);
			}
		}
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
        mPlayer = (GameObject) PhotonNetwork.Instantiate("spaceship1", new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, 0);

       ((MonoBehaviour) mPlayer.GetComponent("PlayerShip")).enabled = true;


		//Add name onto our ship
		string PlayerUserName = mPlayer.GetComponent<PhotonView>().owner.name;

		mPlayer.GetComponentInChildren<TextMesh>().text = PlayerUserName; 
    }
}
