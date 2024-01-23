using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// <list type="table">
/// <item>
/// <term>Dash</term>
/// <description>The player can dash</description>
/// </item>
/// <item>
/// <term>DoubleJump</term>
/// <description>The player can double jump</description>
/// </item>
/// <item>
/// <term>WallJump</term>
/// <description>The player can wall jump</description>
/// </item>
/// <item>
/// <term>Bounce</term>
/// <description>The player can bounce off of the ground</description>
/// </item>
/// <item>
/// <term>Teleport</term>
/// <description>The player can teleport</description>
/// </item>
/// </list>
/// </summary>
public enum PowerType
{
    Dash,
    DoubleJump,
    WallJump,
    Bounce,
    Teleport
}

public class CardStack : MonoBehaviour
{
    public CardUIManager cardUIManager;

    public List<Card> cards;

    /// <summary>
    ///  Adds a card to the stack.
    ///  If the stack is empty, add the card to the stack
    ///  If not, add the card to the second position in the stack
    /// </summary>
    /// <param name="power">The type of power to be added</param>
    public void Add(PowerType power)
    {
        if (cards.Count > 1)
        {
            cards.Insert(1, new Card(power));
        }
        else
        {
            cards.Add(new Card(power));
        }
    }

    public void Use()
    {
        if (cards.Count > 0)
        {
            cards.RemoveAt(0);
        }

    }

}

[Serializable]
public class Card
{
    public PowerType power;
    public Color color;
    public Card(PowerType power)
    {
        this.power = power;
    }
}


