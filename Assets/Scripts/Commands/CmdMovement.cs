using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMovement : ICommand
{
    private IMoveablle _moveablle;
    private Vector3 _direction;
    //private float _speed;

    public CmdMovement(IMoveablle moveablle, Vector3 direction)
    {
        //_speed = speed;
        _moveablle = moveablle;
        _direction = direction;
    }

    public void Execute() => _moveablle.Move(_direction);
}
