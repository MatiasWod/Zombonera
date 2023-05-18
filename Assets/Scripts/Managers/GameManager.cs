using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    public void GameOver()
    {
        SceneManager.LoadScene((int)UnityScenes.End);
    }
}
