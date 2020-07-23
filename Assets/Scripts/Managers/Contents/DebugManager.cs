using UnityEngine;
using UnityEngine.UI;

public class DebugManager
{

    public static void Debug(string debugMessage)
    {
        Text debugText = GameObject.Find("DebugText").GetComponent<Text>();
        string text = debugText.text;
        text += "\n";
        text += debugMessage;

        debugText.text = text;
    }

}
