using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public GameObject[] _spawnPoints;
    public GameObject[] _zombies;       //if we decide to add other type of zombies
    [SerializeField] private TextMeshProUGUI _textField;

    public int _zombieCountIncrease;
    public int _round;
    public bool _spawning;
    private int _zombiesSpawned;
    public int _zombiesKilled;
    public bool cubeZombies = false;

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
        _textField.text = IntToRoman(_round);
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

    public static string IntToRoman(int number)
    {
        int[] arabicValues = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        string[] romanSymbols = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        StringBuilder result = new StringBuilder();
        int remaining = number;

        for (int i = 0; i < arabicValues.Length; i++)
        {
            while (remaining >= arabicValues[i])
            {
                result.Append(romanSymbols[i]);
                remaining -= arabicValues[i];
            }
        }

        return result.ToString();
    }

    void SpawnZombie(int round)
    {
        int spawnPostion = Random.Range(0, 4 );
        if (cubeZombies)
        {
            Instantiate(_zombies[1], _spawnPoints[spawnPostion].transform.position, _spawnPoints[spawnPostion].transform.rotation);
        }
        else
        {
            int num=Random.Range(0, 2);
            if(num==0)
                Instantiate(_zombies[0], _spawnPoints[spawnPostion].transform.position, _spawnPoints[spawnPostion].transform.rotation);
            else
            {
                Instantiate(_zombies[2], _spawnPoints[spawnPostion].transform.position, _spawnPoints[spawnPostion].transform.rotation);

            }
        }
        _zombiesSpawned++;
    }

    public void SetSpawnCubes(bool cond)
    {
        cubeZombies = cond;
    }
}



