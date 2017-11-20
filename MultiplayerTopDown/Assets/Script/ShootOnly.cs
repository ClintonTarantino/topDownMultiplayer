using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOnly : MonoBehaviour {

    public float seconds;
    public float fireRate;

    public GameObject shot;

    public Transform shotSpawn;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        seconds += Time.deltaTime;

        if(seconds > fireRate) {
            seconds = 0;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
	}
}
