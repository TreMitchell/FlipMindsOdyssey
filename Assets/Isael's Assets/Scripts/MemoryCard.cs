// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MemoryCard : MonoBehaviour
// {
//     [SerializeField] GameObject cardBack;
//     [SerializeField] SceneController controller;
//     [SerializeField] AudioClip clickSound; // New audio clip field

//     private int _id;
//     public int Id
//     {
//         get { return _id; }
//     }

//     private AudioSource audioSource; // New AudioSource component

//     void Start()
//     {
//         // Add an AudioSource component if not already attached
//         audioSource = GetComponent<AudioSource>();
//         if (audioSource == null)
//         {
//             audioSource = gameObject.AddComponent<AudioSource>();
//         }
//         // Set the audio clip for the AudioSource
//         audioSource.clip = clickSound;
//     }

//     public void SetCard(int id, Sprite image)
//     {
//         _id = id;
//         GetComponent<SpriteRenderer>().sprite = image;
//     }

//     public void OnMouseDown()
//     {
//         if (cardBack.activeSelf && controller.canReveal)
//         {
//             cardBack.SetActive(false);
//             controller.CardRevealed(this);
//         }
//     }

//     public void Unreveal()
//     {
//         cardBack.SetActive(true);
//     }
// }


using UnityEngine;
using System.Collections;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack;
    [SerializeField] SceneController controller;
    [SerializeField] AudioClip clickSound;
    private bool isFlipped = false;
    private bool isAnimating = false;
    private int _id;
    public int Id { get { return _id; } }
    private AudioSource audioSource;

    public bool IsFlipped { get { return isFlipped; } }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = clickSound;
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
            if (clickSound != null && audioSource != null)
            {
                audioSource.Play();
            }
            if (!isFlipped && !isAnimating)
            {
                Flip();
            }
        }
    }

    public void Unreveal()
    {
        if (!isAnimating)
        {
            isFlipped = false;
            isAnimating = true;
            cardBack.SetActive(true);
            StartCoroutine(FlipBackAnimation());
        }
        else
        {
            Debug.LogWarning("Flip animation is already in progress");
        }
    }

    public void Flip()
    {
        if (!isAnimating)
        {
            isFlipped = true;
            isAnimating = true;
            StartCoroutine(FlipAnimation());
        }
        else
        {
            Debug.LogWarning("Flip animation is already in progress");
        }
    }

    IEnumerator FlipAnimation()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 180f, 0);

        for (float t = 0; t <= 1; t += Time.deltaTime * 5f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        isAnimating = false;
        CheckGameCompletion();
    }

    IEnumerator FlipBackAnimation()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);

        for (float t = 0; t <= 1; t += Time.deltaTime * 5f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        isAnimating = false;
    }

    void CheckGameCompletion()
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
            controller.GameCompleted();
        }
    }
}
