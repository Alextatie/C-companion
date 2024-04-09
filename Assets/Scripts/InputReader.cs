using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputReader : MonoBehaviour
{
    private string input;
    public TMP_InputField output;
    public TextMeshProUGUI answer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadInput(string s)
    {
        input = s;
        Debug.Log(input);
    }

    public void Q1_1()
    {
        //return input is "printf";
        if (input is "printf")
        {
            Debug.Log("true");
            answer.text = "<color=green>Correct!";
            output.text = "<color=white>Hello World!";
            SoundManager.PlaySound("correct");
        }
        else
        {
            Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            output.text = "<color=red>Error!";
            SoundManager.PlaySound("wrong");
        }
    }
}
