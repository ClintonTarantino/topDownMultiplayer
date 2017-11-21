using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    float seconds;


    public float damage = 10;
    public float speed = 10;
    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,-1) * speed;

    }

    // Update is called once per frame
    void Update () {
        seconds += Time.deltaTime;

        if (seconds > 3)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("We Hit " + collision.collider.gameObject.name);
        if (collision.collider.gameObject.GetComponent<PlayerShip>())
        {
            collision.collider.gameObject.GetComponent<PlayerShip>().gethit(damage);


        }
        Destroy(gameObject);
    }
}
