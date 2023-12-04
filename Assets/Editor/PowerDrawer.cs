using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

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