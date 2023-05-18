using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject[] _spawnPoints;
    public GameObject[] _zombies;       //if we decide to add other type of zombies
    public int _zombieCountIncrease;
    public int _round;
    public bool _spawning;
    private int _zombiesSpawned;
    public int _zombiesKilled;

    // Start is called before the first frame update
    void Start()
    {
        _zombieCountIncrease = 4;
        _round = 1;
        _spawning = false;
        _zombiesSpawned = 0;
        _zombiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_spawning==false && _zombiesSpawned == _zombiesKilled)
        {
            StartCoroutine(SpawnRound(_zombieCountIncrease * _round));
        }
    }

    IEnumerator SpawnRound(int zombieCounter)
    {
        Debug.Log("Round Started");
        _spawning = true;
        yield return new WaitForSeconds(5);
        for(int i=0; i< zombieCounter; i++)
        {
            SpawnZombie(_round);
            yield return new WaitForSeconds(1);
        }
        _round += 1;
        _spawning = false;
        
        yield break;
    }

    void SpawnZombie(int round)
    {
        int spawnPostion = Random.Range(0, 4 );
        Instantiate(_zombies[0], _spawnPoints[spawnPostion].transform.position, _spawnPoints[spawnPostion].transform.rotation);
        
        _zombiesSpawned++;
    }
}
