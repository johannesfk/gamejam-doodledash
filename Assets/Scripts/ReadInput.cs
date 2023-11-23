using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadInput : MonoBehaviour
{
    public TextMeshProUGUI obj_name;
    public TMP_InputField user_inputfield;

    void Start()
    {
        obj_name.text = PlayerPrefs.GetString("User_name");
        user_inputfield.characterLimit = 10;
    }
    public void SetName()
    {
        if (user_inputfield.text != "")
        {
            obj_name.text = user_inputfield.text;
            PlayerPrefs.SetString("User_name", obj_name.text);
            PlayerPrefs.Save();
            Debug.Log("Username saved: " + PlayerPrefs.GetString("User_name"));
        }
        else
        {
            Debug.LogError("user_inputfield is not assigned.");
        }
    }
}
