
using UnityEngine;

public interface IEnemyState
{
    public void Enter(EnemyController e);
    public void Excute(EnemyController e);
    public void Exit(EnemyController e);
}
