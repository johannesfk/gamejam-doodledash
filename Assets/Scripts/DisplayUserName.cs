using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUserName : MonoBehaviour
{
    public Text obj_text;
    public InputField display;

    void Start()
    {
        obj_text.text = PlayerPrefs.GetString("User_name", "");

    }
    public void DisplayText()
    {
        obj_text.text = display.text;
        PlayerPrefs.SetString("User_name", obj_text.text);
        PlayerPrefs.Save();
    }
}
