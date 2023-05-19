using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    enum EnemyBehaviourStates
    {
        chasing,
        leavingCharacterAlone,
    }
    
    private NavMeshAgent navMeshAgent;               //  Nav mesh agent component
    private float speedRun;                       //  Running speed
    private Vector3 characterLastPosition;                       //  Last position of the Character

    private EnemyBehaviourStates state;
    private float timeleavingCharacterAlone = 3;  // tres segundos
    
    void Start()
    {    
        state = EnemyBehaviourStates.chasing;
        speedRun = 20;
        characterLastPosition = GameObject.Find("Character").transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedRun;             //  Set the navemesh speed with the normal speed of the enemy
        navMeshAgent.SetDestination(characterLastPosition);    //  Set the destination to the first waypoint
    }
 
    private void Update()
    {
        if(!navMeshAgent.isStopped){
            if (state == EnemyBehaviourStates.chasing)
            {
                var CharacterObject = GameObject.Find("Character");
                if(CharacterObject != null){
                    characterLastPosition = CharacterObject.transform.position;
                    
                    if (Vector3.Distance(transform.position, characterLastPosition) <= 5) //Esto es el rango, frena un toque cuanto toca la pelota
                    {
                        LeaveCharacterAloneForAMoment();
                    }
                    else
                    {
                        Chasing();
                    }
                }
            }
            else if(state == EnemyBehaviourStates.leavingCharacterAlone)
            {
                // Solo estaremos en este estado por un tiempo corto para darle al Character oportunidad de recuperarse
                timeleavingCharacterAlone -= Time.deltaTime;
                if (timeleavingCharacterAlone <= 0)
                {
                    timeleavingCharacterAlone = 1;
                    state = EnemyBehaviourStates.chasing;
                }
            }
        }
    }
 
    private void Chasing()
    {
        Move(speedRun);
        navMeshAgent.SetDestination(characterLastPosition);          //  set the destination of the enemy to the Character location
    }
    
    void Move(float speed)
    {
        navMeshAgent.speed = speed;
    }
    
    public void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void LeaveCharacterAloneForAMoment()
    {
        state = EnemyBehaviourStates.leavingCharacterAlone;
    }
}