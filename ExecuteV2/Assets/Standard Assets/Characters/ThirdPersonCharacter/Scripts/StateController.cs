using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UIElements;

public class StateController : MonoBehaviour {

    public State currentState;
    public GameObject[] navPoints;
    private GameObject[] hidePoints;
    public GameObject[] coverPoints;
    public GameObject enemyToChase;
    public int navPointNum;
    public float remainingDistance;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public Renderer[] childrenRend;
    public GameObject[] enemies;
    public float detectionRange = 30;
    public double health = 100.0;
    


    [HideInInspector]
    public int shotAt = 0;
    public float startTime;
    public float maxTime;
    public TextMesh healthText;

    public Transform GetNextNavPoint()
    {
        navPointNum = Random.Range(0, navPoints.Length);
        return navPoints[navPointNum].transform;
    }

    

    public Transform GetHidePoint()
    {
        float dist = 1000000000;
        GameObject hidePoint = hidePoints[0];
        foreach(GameObject point in hidePoints){
            float distTemp = Vector3.Distance(point.transform.position, ai.transform.position);
            
            if(dist > distTemp)
            {
                
                dist = distTemp;
                hidePoint = point;
            }
        }

        Transform hidingSpot = hidePoint.transform;
        return hidingSpot;
    }

    public bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 direction = ai.transform.position - enemyToChase.transform.position;
        if (Physics.Raycast(enemyToChase.transform.position, direction, out hit)){
            if(hit.collider.tag == "Player")
            {
                return true;
            }else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
        
    }

    public Transform GetClosestCover()
    {
        coverPoints = GameObject.FindGameObjectsWithTag("HidePoint");
        Camera cam = enemyToChase.GetComponentInChildren<Camera>();
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        float dist = 1000000000;
        Transform coverSpot = null;
        GameObject coverPoint;



        foreach (GameObject point in coverPoints)
        {
           
            float distTemp = Vector3.Distance(point.transform.position, ai.transform.position);
           
            RaycastHit hit;
            // Calculate Ray direction
            Vector3 direction = cam.transform.position - point.transform.position;
            if (Physics.Raycast(point.transform.position, direction, out hit) && dist > distTemp)
            {
                if (hit.collider.tag == "wall") //hit something else before the camera
                {
                    dist = distTemp;
                    coverPoint = point;
                    coverSpot = coverPoint.transform;
                }
            }

        }

        if(coverSpot != null)
        {
            return coverSpot;
        }
        else
        {
            print("All cover points visible.");
            coverSpot = coverPoints[0].transform;
            return coverSpot;
        }

        
    }

    public void ChangeColor(Color color)
    {
        foreach(Renderer r in childrenRend)
        {
            foreach(Material m in r.materials)
            {
                m.color = color;
            }
        }
    }

    public void Damaged()
    {
        health -= (int) Random.Range(10f,70f);
        
        if(health < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            Damaged();
        }
    }

    public bool CheckIfInRange(string tag)
    {
        enemies = GameObject.FindGameObjectsWithTag(tag);
        if (enemies != null)
        {
            foreach (GameObject g in enemies)
            {
                if(Vector3.Distance(g.transform.position, transform.position) < detectionRange)
                {
                    enemyToChase = g;
                    return true;
                }
            }
        }
        return false;
    }
	void Start () {
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        hidePoints = GameObject.FindGameObjectsWithTag("HidePoint");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        childrenRend = GetComponentsInChildren<Renderer>();
        SetState(new PatrolState(this));
	}
	
	void Update () {
        currentState.CheckTransitions();
        currentState.Act();
        healthText.text = health.ToString();
        healthText.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        healthText.transform.Rotate(0,180,0);

        
	}
    public void SetState(State state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "AI agent in state " + state.GetType().Name;

        
        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void Heal()
    {
        health += 0.01;
    }

    public void StartTimer(float timerLength)
    {
        startTime = Time.time;
        maxTime = Time.time + timerLength;
    }

    public bool CheckTime()
    {
        if(Time.time > maxTime)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public bool PlayerCanSee()
    {
        RaycastHit hit;
        Vector3 direction = enemyToChase.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit) && hit.collider.tag == "Player")
        {
            return true;
        }else
        {
            return false;
        }
    }
}
