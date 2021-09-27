using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC_ClassBased : Enemy     
{
    [SerializeField]
    private string currentStateName;
    private EnemyState currentState;

    public NPC_EnemyAttackState attackState = new NPC_EnemyAttackState();
    public NPC_EnemyIdleState idleState = new NPC_EnemyIdleState();

    public NavMeshAgent navAgent;

    public Renderer mesh;

    [HideInInspector]
    public GameObject attackTarget;

    public GameObject playerRef;

    public Transform[] patrolPoint;

    public int patrolPointCount;

    public float triggerRange;

    public float meleeRange;

    public EnemyData enemyData;

    public Animation attackAnim;

    public Animator anim;

    private void OnEnable()
    {
        currentState = idleState;
        patrolPointCount = 0;
        Init();
    }


    private void Update()
    {
        currentState = currentState.DoState(this);
    }
  
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }    

    protected override void Init()
    {
        health = enemyData.health;
        damage = enemyData.damage;
        movementSpeed = enemyData.movementSpeed;
        enemyType = enemyData.type;
    }

    protected override void setHealth(int newHealth)
    {
        health = newHealth;
    }

    protected override void setDamage(int newDamage)
    {
        damage = newDamage;
    }

    protected override void setSpeed(int newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public override int getHealth()
    {
        return health;
    }

    public override int getDamage()
    {
        return damage;
    }

    public override int getSpeed()
    {
        return movementSpeed;
    }

    public override void takeDamage(int damageAmount)
    {
        int newHealth = health - damageAmount;
        setHealth(newHealth);
    }

    public override void gotSlow(int slowAmount)
    {
        int newMovementSpeed = movementSpeed - slowAmount;
        setSpeed(slowAmount);
    }
}
