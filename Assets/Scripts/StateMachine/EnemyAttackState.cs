using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    [SerializeField] private float coolDown = 0.8f;

    private float timeNextAttack = 0;
    public void Enter(EnemyController e)
    {
        Debug.Log("AttackState Enter");
    }

    public void Excute(EnemyController e)
    {
        Debug.Log("AttackState Excute");
        if (Time.time > timeNextAttack)
        {
            if (e.GetPlayerPos().x > e.transform.position.x)
            {
                if(!e.isFacingRight) e.Flip();
            }
            else if(e.GetPlayerPos().x < e.transform.position.x)
            {
                if (e.isFacingRight) e.Flip();
            }
            e.SetTriggerAttack();
            Collider2D[] hits = Physics2D.OverlapCircleAll(e.GetAttackPoint().position, e.GetRangeAttack(), e.GetLayerMask());

            foreach (Collider2D hit in hits)
            {
                hit.GetComponent<EnemyHealth>().TakeDame(e.GetAttackDamage());
            }

            timeNextAttack = Time.time + coolDown;
        }

        if (IsOutRangeAttack(e))
        {
            e.SwitchState(e.chaseState);
            return;
        }
    }

    public bool IsOutRangeAttack(EnemyController e)
    {
        return Vector3.Distance(e.transform.position, e.GetPlayerPos()) > e.GetRangeAttack();
    }

    public void Exit(EnemyController e)
    {
        Debug.Log("AttackState Excute");
    }
}
