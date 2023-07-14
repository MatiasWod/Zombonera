using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Actor))]
public class LifeController : MonoBehaviour, IDamagable
{
    #region I_DAMAGABLE_PROPERTIES
    public float CurrentLife => _currentLife;
    private Animator _animator;
    private IEnemy _movement;
    [SerializeField] private float _currentLife;
    [SerializeField] private Slider healthBar;
    private bool _isAnimatorNotNull;
    public float MaxLife => GetComponent<Actor>().Stats.MaxLife;
    [SerializeField] private ParticleSystem collisionParticles;
    #endregion

    #region UNITY_EVENTS
    void Start()
    {
        _isAnimatorNotNull = _animator != null;
        _currentLife = MaxLife;
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Enemies>();
        if(_movement == null)
        {
            _movement = GetComponent<EnemyAi>();
        }
    }
    #endregion

    private void Update()
    {
        if (healthBar != null)
        {
            healthBar.value = _currentLife / MaxLife;
        }
    }

    #region I_DAMAGABLE_METHODS
    public void TakeDamage(int damage)
    {
        _currentLife -= damage;
        if (collisionParticles != null)
        {
            Instantiate(collisionParticles, transform.position, transform.rotation);
        }

        if (IsDead())
        {
            if (name == "Character")
            {
                GameManager.instance.GameOver();
            }
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (_currentLife + amount >= MaxLife)
        {
            _currentLife = MaxLife;
            return;
        }
        _currentLife += amount;
        return;
    }
    #endregion

    #region PRIVATE_METHODS
    
    private bool IsDead() => _currentLife <= 0;


    private IEnumerator DieCoroutine()
    {
        if (_animator != null)
        {
            _movement.Stop();
            
            if (GetComponent<EnemyAi>()==null)
            {
                _animator.SetBool("isDead", true);
                yield return new WaitForSeconds(3f);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }

        }
        Destroy(gameObject);
    }

    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }



    #endregion
}
