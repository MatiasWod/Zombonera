using UnityEngine;

public class HealingTrigger : MonoBehaviour
{
    [SerializeField] private int _healingAmount = 1; // Amount of healing to apply
    private float _currentCd=0;
    [SerializeField] private float _healingCd = 3;

    private void Update()
    {
        if (_currentCd >= 0)
        {
            _currentCd -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        // Check if the colliding object is the player character
        GameObject player = GameObject.Find("Character");
        IDamagable damagable = player.GetComponent<IDamagable>();
        
        if (damagable != null && _currentCd <= 0)
        {
            // Apply healing to the player character
            EventQueueManager.instance.AddEvent(new CmdHeal(damagable, _healingAmount));
            _currentCd = _healingCd;
            Debug.Log("trigger");
        }
    }
}
