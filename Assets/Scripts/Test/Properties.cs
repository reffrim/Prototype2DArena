using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    string _userMessage;
    public string UserMessage
    {
        get
        {
            return _userMessage;
        }
        set
        {
            if (value == "I h4x0r You!")
                _userMessage = "Hacking Attempt Prevented";
            else
                _userMessage = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UserMessage = "I h4x0r You!";
        Debug.Log(UserMessage);

        UserMessage = "Hi there!";
        Debug.Log(UserMessage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
