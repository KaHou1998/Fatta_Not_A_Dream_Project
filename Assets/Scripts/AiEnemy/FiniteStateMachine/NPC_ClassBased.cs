using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class NPC_ClassBased : Enemy     
{
    [SerializeField]
    private string currentStateName;
    private EnemyState currentState;
    public EnemyState newState;

    public NPC_EnemyAttackState attackState = new NPC_EnemyAttackState();
    public NPC_EnemyIdleState idleState = new NPC_EnemyIdleState();

    [Header("Component Reference")]
    public NavMeshAgent navAgent;
    public Renderer mesh;
    public GameObject playerRef;
    public List<Waypoint> patrolPoint;
    public Animator anim;
    public Rigidbody rigidBody;
    // Steering Behavior
    //public steering_script seek;

    [HideInInspector]
    public GameObject attackTarget;

    [HideInInspector]
    public int patrolPointCounter;

    [Header("Enemy Settings")]
    public float triggerRange;
    public float meleeRange;
    public EnemyData enemyData;
    public float spaceBetween = 1.5f;
    public Vector3 trOffset;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public float movementRangeMin;
    public float movementRangeMax;
    public float movingSpeed = 2.0f;

    GameObject[] EnemyAI;
    public IAstarAI ai;


    private void OnEnable()
    {
        currentState = idleState;
        newState = currentState;
        patrolPointCounter = 0;
        playerRef = GameObject.Find("Player");
        ai = GetComponent<IAstarAI>();
        Init();
    }

    public void Start()
    {
        EnemyAI = GameObject.FindGameObjectsWithTag("EnemyAi");
        currentState.OnEnter(this);
    }

   public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public bool FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                return true;
            }
        }
        
        return false;
    }

    private void Update()
    {
        //currentState.DoState(this);
        currentState.Update();
        if (newState != currentState)
        {
            //currentState.OnExit();
            newState.OnEnter(this);
            currentState = newState;
        }

        /*foreach(GameObject go in EnemyAI)
        {
            if(go != gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, this.transform.position);
                if(distance <= spaceBetween)
                {
                    Vector3 direction = transform.position - go.transform.position;
                    //transform.Translate(direction * Time.deltaTime);
                }
            }
        }*/
    }

   public Vector3 RandomMovePosition()
    {
        //Vector3 randomMovePosition = new Vector3(Random.Range(this.transform.position.x + movementRangeMin, this.transform.position.x + movementRangeMax),1.036f, Random.Range(this.transform.position.z + movementRangeMin, this.transform.position.z + movementRangeMax));
        Vector3 randomMovePosition = new Vector3(Random.Range(this.transform.position.x - Random.Range(movementRangeMin, movementRangeMax), this.transform.position.x + Random.Range(movementRangeMin, movementRangeMax)),
                                                  1.036f,
                                                 Random.Range(this.transform.position.z - Random.Range(movementRangeMin, movementRangeMax), this.transform.position.z + Random.Range(movementRangeMin, movementRangeMax)));
        return randomMovePosition;
    }
  
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + trOffset, triggerRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, movementRangeMin);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, movementRangeMax);
    }    

    protected override void Init()
    {
        health = enemyData.health;
        damage = enemyData.damage;
        movementSpeed = enemyData.movementSpeed;
        enemyType = enemyData.type;
        attackDelay = enemyData.attackDelay;
        patrolDelay = enemyData.patrolDelay;
        //navAgent.speed = movementSpeed;
    }

    protected override void setAttackDelay(float newAttackDelay)
    {
        attackDelay = newAttackDelay;
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

    public override float getAttackDelay()
    {
        return attackDelay;
    }

    public override float getpatrolDelay()
    {
        return patrolDelay;
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

    public void ChangeState(EnemyState _newState)
    {
        newState = _newState;
    }
}
