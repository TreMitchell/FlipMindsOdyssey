using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack;
    [SerializeField] SceneController controller;
    [SerializeField] AudioClip clickSound; // New audio clip field
    private bool isFlipped = false; // Flag to track if the card is flipped
    private bool isAnimating = false; // Keeps track of a flip animation's progress

    // Variables that control the flip animation's speed and angle
    public float flipSpeed = 5f; // Speed of the flip animation
    public float flipAngle = 180f; // Angle to rotate the card
    private int _id;
    public int Id { 
        get { return _id; }
    }

    private AudioSource audioSource; // New AudioSource component

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // Set the audio clip for the AudioSource
        audioSource.clip = clickSound;
    }

    public void SetCard(int id, Sprite image) {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    } 
   
    public void OnMouseDown() {
        if (cardBack.activeSelf && controller.canReveal){
            cardBack.SetActive(false);
            controller.CardRevealed(this);
            // Play the click sound when the card is clicked
            if (clickSound != null && audioSource != null)
            {
                audioSource.Play();
            }
            if (!isFlipped && !isAnimating) {
                Flip();
             }
        }
    }

    public void Unreveal(){
        if (!isAnimating) {
        isFlipped = false;
        isAnimating = true;
        //Play flip back animation
        cardBack.SetActive(true);
        StartCoroutine(FlipBackAnimation());   
        }
        else {
            Debug.LogWarning("Flip animation is already in progress");
        }
    }

    public void Flip() {
        if (!isAnimating) {
          isFlipped = true;
          isAnimating = true; //Setting animation flag to true so only 2 cards can be flipped at a time
          Debug.Log("Flipping Card: " + _id);
          //Play flip animation
          StartCoroutine(FlipAnimation());  
        } 
        else {
            Debug.LogWarning("Flip animation is already in progress");
        }
    }


    IEnumerator FlipAnimation() {
        // Flip the card to show the card value
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, flipAngle, 0);

        for (float t = 0; t <= 1; t += Time.deltaTime * flipSpeed) {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        isAnimating = false; // Reset animation flag
    }

    IEnumerator FlipBackAnimation() {
        // Flip the card back to its face-down position
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);

        for (float t = 0; t <= 1; t += Time.deltaTime * flipSpeed) {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        isAnimating = false; //Reset animation flag
    }
}
