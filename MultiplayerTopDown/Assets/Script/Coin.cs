using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public float tumble = 5f;

	GameObject temp;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;

		temp = GameObject.Find("CoinSound");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.GetComponent<PlayerShip>())
		{
			collision.collider.gameObject.GetComponent<PlayerShip>().addMoney(2f);
			GetComponent<PhotonView>().RPC("gethit", PhotonTargets.MasterClient, null);
		}	
	}

	[PunRPC]
	public void gethit()
	{
		if (PhotonNetwork.isMasterClient)
		{
			temp.GetComponent<AudioSource>().Play();
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
