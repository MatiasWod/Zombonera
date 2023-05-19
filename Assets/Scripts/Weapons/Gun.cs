using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    [SerializeField] protected GunStats _stats;

    #region GUN_PROPERTIES
    [SerializeField] protected int _currentBulletCount;
    [SerializeField] protected float _currentShotCooldown;
    #endregion

    #region I_GUN_PROPERTIES
    public GameObject BulletPrefab => _stats.BulletPrefab;
    public float BulletSpeed => _stats.BulletSpeed;
    public int Damage => _stats.Damage;
    public int MaxBulletCount => _stats.MaxBulletCount;
    public float ShotCooldown => _stats.ShotCooldown;

    public AudioSource m_shootingSound;
    
    #endregion

    #region UNITY_EVENTS
    private void Start()
    {
        m_shootingSound = GetComponent<AudioSource>();
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
            Debug.Log(transform.rotation.eulerAngles);
            m_shootingSound.Play();
            var bullet = Instantiate(BulletPrefab, transform.position + transform.forward * 2 , transform.rotation);
            bullet.GetComponent<Bullet>().SetOwner(this);
            _currentShotCooldown = ShotCooldown;
            _currentBulletCount--;
        }
    }

    public virtual void Reload() => _currentBulletCount = MaxBulletCount;
    #endregion
}
