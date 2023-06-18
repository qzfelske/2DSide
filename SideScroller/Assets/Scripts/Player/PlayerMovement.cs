using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private bool grounded;
    private bool colliding;
    private int collisionDir = 0; //1 = left & 2 = right

    public float playerSpeed;
    public float playerJumpPower;

    void Start()
    {
        
    }

    void Update()
    {
        if (grounded)
        {
            if (!(colliding && collisionDir == 1 && Input.GetAxis("Horizontal") < 0) && !(colliding && collisionDir == 2 && Input.GetAxis("Horizontal") > 0))
            {
                transform.position += new Vector3(Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime, 0);
            }
        }


        //TODO: dont stop if colliding with slope or hill etc
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && !grounded) { grounded = true; }
        
        if (!colliding) 
        { 
            colliding = true; 

            if (transform.position.x > collision.transform.position.x) { collisionDir = 1; }
            else if (transform.position.x < collision.transform.position.x) { collisionDir = 2; }
            else { collisionDir = 0; }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && grounded) { grounded = false; }

        if (colliding) 
        { 
            colliding = false;
            collisionDir = 0;
        }
    }
}
