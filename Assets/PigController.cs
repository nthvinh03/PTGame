using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float speed;
    public float rangeAttack;
    public float rangeChase;
    public float damage;

    public Transform AttackPoint;
    public Transform player;

    private bool isFacingRight = true;
    public float timeChangeDirection = 2f;
    private float timer = 0;
    private int direction = 1;

    private Rigidbody2D rb;
    private Animator anim;

    public LayerMask whatIsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > timeChangeDirection)
        {
            timer = 0;
            FLip();
        }
        direction = isFacingRight == true ? 1 : -1;
       
    }
    private void FixedUpdate()
    {
        PigMove();
        DetectedPlayer();
        AttackPlayer();
    }

    public void PigMove()
    {
        Debug.Log("run");
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    public void FLip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void DetectedPlayer()
    {
        if(Vector3.Distance(player.position, transform.position) <= rangeChase)
        {
            if (player.position.x > transform.position.x)
            {
                if (!isFacingRight)
                {
                    FLip();
                }
                
            }
            else if(player.position.x < transform.position.x)
            {
                if (isFacingRight)
                {
                    FLip();
                }
            }
        }
    }

    void AttackPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) <= rangeAttack)
        {
            anim.SetTrigger("attack");
            Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position,rangeAttack , whatIsPlayer);

            foreach (Collider2D hit in hits)
            {
                hit.GetComponent<EnemyHealth>().TakeDame(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, rangeAttack);
        Gizmos.DrawWireSphere(transform.position, rangeChase);
    }
}
