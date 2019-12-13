using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public GameObject cam;
    public float speed = 2f, sensitivity = 2f, jumpDistance = 5f;
    float moveFB, moveLR, rotX, rotY, verticalVelocity;
    CharacterController charCon;
    private float lastRot;
    Animator anim;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public int walk = 0;
    

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        charCon = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 8f;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 2f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            fireGun();
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Cursor.visible = true;
           Cursor.lockState = CursorLockMode.None;
            
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)  || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("walk", 1);
        }
        else
        {
            anim.SetInteger("walk", 0);
        }


        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;
       
        //rotY = Mathf.Clamp(rotY, -120f, 60f);
        

        Vector3 movement = new Vector3(moveLR, verticalVelocity, moveFB);
        transform.Rotate(0, rotX, 0);
        cam.transform.Rotate(-rotY, 0, 0);

        Vector3 currentRot = cam.transform.localEulerAngles;
        //currentRot.x = currentRot.x * -1f;
        

        if(currentRot.x > 60f && currentRot.x< 300f && lastRot < 65f)
        {
            currentRot.x = 60f;
        }
        if (currentRot.x > 60f && currentRot.x < 300f && lastRot > 290)
        {
            currentRot.x = 300f;
        }
       
        
        cam.transform.localRotation = Quaternion.Euler(currentRot);


        movement = transform.rotation * movement;
        charCon.Move(movement * Time.deltaTime);

        lastRot = currentRot.x;
        if (charCon.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpDistance;
            }
        }

        
    }

    private void fireGun()
    {
        Ray ray = cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            bulletSpawn.transform.LookAt(hit.point);
        }
            
        
        print("shot");
        GameObject instantiatedProjectile = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        instantiatedProjectile.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.TransformDirection(Vector3.forward * 200);
    }

    private void FixedUpdate()
    {
        if (!charCon.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        else
        {

        }
    }
}
