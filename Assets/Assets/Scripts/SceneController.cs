using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int gridRows = 2;
    public int gridColumns = 10;
    public float offsetX = 1.5f;
    public float offsetY = 2.5f;

    private int score = 0;
    private int miss = 0;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    //private MemoryCard thirdRevealed;

    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] TMP_Text missLabel;

    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;

    public bool canReveal 
    {
        get { return secondRevealed == null; }
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
            Debug.Log("Match?: " + (firstRevealed.Id == secondRevealed.Id));
            
            StartCoroutine(CheckMatch());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("20CardGame");
    }

    private IEnumerator CheckMatch()
    {
        if (firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            Debug.Log($"Score: {score}");
            scoreLabel.text = $"Score: {score}";
            
            if (score == 10) {
                Debug.Log("Congratulations!  You won!");
                Debug.Log("Results: "+ $"Score: {score}" + $" Miss: {miss}" + $" Total Attempts: {score + miss}");
            }
        }
        else
        {
            miss++;
            Debug.Log($"Miss: {miss}");
            missLabel.text =  $"Miss: {miss}";
            yield return new WaitForSeconds(0.5f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
            //thirdRevealed.Unreveal();
        }

        firstRevealed = null;
        secondRevealed = null;
        //thirdRevealed = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9};

        //Shuffle the numbers array
        numbers = ShuffleArray(numbers);

        
        //instantiate & position 20 cards
        for (int i = 0; i < gridColumns; i++)
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

                int index = j * gridColumns + i;
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
            int temp = newArray[i];
            int rand = Random.Range(i, newArray.Length);
            newArray[i] = newArray[rand];
            newArray[rand] = temp;
        }
        return newArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
