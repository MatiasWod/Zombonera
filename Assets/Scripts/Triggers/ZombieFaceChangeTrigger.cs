using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFaceChangeTrigger : MonoBehaviour
{
    [SerializeField] private RoundManager r_manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "pelota")
        {
            Debug.Log("Object named 'pelota' collided with the trigger.");
            r_manager.SetSpawnCubes(true);  
        }
    }
}
