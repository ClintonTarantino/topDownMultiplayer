using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float seconds;


    public float damage = 10;
    public float speed = 1;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
        seconds += Time.deltaTime;

        if(seconds > 3) {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision collision) {

        Debug.Log("We Hit " + collision.collider.gameObject.name);
        if (collision.collider.gameObject.GetComponent<EnemyOne>()) {
            collision.collider.gameObject.GetComponent<PhotonView>().RPC("gethit",
                                                                         PhotonTargets.MasterClient, damage);
        }
        Destroy(gameObject);
    }
}
