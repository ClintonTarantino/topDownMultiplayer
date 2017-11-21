using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {

    public float speed;

    public float xMin;
    public float xMax;

    public float zMin;
    public float zMax;

    public float life = 100;
    public float maxLife = 100;

    public Transform lifeBar;

	public Transform colorShip;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3(
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, xMin, xMax),
            0.0f,
             Mathf.Clamp(GetComponent<Rigidbody>().position.z, zMin, zMax)
            );

	}

    public void gethit (float damage) {
        if(life > 0) {
            life -= damage;
        }
        if(life <= 0) {
            PhotonNetwork.Instantiate("Explosion", new Vector3(this.transform.position.x, 0.0f, this.transform.position.z), Quaternion.identity, 0);
            PhotonNetwork.Destroy(gameObject);
        }
        lifeBar = transform.Find("myhealth");
        lifeBar.localScale = new Vector3(life / maxLife * 0.1f, 0.01f, 0.01f);
    }
}
