using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEscapeState : IEnemyState
{
    [SerializeField] private float escapeDistance = 10f; 
    [SerializeField] private float healthThreshold = 0.5f; 

    public void Enter(EnemyController e)
    {
        Debug.Log("Escape Enter");
    }

    public void Excute(EnemyController e)
    {
        Debug.Log("Escape Excute");

        Vector3 target = e.GetPlayerPos();
        Vector3 escapeDirection = e.transform.position - target;
        escapeDirection.Normalize(); 

        e.MoveTo(e.transform.position + escapeDirection * 4f * Time.deltaTime);

        if (Vector3.Distance(e.transform.position, e.GetPlayerPos()) > escapeDistance || e.GetEHealth().GetHealthPercent() > healthThreshold)
        {
            e.SwitchState(e.patrolState);
        }
    }

    public void Exit(EnemyController e)
    {
        Debug.Log("Escape Exit");
    }
}
