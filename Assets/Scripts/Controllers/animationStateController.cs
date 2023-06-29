using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _characterLastPosition;
    private float _attackRange;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _attackRange = 5;
        _characterLastPosition = GameObject.Find("Character").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        var CharacterObject = GameObject.Find("Character");
        if(CharacterObject != null)
            _characterLastPosition = CharacterObject.transform.position;

        bool isAttacking = _animator.GetBool("isAttacking");
        bool inAttackingRange = (Vector3.Distance(transform.position, _characterLastPosition) <= _attackRange);
        if (!isAttacking && inAttackingRange)
        {
            _animator.SetBool("isAttacking", true);
        }
        else if (isAttacking && !inAttackingRange)
        {
            _animator.SetBool("isAttacking",false);
        }
    }
}

