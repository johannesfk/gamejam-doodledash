using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{

    [SerializeField] int powerListPosition;
    private SpriteRenderer sR;
    private Image image;
    private PowerType powerType;
    private CardStyles cardStyles;
    private CardStack cardStack;

    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
        cardStack = FindObjectOfType<CardStack>();
        cardStyles = cardStack.GetComponentInParent<CardStyles>();
    }

    void Update()
    {
        if (cardStack.cards.Count > powerListPosition)
        {

            powerType = cardStack.cards[powerListPosition].power;


            if (cardStyles.cardSprites != null)
            {
                Debug.Log("cardStyles.cardSprites != null");
                image.sprite = cardStyles.cardSprites[powerType];
                image.color = new Color(1, 1, 1, 1);
                /* Sprite newPowerSprite = cardStyles.cardSprites[powerType];
                sR.sprite = newPowerSprite;
                Debug.Log("newPowerSprite: " + newPowerSprite.name); */
            }
            else
            {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);
        }

    }
}
