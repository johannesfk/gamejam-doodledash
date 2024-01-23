using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public static int collectableCount;
    public static bool allCollected = false;

    private void Start()
    {
        collectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
    }
    private void FixedUpdate()
    {
        Debug.Log("collectableCount: " + collectableCount);

        // Check if all collectables are collected
        if (collectableCount < 1)
        {
            /// If the count of collables is less than 1,
            /// then all collectables are collected
            allCollected = true;
            Debug.Log("Gå til exit!");
        }
        else
        {
            /// If the count of collables is more than 1,
            /// then not all collectables are collected
            allCollected = false;
        }
    }
}