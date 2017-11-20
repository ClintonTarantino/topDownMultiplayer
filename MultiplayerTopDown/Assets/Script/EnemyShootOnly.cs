using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootOnly : MonoBehaviour {

    public float seconds;
    public float fireRate;

    public GameObject shot;

    public Transform shotSpawn1;
    public Transform shotSpawn2;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        seconds += Time.deltaTime;

        if (seconds > fireRate)
        {
            seconds = 0;
            Instantiate(shot, shotSpawn1.position, shot.transform.rotation);
            Instantiate(shot, shotSpawn2.position, shot.transform.rotation);
        }
    }
}
