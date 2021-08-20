using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public bool EnableSpawning; 
    public GameObject[] Enemys;
    public int NumberOfEnemys;
    public int EnemyDistance;
    
    void Start()
    {
        if (EnableSpawning){
            for (int i = 0; i < NumberOfEnemys; i++)
            {
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy(){
        GameObject currentEnemy;

        currentEnemy=Enemys[Random.Range(0,Enemys.Length)];

        Vector3 Position=gameObject.transform.position;
        Position.x+=Random.value * EnemyDistance * 2 - EnemyDistance;
        Position.y=15;
        Position.z+=Random.value * EnemyDistance * 2 - EnemyDistance;
        var Enemy=Instantiate(currentEnemy, Position,Random.rotation);

        //Enemy.transform.LookAt(gameObject.transform.position);
    }
}
