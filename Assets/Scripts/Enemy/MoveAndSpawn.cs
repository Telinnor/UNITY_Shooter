using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndSpawn : MonoBehaviour
{
    public float MovingSpeed;
    private float DefaultMovingSpeed=8.0f;
    private GameObject Player;
    void Start()
    {
        Player=GameObject.Find("Player");  
        if (MovingSpeed==0)
            MovingSpeed=DefaultMovingSpeed; 
    }


    void Update()
    {
        UpdateLook();
        UpdateMove();
    }


    private void UpdateLook(){
        gameObject.transform.LookAt(Player.transform.position);
    }
    private void UpdateMove(){
        gameObject.transform.Translate(0,0,MovingSpeed*Time.deltaTime);
       
    }

    public void BulletHit(){
        Respawn();
        ScorePoint();
        Destroy(gameObject);
    }
    private void Respawn(){
        Player.GetComponent<SpawnEnemys>().SpawnEnemy();
    }
    private void ScorePoint(){
        GameObject.Find("Score").GetComponent<Scoreboard>().GetPoint();
    }
}
