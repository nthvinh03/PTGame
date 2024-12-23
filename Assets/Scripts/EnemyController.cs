using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D enemyRb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange = 0.8f;
    [SerializeField] private float rangeChase;

    public bool isFacingRight = true;

    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyEscapeState escapeState = new EnemyEscapeState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyChaseState chaseState = new EnemyChaseState();

    private IEnemyState currState;

    public Transform attackPoint;
    private Transform playerPos;

    private EnemyHealth eHealth;

    [SerializeField] private LayerMask whatIsPlayer;

    private Animator e_anim;

    private void Awake()
    {
        currState = patrolState;
        currState?.Enter(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.Find("Player").transform;
        
        eHealth = GetComponent<EnemyHealth>();
        e_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currState?.Excute(this);
    }

    public void SwitchState(IEnemyState state)
    {
        currState?.Exit(this);
        currState = state;
        currState?.Enter(this);
    }
    public void MoveTo(Vector3 direction)
    {
        enemyRb.velocity = new Vector2(direction.x * moveSpeed, enemyRb.velocity.y);
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public Vector3 GetPlayerPos()
    {
        return playerPos.position;
    }

    public float GetRangeChase()
    {
        return rangeChase;
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }
    public LayerMask GetLayerMask()
    {
        return whatIsPlayer;
    }

    public float GetRangeAttack()
    {
        return attackRange;
    }

    public Transform GetAttackPoint()
    {
        return attackPoint;
    }

    public void SetTriggerAttack()
    {
        e_anim.SetTrigger("attack");
    }

    public EnemyHealth GetEHealth()
    {
        return this.eHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, rangeChase);
    }
}
