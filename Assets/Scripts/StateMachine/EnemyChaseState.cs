using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    public void Enter(EnemyController e)
    {
        Debug.Log("Chase Enter");
    }

    public void Excute(EnemyController e)
    {
        Debug.Log("Chase Excute");
        ChasePlayer(e);

        if (IsInAttackRange(e))
        {
            e.SwitchState(e.attackState);
            return;
        }

        if (IsOutOfChaseRange(e))
        {
            e.SwitchState(e.patrolState);
            return;
        }
    }

    public void Exit(EnemyController e)
    {
        Debug.Log("Chase Exit");
    }

    private void ChasePlayer(EnemyController e)
    {
        Vector3 targetPos = e.GetPlayerPos();
        e.MoveTo((targetPos - e.transform.position).normalized );
    }

    private bool IsInAttackRange(EnemyController e)
    {
        return Vector3.Distance(e.transform.position, e.GetPlayerPos()) <= e.GetRangeAttack();
    }

    private bool IsOutOfChaseRange(EnemyController e)
    {
        return Vector3.Distance(e.transform.position, e.GetPlayerPos()) > e.GetRangeChase();
    }
}
