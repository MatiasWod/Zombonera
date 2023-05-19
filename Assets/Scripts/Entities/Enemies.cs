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
    
    private NavMeshAgent _navMeshAgent;               
    private int _damage;
    private float _attackRange;
    private float _speedRun;                       
    private Vector3 _characterLastPosition;                       

    private EnemyBehaviourStates _state;
    private float _timeleavingCharacterAlone = 2;  
    private float _timeleavingCharacterAloneReset = 2; 


    void Start()
    {    
        _state = EnemyBehaviourStates.chasing;

        _speedRun = 20;
        _damage = 20;
        _attackRange = 5;
        _characterLastPosition = GameObject.Find("Character").transform.position;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = _speedRun;            
        _navMeshAgent.SetDestination(_characterLastPosition);    
    }
 
    private void Update()
    {
        if(!_navMeshAgent.isStopped){
            if (_state == EnemyBehaviourStates.chasing)
            {
                var CharacterObject = GameObject.Find("Character");
                if(CharacterObject != null){
                    _characterLastPosition = CharacterObject.transform.position;
                    
                    if (Vector3.Distance(transform.position, _characterLastPosition) <= _attackRange) 
                    {
                        IDamagable damagable = CharacterObject.GetComponent<IDamagable>(); 
                        EventQueueManager.instance.AddEvent(new CmdApplyDamage(damagable, _damage));
                        LeaveCharacterAloneForAMoment();
                    }
                    else
                    {
                        Chasing();
                    }
                }
            }
            else if(_state == EnemyBehaviourStates.leavingCharacterAlone)
            {
                
                _timeleavingCharacterAlone -= Time.deltaTime;
                if (_timeleavingCharacterAlone <= 0)
                {
                    _timeleavingCharacterAlone = _timeleavingCharacterAloneReset;
                    _state = EnemyBehaviourStates.chasing;
                }
            }
        }
    }
 
    private void Chasing()
    {
        Move(_speedRun);
        _navMeshAgent.SetDestination(_characterLastPosition);          
    }
    
    void Move(float speed)
    {
        _navMeshAgent.speed = speed;
    }
    
    public void Stop()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0;
    }

    public void LeaveCharacterAloneForAMoment()
    {
        _state = EnemyBehaviourStates.leavingCharacterAlone;
    }
}