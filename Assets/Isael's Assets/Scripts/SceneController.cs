//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using TMPro;

//public class SceneController : MonoBehaviour
//{
//    public const int gridRows = 2;
//    public const int gridCols = 6;
//    public const float offsetX = 2.8f;
//    public const float offsetY = 4f;

//    [SerializeField] MemoryCard originalCard;
//    [SerializeField] TMP_Text scoreLabel;
//    [SerializeField] TMP_Text wrongAttemptsLabel;
//    [SerializeField] Sprite[] images;
//    [SerializeField] GameObject gameCompletionUI;
//    [SerializeField] TMP_Text gameCompletionText;

//    private MemoryCard firstRevealed;
//    private MemoryCard secondRevealed;
//    private int score = 0;
//    private int wrongAttempts = 0;

//    public bool canReveal
//    {
//        get { return secondRevealed == null; }
//    }

//    private void CheckGameCompletion()
//    {
//        MemoryCard[] cards = FindObjectsOfType<MemoryCard>();
//        bool allMatched = true;
//        foreach (MemoryCard card in cards)
//        {
//            if (!card.IsFlipped) // Access isFlipped using the IsFlipped property
//            {
//                allMatched = false;
//                break;
//            }
//        }

//        if (allMatched)
//        {
//            GameCompleted(); // Call the function in SceneController to handle game completion
//        }
//    }

//    public void GameCompleted()
//    {
//        gameCompletionUI.SetActive(true); // Show the game completion UI
//        gameCompletionText.text = "You passed this Level!"; // Set the completion message
//        gameCompletionText.color = Color.white; // Set text color to white
//        gameCompletionText.alignment = TextAlignmentOptions.Center; // Center-align the text
//    }


//    void Start()
//    {
//        Vector3 startPos = originalCard.transform.position;

//        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
//        numbers = ShuffleArray(numbers);

//        for (int i = 0; i < gridCols; i++)
//        {
//            for (int j = 0; j < gridRows; j++)
//            {
//                MemoryCard card;

//                if (i == 0 && j == 0)
//                {
//                    card = originalCard;
//                }
//                else
//                {
//                    card = Instantiate(originalCard) as MemoryCard;
//                }

//                int index = j * gridCols + i;
//                int id = numbers[index];
//                card.SetCard(id, images[id]);

//                float posX = (offsetX * i) + startPos.x;
//                float posY = -(offsetY * j) + startPos.y;
//                card.transform.position = new Vector3(posX, posY, startPos.z);
//            }
//        }
//    }

//    private int[] ShuffleArray(int[] numbers)
//    {
//        int[] newArray = numbers.Clone() as int[];
//        for (int i = 0; i < newArray.Length; i++)
//        {
//            int tmp = newArray[i];
//            int r = Random.Range(i, newArray.Length);
//            newArray[i] = newArray[r];
//            newArray[r] = tmp;
//        }
//        return newArray;
//    }

//    public void CardRevealed(MemoryCard card)
//    {
//        if (firstRevealed == null)
//        {
//            firstRevealed = card;
//        }
//        else
//        {
//            secondRevealed = card;
//            StartCoroutine(CheckMatch());
//        }
//    }

//    private IEnumerator CheckMatch()
//    {
//        if (firstRevealed.Id == secondRevealed.Id)
//        {
//            score++;
//            scoreLabel.text = $"Score: {score}";
//        }
//        else
//        {
//            wrongAttempts++;
//            wrongAttemptsLabel.text = $"Wrong Attempts: {wrongAttempts}";
//            Debug.Log("Wrong Attempts: " + wrongAttempts);

//            yield return new WaitForSeconds(.5f);

//            firstRevealed.Unreveal();
//            secondRevealed.Unreveal();
//        }

//        firstRevealed = null;
//        secondRevealed = null;

//        // Check if all matches have been found
//        if (score == (gridRows * gridCols) / 2)
//        {
//            // Load the next scene
//            LoadNextScene();
//        }
//    }

//    void LoadNextScene()
//    {
//        // Load the next scene (assuming the next scene is named "NextScene")
//        SceneManager.LoadScene("AnimalCrossing");
//    }

//    public void Restart()
//    {
//        SceneManager.LoadScene("Isael");
//    }
//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 6;
    public const float offsetX = 2.8f;
    public const float offsetY = 4f;

    [SerializeField] MemoryCard originalCard;
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] TMP_Text wrongAttemptsLabel;
    [SerializeField] Sprite[] images;
    [SerializeField] GameObject gameCompletionUI;
    [SerializeField] TMP_Text gameCompletionText;
    [SerializeField] string nextSceneName; // Next scene to load after completing the level
    [SerializeField] string restartSceneName; // Scene to load when restarting

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;
    private int score = 0;
    private int wrongAttempts = 0;

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    private void CheckGameCompletion()
    {
        MemoryCard[] cards = FindObjectsOfType<MemoryCard>();
        bool allMatched = true;
        foreach (MemoryCard card in cards)
        {
            if (!card.IsFlipped) // Access isFlipped using the IsFlipped property
            {
                allMatched = false;
                break;
            }
        }

        if (allMatched)
        {
            GameCompleted(); // Call the function in SceneController to handle game completion
        }
    }

    public void GameCompleted()
    {
        gameCompletionUI.SetActive(true); // Show the game completion UI
        gameCompletionText.text = "You passed this Level!"; // Set the completion message
        gameCompletionText.color = Color.white; // Set text color to white
        gameCompletionText.alignment = TextAlignmentOptions.Center; // Center-align the text
    }

    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;

                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void CardRevealed(MemoryCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            scoreLabel.text = $"Score: {score}";
        }
        else
        {
            wrongAttempts++;
            wrongAttemptsLabel.text = $"Wrong Attempts: {wrongAttempts}";
            Debug.Log("Wrong Attempts: " + wrongAttempts);

            yield return new WaitForSeconds(.5f);

            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }

        firstRevealed = null;
        secondRevealed = null;

        // Check if all matches have been found
        if (score == (gridRows * gridCols) / 2)
        {
            // Load the next scene
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    public void Restart()
    {
        SceneManager.LoadScene(restartSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
