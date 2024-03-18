using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 3;
    public const int gridCols = 6;
    public const float offsetX = 2.5f;
    public const float offsetY = 4f;

    private int score = 0;

    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }
    public void CardRevealed(MemoryCard card)
    {
        if (firstRevealed == null)
        {
        firstRevealed = card;
        } else
        {
            secondRevealed = card;
            //Debug.Log("Match?" + (firstRevealed.Id == secondRevealed.Id));
            StartCoroutine(CheckMatch());
        }
    }
       
    private IEnumerator CheckMatch()
    {
        if (firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            //Debug.Log("Score: " + score);
            scoreLabel.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.8f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }
        firstRevealed = null;
        secondRevealed = null;
    }

    void Start()
    {
        //int id = Random.Range(0, images.Length);
       //originalCard.SetCard(id, images[id]);

        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5 , 5, 6, 6, 7, 7, 8, 8};
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

                //int id = Random.Range(0, images.Length);
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float poxY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, poxY, startPos.z);
            }
        }
    }
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++) 
        {
            int temp = newArray[i];
            int rand = Random.Range(i, newArray.Length);
            newArray[i] = newArray[rand];
            newArray[rand] = temp;
        }
        return newArray;
    
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
