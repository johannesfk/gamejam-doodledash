using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectableCard : MonoBehaviour
{
    [SerializeField]
    private PowerType power;
    //[SerializeField]
    private CardStack cardStack;

    private CardStyles cardStyles;

    private void Awake()
    {
        cardStack = FindObjectOfType<CardStack>();
        cardStyles = cardStack.GetComponentInParent<CardStyles>();

        if (cardStyles.cardColors != null)
        {
            Color cardColor = cardStyles.cardColors[power];
            GetComponent<SpriteRenderer>().color = cardColor;
        }

        /*
        Enable this when we have sprites for the cards
        
        if (cardStyles.cardSprites != null)
        {
            Sprite cardSprite = cardStyles.cardSprites[power];
            GetComponent<SpriteRenderer>().sprite = cardSprite;
        }
        */
    }

    private void OnDestroy()
    {
        Debug.Log("Tilføjer " + power + " til kortstakken");
        cardStack.Add(power);
    }
}
