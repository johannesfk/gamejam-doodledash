using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    public static string username;
    TextMeshProUGUI text;

    public string GetUsername()
    {
        string username = text.text;

        Debug.Log(username);
        return username;
    }
}
