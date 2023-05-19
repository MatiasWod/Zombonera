using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machingun : Gun
{
    [SerializeField] private int _shotCount = 5;

    private void Update()
    {
        if (_currentShotCooldown >= 0) _currentShotCooldown -= Time.deltaTime;
    }

    public override void Attack()
    {
        if (_currentShotCooldown <= 0 && _currentBulletCount > 0)
        {
            m_shootingSound.Play();
            for (int i = 0; i < _shotCount; i++)
            {
                var bullet = Instantiate(BulletPrefab, transform.position + Vector3.forward * i * 0.5f, transform.rotation);
                bullet.GetComponent<Bullet>().SetOwner(this);
                
            }
            _currentBulletCount--;
            _currentShotCooldown = ShotCooldown;
        }
    }

    public override void Reload() => base.Reload();
}
