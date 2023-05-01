using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    [SerializeField] protected GunStats _stats;

    #region GUN_PROPERTIES
    [SerializeField] private int _currentBulletCount;
    #endregion

    #region I_GUN_PROPERTIES
    public GameObject BulletPrefab => _stats.BulletPrefab;
    public int Damage => _stats.Damage;
    public int MaxBulletCount => _stats.MaxBulletCount;
    public float ShotCooldown => _stats.ShotCooldown;
    [SerializeField] protected float _currentShotCooldown;
    #endregion

    #region UNITY_EVENTS
    private void Start()
    {
        _currentBulletCount = MaxBulletCount;
        _currentShotCooldown = ShotCooldown;    
    }

    private void Update()
    {
        if (_currentShotCooldown >= 0) _currentShotCooldown -= Time.deltaTime;
    }
    #endregion

    #region I_GUN_PROPERTIES
    public virtual void Attack()
    {
        if (_currentShotCooldown <= 0)
        {
            var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().SetOwner(this);
            _currentShotCooldown = ShotCooldown;
        }
    }

    public virtual void Reload() => _currentBulletCount = MaxBulletCount;
    #endregion
}
