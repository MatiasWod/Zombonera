using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machingun : Gun
{
    [SerializeField] private int _shotCount = 5;
    [SerializeField] public GameObject _gunsRotation;
    public GameObject _machingunObject;
    private void Update()
    {
        if (_currentShotCooldown >= 0) _currentShotCooldown -= Time.deltaTime;
    }

    public override void Attack()
    {
        if (_currentShotCooldown <= 0 && _currentBulletCount > 0)
        {
            //Nescesary because ak textures are fliped and causes problem with the forward vector
            Quaternion aux =  _gunsRotation.transform.rotation ;
            /*Debug.Log(aux.eulerAngles);
            Debug.Log(transform.rotation.eulerAngles - new Vector3(270, 90, 0));*/
            m_shootingSound.Play();
            for (int i = 0; i < _shotCount; i++)
            {
                var bullet = Instantiate(BulletPrefab, transform.position + _character.transform.rotation * Vector3.forward * 4 + (aux * Vector3.forward) * i, aux);
                bullet.GetComponent<Bullet>().SetOwner(this);

            }
            _currentBulletCount--;
            _currentShotCooldown = ShotCooldown;
            hudBullet(_currentBulletCount, MaxBulletCount);
            _machingunObject.GetComponent<Animator>().Play("MachingunRecoil");
        }
    }

    public override void Reload() => base.Reload();
}