using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAgent : MonoBehaviour
{
    private AIDestinationSetter destinationSetter;
    private AIPath AIPath;

    private float oldPosition;
    private bool moving;
    
    [SerializeField] private int damage = 1;

    [SerializeField] private float TargetDetectionRaycastRadius = 50f;

    private Transform sight;
    public List<GameObject> possibleTargetsAI;
    public GameObject possibleTargetPlayer;
    public List<GameObject> possibleTargetsObstacles;
    
    [SerializeField] private float attackRange = 5f;

    void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        AIPath = GetComponent<AIPath>(); ;
        
        sight = gameObject.gameObject.transform.Find("Sight");
    }

    private void Start()
    {
        oldPosition = transform.position.x;
    }

    private void Update()
    {
        CheckIfMoving();
        CheckForPossibleTargetsInFieldOfView();
    }

    public void CheckIfMoving()
    {
        if (oldPosition != transform.position.x)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        oldPosition = transform.position.x;
    }

    public void CheckForPossibleTargetsInFieldOfView()
    {
        //delete targets from previous frame
        possibleTargetsAI = new List<GameObject>();
        possibleTargetsObstacles = new List<GameObject>();
        possibleTargetPlayer = null;

        //get all colliders near the agent
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, TargetDetectionRaycastRadius);

        //check what team the current AI is on
        if (gameObject.transform.CompareTag("EnemyTeam"))
        {
            //for each detected collider check if in line of sight of agent
            foreach (var detectedCollider in detectedColliders)
            {
                //Debug.Log("Detected Collider" + detectedCollider);
                
                if (detectedCollider.transform.CompareTag("Player"))
                {
                    if (TargetInFieldOfView(detectedCollider.transform))
                    {
                        Debug.Log("Player in FoV");
                        possibleTargetPlayer = detectedCollider.gameObject;

                    }
                }
                if (detectedCollider.transform.CompareTag("PlayerTeam"))
                {

                    if (TargetInFieldOfView(detectedCollider.transform))
                    {
                        if (detectedCollider.gameObject.layer == 7) //Layer 7 = AI
                        {
                            possibleTargetsAI.Add(detectedCollider.gameObject);
                            
                        } else if (detectedCollider.gameObject.layer == 6) //Layer 6 = Obstacle 
                        {
                            possibleTargetsObstacles.Add(detectedCollider.gameObject);
                        }
                    }
                }
            }
        }
        else if (gameObject.transform.CompareTag("PlayerTeam"))
        {
            foreach (var detectedCollider in detectedColliders)
            {
                if (detectedCollider.transform.CompareTag("EnemyTeam"))
                {
                    //Debug.Log("Detected EnemyTeamAI");
                    if (TargetInFieldOfView(detectedCollider.transform))
                    {
                        if (detectedCollider.gameObject.layer == 7) //Layer 7 = AI
                        {
                            //Debug.Log("PlayerTeamAI in FoV");
                            possibleTargetsAI.Add(detectedCollider.gameObject);
                            
                        } else if (detectedCollider.gameObject.layer == 6) //Layer 6 = Obstacle 
                        {
                            possibleTargetsObstacles.Add(detectedCollider.gameObject);
                        }
                    }
                }
            }
        }
    }

    //Check if target is in line of sight (not behind object)
    public bool TargetInFieldOfView(Transform target)
    {
        sight.transform.LookAt(target);
        //Raycast returns true if ray intersects with a Collider, otherwise false (first collider that was hit)
        RaycastHit hit;
        Physics.Raycast(sight.transform.position, sight.transform.forward, out hit, Mathf.Infinity);

        if (hit.transform == target)
        {
            return true;
        }
        return false;
    }


    public void MoveToLocation(Vector3 location)
    {
        AIPath.isStopped = false;
        AIPath.destination = location;
        
    }
    
    public void MoveToTarget(GameObject target)
    {
        AIPath.isStopped = false;
        if (target != null)
        {
            destinationSetter.target = target.transform;
        }
        else
        {
            
            destinationSetter.target = null;
        }
        
    }
    
    public bool AtDestination()
    {
        if (AIPath.reachedEndOfPath)
        {
            return true;
        }

        return false;
    }

    public Vector3 PickRandomLocationInRange(float radius)
    {
        Vector3 newPos = new Vector3();
        newPos.x = Random.Range(transform.position.x - radius, transform.position.x + radius);
        newPos.y = Random.Range(transform.position.y - radius, transform.position.y + radius);
        newPos.z = transform.position.z;

        return newPos;
    }

    //Needed for GOAP
    public bool IsMoving()
    {
        return moving;
    }
    

    //Needed for GOAP
    public bool hasTargetsInFieldOfView()
    {
        if (possibleTargetsAI.Count != 0)
        {
            return true;
        }

        return false;
    }

    //Needed for GOAP
    public bool hasPlayerInFieldOfView()
    {
        if (possibleTargetPlayer != null)
        {
            return true;
        }

        return false;
    }

    public String getAdversayTeam()
    {
        if (gameObject.tag == "PlayerTeam")
        {
            return "EnemyTeam";
        }
        return "PlayerTeam";
    }

    public void DealDamage(GameObject target)
    {
        HitHealth targetHealth = target.GetComponent<HitHealth>();
        
        targetHealth.decreaseHealth(damage);
        
    }
    public void StopFollowingTarget()
    {
        AIPath.isStopped = true;
        
    }

    public bool InAttackRange(GameObject target)
    {
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        
        if (distance < attackRange)
        {
            return true;
        }

        return false;
    }

    
}

