using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private bool grounded;
    private bool colliding;
    private int collisionDir = 0; //1 = left & 2 = right
    private bool canStep;
    private float max = 0;

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

        if (canStep)
        {
            if (collisionDir == 1) { transform.position += new Vector3(-0.1f, (transform.position.y - max) + 0.05f); }
            else { transform.position += new Vector3(0.1f, (transform.position.y - max) + 0.05f); }
            
            canStep = false;
        }

        //TODO: dont stop if colliding with slope or hill etc
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //TODO: determine grounded not using the tag but where the collision is relative to the player

        if (collision.transform.tag == "Ground" && !grounded) { grounded = true; }
        
        if (!colliding && collision.transform.tag != "Ground") 
        { 
            colliding = true; 

            if (transform.position.x > collision.transform.position.x) { collisionDir = 1; }
            else if (transform.position.x < collision.transform.position.x) { collisionDir = 2; }
            else { collisionDir = 0; }

            max = collision.GetContact(0).point.y;
            for (int i = 1; i < collision.contactCount; i++)
            {
                if (collision.GetContact(i).point.y > max)
                {
                    max = collision.GetContact(i).point.y;
                }

                //Debug.Log("point: " + collision.GetContact(i).point.y);
            }

            //Debug.Log("player y: " + transform.position.y);
            //Debug.Log("max y: " + max);
            //Debug.Log("difference: " + (transform.position.y - max));

            canStep = transform.position.y - max > .35;

            Debug.Log(canStep);
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
