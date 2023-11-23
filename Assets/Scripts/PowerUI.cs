using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUI : MonoBehaviour
{

    [SerializeField] int powerListPosition;
    private SpriteRenderer sR;
    private PowerType powerType;
    private CardStyles cardStyles;
    private CardStack cardStack;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        cardStack = FindObjectOfType<CardStack>();
        cardStyles = cardStack.GetComponentInParent<CardStyles>();
    }

    void Update()
    {
        powerType = cardStack.cards[powerListPosition].power;

        if (cardStyles.cardSprites != null)
        {
            Sprite newPowerSprite = cardStyles.cardSprites[powerType];
            sR.sprite = newPowerSprite;
        }

    }
}
