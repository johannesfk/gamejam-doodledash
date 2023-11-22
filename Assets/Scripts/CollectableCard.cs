using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectableCard : MonoBehaviour
{
    [SerializeField]
    private PowerType power;
    [SerializeField]
    private CardStack cardStack;

    private CardStyles cardStyles;

    private void Awake()
    {
        cardStyles = cardStack.GetComponentInParent<CardStyles>();

        Color cardColor = cardStyles.cardColors[power];
        Sprite cardSprite = cardStyles.cardSprites[power];

        GetComponent<SpriteRenderer>().color = cardColor;
        GetComponent<SpriteRenderer>().sprite = cardSprite;
    }

    private void OnDestroy()
    {
        Debug.Log("Tilføjer " + power + " til kortstakken");
        cardStack.Add(power);
    }
}
