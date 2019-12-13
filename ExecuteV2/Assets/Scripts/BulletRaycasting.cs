using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycasting : MonoBehaviour
{
    GameObject[] players;
    GameObject[] AIs;
    public int shotAtDist = 3;
    private float startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        AIs = GameObject.FindGameObjectsWithTag("ai");
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {


        if (Time.time - startTime > 2)
        {
            Destroy(this.gameObject);
        }

        AIs = GameObject.FindGameObjectsWithTag("ai");
        foreach (GameObject ai in AIs)
        {
            if(Vector3.Distance(this.transform.position, ai.transform.position) < shotAtDist)
            {
                ai.GetComponent<StateController>().shotAt += 1;
                
            }
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
