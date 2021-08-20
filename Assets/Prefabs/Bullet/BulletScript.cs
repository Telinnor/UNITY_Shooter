using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletScript : MonoBehaviour
{
    
    public float bulletSpeed;
    private GameObject Player;
    void Start()
    {
        Debug.Log("Bullet spawn");
        Player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,bulletSpeed*Time.deltaTime);
        if (Vector3.Distance(Player.transform.position,transform.position)>100){
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Obstacle Hitted");
        //Debug.Log(other.tag);
        if(other.tag=="Enemy"){
            other.SendMessage("BulletHit");
        }            
       
        Destroy(gameObject);
    }

}
