using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectableCard : MonoBehaviour
{
    [SerializeField]
    private PowerType power;
    // [SerializeField]
    private CardStack cardStack;

    private CardStyles cardStyles;

    private void Start()
    {
        cardStack = FindObjectOfType<CardStack>();
        cardStyles = cardStack.GetComponentInParent<CardStyles>();

        if (cardStyles.cardColors != null)
        {
            Debug.Log("Tilføjer farve for " + power + " til kortstakken");
            Color cardColor = cardStyles.cardColors[power];

            GetComponent<SpriteRenderer>().color = cardColor;
        }

        if (cardStyles.cardSprites != null)
        {
            Debug.Log("Tilføjer sprite for " + power + " til kortstakken");
            Sprite cardSprite = cardStyles.cardSprites[power];
            GetComponent<SpriteRenderer>().sprite = cardSprite;
        }

    }

    private void OnDestroy()
    {
        Debug.Log("Tilføjer " + power + " til kortstakken");
        cardStack.Add(power);
    }
}
