using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard: MonoBehaviour
{
    [SerializeField] GameObject Card_Back;
    [SerializeField] SceneController controller;

    private int _id;
    public int Id
    {
        get { return _id; }
    }
  
    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void OnMouseDown()
    {
       //cardBack.SetActive(false);
     if (Card_Back.activeSelf && controller.canReveal)
        {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        Card_Back.SetActive(true); 
    }
}
