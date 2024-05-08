//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Quit_Game : MonoBehaviour
//{
//    public void QuitGame(){
//        Application.Quit();
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private static QuitGame instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
