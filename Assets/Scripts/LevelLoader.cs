using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    private Canvas canvas;
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas component found in the children of 'LevelLoader'");
        }
        else
        {
            // Assuming you have a reference to the camera you want the canvas to render with
            Camera renderCamera = Camera.main; // Replace with your camera
            canvas.worldCamera = renderCamera;
        }
    }
}
