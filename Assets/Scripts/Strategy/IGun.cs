using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    GameObject BulletPrefab { get; }
    float BulletSpeed { get; }
    int Damage { get; }
    int MaxBulletCount { get; }
    
    void Attack();
    void Reload();
}
