using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;


    public class NPC_EnemyIdleState : EnemyState
    {
        float DelayCounter;
        Vector3 PatrolLocation;
        Vector3 GeneratedPoint;
        IdleState curState;
        IAstarAI ai;
        Vector3 targetPos = new Vector3(10,0,10);

        enum IdleState
        {
            move,
            generating,
            stop
        };

        public void OnEnter(NPC_ClassBased npc)
        {
            DelayCounter = npc.getpatrolDelay();
            npc.navAgent = npc.GetComponent<NavMeshAgent>();
            npc.anim = npc.GetComponentInChildren<Animator>();
            ai = npc.ai;
            if (ai != null) ai.onSearchPath += Update;


    }

        public void Update()
        {
            if ( ai != null) 
                ai.destination = targetPos;
        }

        public void DoState(NPC_ClassBased npc)
        {
            //Debug.Log("Idle");

            //Patrol(npc);

            switch (curState)
            {
                case IdleState.move:
                    if (CountDown())
                    {
                        curState = IdleState.stop;
                        DelayCounter = 5;
                    }
                    else
                    {
                        MoveFreely(npc);
                        npc.anim.SetBool("Moving", true);
                    }
                    break;

                case IdleState.generating:
                    GeneratedPoint = GenerateRandomVelocity();
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(npc.transform.position + GeneratedPoint * 10, out hit, Vector3.Distance(npc.transform.position + GeneratedPoint, npc.transform.position), NavMesh.AllAreas))
                    {
                        curState = IdleState.move;
                    }
                    break;

                case IdleState.stop:
                    if (CountDown())
                    {
                        curState = IdleState.generating;
                        DelayCounter = 5;
                    }
                    npc.anim.SetBool("Moving", false);
                    break;
            }

            /*if (Vector3.Distance(npc.playerRef.transform.position, npc.transform.position + npc.trOffset) < npc.triggerRange)
            {
                npc.navAgent.ResetPath();
                npc.ChangeState(npc.attackState);
            }*/

            if (npc.FindVisibleTargets())
            {
                npc.navAgent.ResetPath();
                npc.ChangeState(npc.attackState);
            }
        }

        void MoveFreely(NPC_ClassBased npc)
        {
            //Debug.Log(GenerateRandomVelocity());
            //npc.transform.position += GeneratedPoint * Time.deltaTime;
            //npc.transform.rotation = Quaternion.Lerp(npc.transform.rotation, Quaternion.LookRotation(GeneratedPoint), Time.deltaTime);
        }

        void Patrol(NPC_ClassBased npc)
        {

            if (npc.patrolPoint[npc.patrolPointCounter] != null)
            {
                float dist = npc.navAgent.remainingDistance;
                if (npc.navAgent != null)
                {
                    if (dist != Mathf.Infinity && npc.navAgent.pathStatus == NavMeshPathStatus.PathComplete && npc.navAgent.remainingDistance == 0)
                    {
                        if (CountDown())
                        {
                            //npc.patrolPoint[npc.patrolPointCounter].ExitVector(PatrolLocation);
                            npc.patrolPointCounter = (npc.patrolPointCounter + 1) % npc.patrolPoint.Count;
                            DelayCounter = npc.getpatrolDelay();
                            //PatrolLocation = npc.patrolPoint[npc.patrolPointCounter].AvailablePoint();
                            PatrolLocation = npc.RandomMovePosition();

                        }
                        npc.anim.SetBool("Moving", false);
                    }
                    else
                    {
                        npc.anim.SetBool("Moving", true);
                    }
                    //npc.navAgent.SetDestination(npc.patrolPoint[npc.patrolPointCounter].GenerateRandomPoint());
                    /*if(PatrolLocation != Vector3.zero)
                    {
                        npc.navAgent.SetDestination(PatrolLocation);
                    }
                    else
                    {
                        npc.patrolPointCounter = (npc.patrolPointCounter + 1) % npc.patrolPoint.Count;
                        PatrolLocation = npc.patrolPoint[npc.patrolPointCounter].AvailablePoint();
                    }*/
                    npc.navAgent.SetDestination(PatrolLocation);

                }

            }
            else
            {
                Debug.LogWarning("Patrol point empty !!!");
            }
        }

        Vector3 GenerateRandomVelocity()
        {
            Vector3 GeneratedVelocity = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
            return GeneratedVelocity;
        }

        bool CountDown()
        {
            DelayCounter -= Time.deltaTime;
            if (DelayCounter <= 0)
            {
                return true;
            }
            return false;
        }

        public void OnExit()
        {

        }
    }


