using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Linq;


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
    public List<Card> cards;
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


// PowerDrawerUIE
[CustomPropertyDrawer(typeof(Card))]
public class DrawerUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var typeField = new PropertyField(property.FindPropertyRelative("power"), "Card Power");
        // Add fields to the container.
        container.Add(typeField);

        return container;
    }
}