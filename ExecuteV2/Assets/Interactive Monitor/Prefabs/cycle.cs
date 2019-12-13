using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cycle : MonoBehaviour
{
    int[] numberCycle = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int arrayPos = 0;
    GameObject player;
    public Text slotNum;
    public Text slotNum2;
    public Text slotNum3;
    public Text slotNum4;
   
    public float clickCounter = 0;
    
    public float frameCount = 0;
    public string passcode ="2222";
    private float speed = 1;

    public bool isUnlocked;

    void Start()
    {
        

    }

    public void TaskOnClick()
    {
        clickCounter ++;
        

        

        if(clickCounter == 5)
        {
           
            checkPasscode();
        }

        if (clickCounter == 6)
        {
            clickCounter = 0;
        }

    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(gameObject.transform.position,player.transform.position) < 3)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isUnlocked)
            {
                TaskOnClick();
            }
        }
           

        if(frameCount == 12/speed)
        {
            if (clickCounter == 1)
            {
                slotNum.text = numberCycle[arrayPos].ToString();
                slotNum2.text = numberCycle[arrayPos].ToString();
                slotNum3.text = numberCycle[arrayPos].ToString();
                slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }

                speed = 2;
      
            }
            if (clickCounter == 2)
            {
                //slotNum.text = numberCycle[arrayPos].ToString();
                slotNum2.text = numberCycle[arrayPos].ToString();
                slotNum3.text = numberCycle[arrayPos].ToString();
                slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }
                speed = 3;
            }
            if (clickCounter == 3)
            {
                //slotNum.text = numberCycle[arrayPos].ToString();
                //slotNum2.text = numberCycle[arrayPos].ToString();
                slotNum3.text = numberCycle[arrayPos].ToString();
                slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }
                speed = 4;
            }
            if (clickCounter == 4)
            {
                //slotNum.text = numberCycle[arrayPos].ToString();
                //slotNum2.text = numberCycle[arrayPos].ToString();
                //slotNum3.text = numberCycle[arrayPos].ToString();
                slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }
            }
            if (clickCounter == 5)
            {
                //slotNum.text = numberCycle[arrayPos].ToString();
                //slotNum2.text = numberCycle[arrayPos].ToString();
                //slotNum3.text = numberCycle[arrayPos].ToString();
                //slotNum4.text = numberCycle[arrayPos].ToString();
                if (arrayPos >= numberCycle.Length - 1)
                {
                    arrayPos = 0;
                }
                else
                {
                    arrayPos += 1;
                }
            }
        }
        if (clickCounter == 0)
        {
            
            slotNum.text = numberCycle[Random.Range(0,9)].ToString();
            slotNum2.text = numberCycle[Random.Range(0, 9)].ToString();
            slotNum3.text = numberCycle[Random.Range(0, 9)].ToString();
            slotNum4.text = numberCycle[Random.Range(0, 9)].ToString();
        }

        if (frameCount == 13)
        {
            frameCount = 0;
        }



        if (speed == 5)
        {
            speed = 1;
        }

        frameCount++;

    }

    void stopFirstNum()
    {
        //slotNum.text = numberCycle[arrayPos].ToString();
    }

    public void checkPasscode()
    {
        string s = slotNum.text + slotNum2.text + slotNum3.text + slotNum4.text;
        if(s == passcode)
        {
            Debug.Log("Correct");
            isUnlocked = true;
        }
        
    }

}
