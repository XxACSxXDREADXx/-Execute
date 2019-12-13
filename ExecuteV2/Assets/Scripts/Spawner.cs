using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject[] EnemyList = new GameObject[50];
    private Vector3 randPos;

    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {


        //EnemyList = GameObject.FindGameObjectsWithTag("ai");
        
        for(int i = 0; i < 49; i++)
        {
            print(EnemyList[i]);
            if(EnemyList[i] == null)
            {
                print("Spawn new enemy");
                randPos = new Vector3(Random.Range(-40, 40), 0, Random.Range(-40, 40));
                GameObject newEnemy = new GameObject("Enemy");
                newEnemy.transform.position = randPos;
                EnemyList[i] = Instantiate(Enemy, newEnemy.transform);
                
            }
                
        }

        EnemyList = GameObject.FindGameObjectsWithTag("ai");           
        
            
        
    }
}
