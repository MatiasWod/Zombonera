using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
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
                var bullet = Instantiate(BulletPrefab, transform.position + Random.insideUnitSphere * 1, transform.rotation);
                bullet.GetComponent<Bullet>().SetOwner(this);
            }

            _currentShotCooldown = ShotCooldown;
            _currentBulletCount--;
        }
    }

    public override void Reload() => base.Reload();
}
