using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private void OnDestroy()
    {
        RoundManager _roundManager = GameObject.FindObjectOfType<RoundManager>();
        if (_roundManager != null)
        {
        _roundManager._zombiesKilled++;
        //Debug.Log("destroyed " + _roundManager._zombiesKilled);
        }

    }
}
