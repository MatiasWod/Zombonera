using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : Bullet
{
    public override void Travel() => transform.Translate(transform.forward * (Time.deltaTime * Speed));
}
