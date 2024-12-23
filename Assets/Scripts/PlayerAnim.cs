using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public static PlayerAnim instance;
    private Animator playerAnim;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        playerAnim = GetComponent<Animator>();
    }

    //public void SetIsWalking(bool isWalking)
    //{
    //    playerAnim.SetBool("isWalking", isWalking);
    //}

    public void SetXVelocity(float xVec)
    {
        playerAnim.SetFloat("xVelocity", xVec);
    }
    
    public void SetTriggerAttack()
    {
        playerAnim.SetTrigger("attack");
    }
    public void SetIsGrounded(bool isGrounded)
    {
        playerAnim.SetBool("isGrounded", isGrounded);
    }
    
    public void SetIsWallSliding(bool isWallSliding)
    {
        playerAnim.SetBool("isWallSliding", isWallSliding);
    }

    public void SetCanClimbLedge(bool canClimbLedge)
    {
        playerAnim.SetBool("canClimbLedge", canClimbLedge);
    }
    public void SetYVelocity(float yVelocity)
    {
        playerAnim.SetFloat("yVelocity", yVelocity);
    }
}
