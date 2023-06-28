using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveablle
{
    #region IMOVEABLE_PROPERTIES
    public float Speed => GetComponent<Actor>().Stats.MovementSpeed;
    [SerializeField] public Rigidbody _character;
    #endregion

    #region IMOVEABLE_METHODS
    public void Move(Vector3 direction)
    {
        if(direction == Vector3.zero)
        {
            _character.velocity = Vector3.zero;
        }
        if(Vector3.forward == direction )
        {
            _character.velocity = transform.forward * ( Speed);
        }
        if(-Vector3.forward == direction)
        {
            _character.velocity = -transform.forward * ( Speed);
        }
        if (Vector3.right == direction)
        {
            _character.velocity = transform.right * ( Speed);
        }
        if (-Vector3.right == direction)
        {
            _character.velocity = -transform.right * ( Speed);
        }
    }
    #endregion
}
