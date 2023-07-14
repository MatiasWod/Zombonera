using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    #region PRIVATE_PROPERTEIS
    [SerializeField] private IGun _owner;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _speed = 1000;
    [SerializeField] private float _lifetime = 2;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private List<int> _layerMasks;
    #endregion

    #region I_BULLET_PROPERTIES
    public IGun Owner => _owner;
    public int Damage => _damage;
    public float Speed => _speed;
    public float LifeTime => _lifetime;

    public int rng;

    public Vector3 direction => _direction;
    #endregion

    #region I_BULLET_METHODS
    public virtual void Travel() => transform.position += (transform.forward * (Time.deltaTime * Speed)); //Funciona la pistola nomas
 

    public void OnCollisionEnter(Collision collision)
    {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            rng = Random.Range(0,10);
            if (damagable != null && rng != 1)
            {
                EventQueueManager.instance.AddEvent(new CmdApplyDamage(damagable, _damage));
            }
            else if (damagable != null && rng == 1)
            {
                EventQueueManager.instance.AddEvent(new CmdApplyDamage(damagable, _damage + 20));
            }
            Destroy(this.gameObject);
       
    }
    #endregion

    #region UNITY_EVENTS
    void Start() 
    {
        // Rename a BulletDamage
        _damage = _owner.Damage;
        // Agregar BulletSpeed en stats
        _speed = _owner.BulletSpeed;

    }
    
    void Update()
    {
        Travel();

        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0) Destroy(this.gameObject);
    }
    #endregion

    public void SetOwner(IGun owner) => _owner = owner;
}