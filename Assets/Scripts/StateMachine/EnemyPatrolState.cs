using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private float coolDownHeal = 1f;
    private float timeNextHeal = 0;
    private float amountHeal = 5f;
    private float changeDirectionTime = 2f;
    private float timer = 0f;

    public void Enter(EnemyController e)
    {
        Debug.Log("Patrol Enter");

    }

    public void Excute(EnemyController e)
    {
        Debug.Log("Patrol Excute");
        Patrol(e);

        HealOverTime(e);

        CheckPlayerDistance(e);
    }

    public void Exit(EnemyController e)
    {
        Debug.Log("Patrol Exit");
    }

    private void Patrol(EnemyController e)
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            e.Flip();
            timer = 0f; 
        }
        e.MoveTo(e.isFacingRight ? Vector3.right : Vector3.left);

    }

    private void HealOverTime(EnemyController e)
    {
        if (Time.time > timeNextHeal)
        {
            //e.GetEHealth().Heal(amountHeal);
            timeNextHeal = Time.time + coolDownHeal;
        }
    }

    private void CheckPlayerDistance(EnemyController e)
    {
        Vector3 playerPos = e.GetPlayerPos();
        float range = e.GetRangeChase();

        if (Vector3.Distance(e.transform.position, playerPos) < range)
        {
            e.SwitchState(e.chaseState);
        }
    }
}
