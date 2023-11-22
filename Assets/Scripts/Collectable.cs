using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnDestroy()
    {
        Collectables.collectableCount--; // Decrease the collectable count by 1
        Debug.Log("Collectable destroyed");
    }
}
