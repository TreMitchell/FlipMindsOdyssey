using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevel1()
    {
        LoadLevel(1);
    }

    public void LoadLevel2()
    {
        LoadLevel(2);
    }

    public void LoadLevel3()
    {
        LoadLevel(3);
    }

    public void LoadLevel4()
    {
        LoadLevel(4);
    }

    public void LoadLevel5()
    {
        LoadLevel(5);
    }
    public void LoadLevel6()
    {
        LoadLevel(6);
    }
}
