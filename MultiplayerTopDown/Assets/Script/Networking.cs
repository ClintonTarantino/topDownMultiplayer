using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networking : MonoBehaviour {

    public float timer;
	public float timerMoney;
	

	int showGUI = 1;

	Vector3 scale;
	float originalWidth = 800f;
	float originalHeight = 600f;

	GameObject mPlayer;

	public float money = 4f;

	string roomName = "room";

	Vector2 ScrollViewVector = Vector2.zero;

	int i;

	// Use this for initialization
	void Start () {
       
	}

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

		timerMoney += Time.deltaTime;

		if(showGUI == 3)
		{ 
        if (timer >= 10.0f)
        {
            timer = 0;
            if (PhotonNetwork.isMasterClient == true) {
                PhotonNetwork.Instantiate("EnemyShip", new Vector3(0.0f, 0.0f, 0.7f), Quaternion.identity, 0);
            }
        }
	}


		if (timerMoney >= 1f)
		{
			if(mPlayer != null)
			{
				money = mPlayer.GetComponent<PlayerShip>().money;
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

		//Join game or Create
		if(showGUI == 2)
		{

			//Increase Entering Name Font Size
			GUI.skin.textField.fontSize = 30;

			//The UI panel to allow  the players to add name
			GUI.Label(new Rect(100f, 100f, 150f, 40f), "Username: ");
			PhotonNetwork.playerName = GUI.TextField(new Rect(280f, 100f, 200f, 40f), PhotonNetwork.playerName, 15);

			//The UI panel to allow  the players to add name
			GUI.Label(new Rect(100f, 150f, 150f, 40f), "Roomname: ");
			this.roomName = GUI.TextField(new Rect(280f, 150f, 200f, 40f), this.roomName, 15);

			if (GUI.Button(new Rect(550f, 150f, 200f, 40f), "Create Room"))
			{
				if (PhotonNetwork.connected == true)
				{
					RoomOptions newRoomOptions = new RoomOptions();
					newRoomOptions.isVisible = true;
					newRoomOptions.isOpen = true;
					newRoomOptions.maxPlayers = 4;

					PhotonNetwork.CreateRoom(this.roomName, newRoomOptions, null);

					showGUI = 3;
				}

			}
			GUI.Label(new Rect(100f, 260f, 400f, 40f), " "+ PhotonNetwork.countOfPlayers + " User are online in "
				
				+PhotonNetwork.countOfRooms + " Room "
				);

			ScrollViewVector = GUI.BeginScrollView(new Rect(100f, 350f, 600f, 300f), ScrollViewVector,
				new Rect(0f, 0f, 500f, 5000f));

			i = 0;
			foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())

			{																						//e.g 2/4 players in room		
				GUI.Label(new Rect(20f, 20f +(50*i), 150f, 40f), roomInfo.name+"(" +roomInfo.playerCount+"/" + roomInfo.maxPlayers+")");

				if (GUI.Button(new Rect(300f, 20f + (50 * i), 150f, 40f), "Join"))
				{
					if (roomInfo.playerCount < roomInfo.maxPlayers)
					{
						PhotonNetwork.JoinRoom(roomInfo.name);
						showGUI = 3;
					}
				}
				i += 1;
			}
			GUI.EndScrollView();


		}
		//Start Game Scene
		if (showGUI == 3) {
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

			GUI.Label(new Rect(600f, 50f, 150f, 40f), "money: " + money);
		}
	}

    void OnJoinedLobby() {
     //   PhotonNetwork.JoinRandomRoom();
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
