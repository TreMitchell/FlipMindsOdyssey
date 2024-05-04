//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UIButton : MonoBehaviour
//{
//    [SerializeField] GameObject targetObject;
//    [SerializeField] string targetMessage;
//    public Color highlightColor = Color.cyan;

//    public void OnMouseEnter()
//    {
//        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
//        if (sprite != null)
//        {
//            sprite.color = highlightColor;
//        }
//    }
//    public void OnMouseExit()
//    {
//        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
//        if (sprite != null)
//        {
//            sprite.color = Color.white;
//        }
//    }

//    public void OnMouseDown()
//    {
//        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
//    }
//    public void OnMouseUp()
//    {
//        transform.localScale = Vector3.one;
//        if (targetObject != null)
//        {
//            targetObject.SendMessage(targetMessage);
//        }
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class UIButton : MonoBehaviour
//{
//    [SerializeField] string targetScene; // New field to specify the target scene
//    [SerializeField] bool isRestartButton = false; // Flag to identify if this button is for restarting
//    [SerializeField] bool isBackButton = false; // Flag to identify if this button is for going back

//    public Color highlightColor = Color.cyan;

//    public void OnMouseEnter()
//    {
//        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
//        if (sprite != null)
//        {
//            sprite.color = highlightColor;
//        }
//    }

//    public void OnMouseExit()
//    {
//        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
//        if (sprite != null)
//        {
//            sprite.color = Color.white;
//        }
//    }

//    public void OnMouseDown()
//    {
//        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
//    }

//    public void OnMouseUp()
//    {
//        transform.localScale = Vector3.one;

//        if (isRestartButton)
//        {
//            // Restart the current scene
//            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//        }
//        else if (isBackButton)
//        {
//            // Go back to the previous scene
//            int currentIndex = SceneManager.GetActiveScene().buildIndex;
//            SceneManager.LoadScene(currentIndex - 1);
//        }
//        else
//        {
//            // Load the specified target scene
//            SceneManager.LoadScene(targetScene);
//        }
//    }
//}


using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public enum ActionType { Restart, Next, Previous, LoadSpecificScene }

    public ActionType actionType;
    public string sceneToLoad; // New field to specify the target scene

    public Color highlightColor = Color.cyan;

    void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor;
        }
    }

    void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }
    }

    void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    void OnMouseUp()
    {
        transform.localScale = Vector3.one;

        switch (actionType)
        {
            case ActionType.Restart:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case ActionType.Next:
                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
                break;
            case ActionType.Previous:
                int currentIndex = SceneManager.GetActiveScene().buildIndex;
                if (currentIndex > 0)
                {
                    SceneManager.LoadScene(currentIndex - 1);
                }
                break;
            case ActionType.LoadSpecificScene:
                SceneManager.LoadScene(sceneToLoad);
                break;
        }
    }
}

