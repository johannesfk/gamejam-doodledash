using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public PowerType power;
    public CardStack cardStack;

    private CardStyles cardStyles;

    private void Awake()
    {
        cardStyles = cardStack.GetComponentInParent<CardStyles>();

        Color cardColor = cardStyles.cardColors[power];

        GetComponent<SpriteRenderer>().color = cardColor;


        /* switch (power)
        {
            case PowerType.Dash:
                SetSpriteColor();
                break;
            default:
                break;
        } */
    }

    private void OnDestroy()
    {
        Debug.Log("Tilføjer " + power + " til kortstakken");
        cardStack.Add(power);
    }

    /* private void SetSpriteColor(Color cardStyle)
    {

        GetComponent<SpriteRenderer>().color = cardStyle;
    } */
}
