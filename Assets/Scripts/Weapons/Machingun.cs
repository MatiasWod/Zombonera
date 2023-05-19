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
            //Nescesary because ak textures are fliped and causes problem with the forward vector
            Quaternion aux = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(270, 90, 0)); 

            Debug.Log(transform.rotation.eulerAngles - new Vector3(270, 90, 0));
            m_shootingSound.Play();
            for (int i = 0; i < _shotCount; i++)
            {
                var bullet = Instantiate(BulletPrefab, transform.position + (aux * Vector3.forward ) * i, aux);
                bullet.GetComponent<Bullet>().SetOwner(this);
                
            }
            _currentBulletCount--;
            _currentShotCooldown = ShotCooldown;
        }
    }

    public override void Reload() => base.Reload();
}
