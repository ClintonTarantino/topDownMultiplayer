using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour {

    float timer;
    float randomx;
    float randomy;
    float speed = 2;

    public float xMin;
    public float xMax;

    public float zMin;
    public float zMax;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(-90.0f, -180.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 1.0f) {
            timer = 0.0f;

            if (PhotonNetwork.isMasterClient ==true) {
                randomx = Random.Range(-0.1f, 0.1f);
                randomy = Random.Range( -0.1f, 0.1f);

                Vector3 movement = new Vector3(randomx, 0.0f, randomy);
                GetComponent<Rigidbody>().velocity = movement * speed;

                GetComponent<Rigidbody>().position = new Vector3(
                    Mathf.Clamp (GetComponent<Rigidbody>().position.x, xMin, xMax),
                     0.0f,
                      Mathf.Clamp (GetComponent<Rigidbody>().position.z, zMin,zMax)
                      );
               GetComponent<Rigidbody>().rotation = Quaternion.Euler(90.0f, -180.0f, 0.0f);
            }
        }
	}
}
