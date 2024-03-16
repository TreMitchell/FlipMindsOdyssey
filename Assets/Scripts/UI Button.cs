using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] string targetMessage;
    public Color highlightColor = Color.cyan;

    private void OnMouseEnter()
    {
       SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor;
        }
    }

    private void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        transform.localScale = Vector3.one * 1.1f;
    }
    private void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }
}

