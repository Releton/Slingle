using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public float speed;
    public float jumping;
    private Rigidbody Rb;
    private void Awake()
    {
        Rb = this.GetComponent<Rigidbody>();
    }
    private bool didJump = false;
    private void Update()
    { 
      if(IsGrounded() && !didJump)
        {
            Jump();
        }  
      else if(IsGrounded() && didJump)
        {
            didJump = false;
        }
    }

    public void SetJumping(float jump)
    {
        jumping = jump;
    }

    private void Jump()
    {
        Rb.AddForce(jumping * Vector3.up);
        didJump = true;
    }

    public void SetSpeed(float speed)
    {
        Rb.velocity = speed * (SlingScript.posn.position - transform.position).normalized;
        this.speed = speed;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(this.transform.position, -Vector3.up, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndWall")
        {
            SlingScript.life -= 1;
            Debug.Log(SlingScript.life);
            Destroy(gameObject);
        }
    }

}
