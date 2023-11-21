using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public PowerType power;
    public CardStack cardStack;

    private void OnDestroy()
    {
        Debug.Log("Tilføjer " + power + " til kortstakken");
        cardStack.Add(power);
    }
}
