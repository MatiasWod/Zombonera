using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.ComponentModel;

public enum UnityScenes
{
    [Description("MainMenu")]
    Menu,
    [Description("SampleScene")]
    Game,
    [Description("EndGame")]
    End,
}

public class MenuManager : MonoBehaviour
{
    public void ActionPlay() => SceneManager.LoadScene((int)UnityScenes.Game);

    

    public void ActionExit() => Application.Quit();
}
