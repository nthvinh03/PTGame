using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float damageAttack;
    [SerializeField] private float rangeAttack;
    [SerializeField] private float coolDown;
    [SerializeField] private LayerMask whatIsEnemy;

    private float timeNextAttack;
    public Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        attackPoint = GameObject.Find("AttackPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (Time.time > timeNextAttack)
        {
            Debug.Log("Player attack");
            PlayerAnim.instance.SetTriggerAttack();
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, rangeAttack, whatIsEnemy);
            
            foreach (Collider2D hit in hits)
            {
                Debug.Log(hit.name);
                 hit.GetComponent<EnemyHealth>().TakeDame(damageAttack);
            }
            timeNextAttack = Time.time + coolDown;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, rangeAttack);
    }
}
