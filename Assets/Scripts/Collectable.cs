using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("Collectable destroyed");
    }
}
