using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool m_FacingRight = true;
    public Rigidbody rb;

    Vector3 movement;

   
  
    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.z = Input.GetAxisRaw("Vertical");
        if (movement.x > 0 && !m_FacingRight)
        {
            Flip();
        }

        else if (movement.x < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);       
    }
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        float scaleX = this.transform.localScale.x*-1;        
        this.transform.localScale = new Vector3(scaleX, this.transform.localScale.y,this.transform.localScale.z);        
    }

    
}
