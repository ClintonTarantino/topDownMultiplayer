using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float seconds;

    public float speed = 1;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
        seconds += Time.deltaTime;

        if(seconds > 0.5) {
            Destroy(gameObject);
        }
	}
}
