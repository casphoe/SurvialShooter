using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelText : MonoBehaviour
{
    public string[] Message;

    public Text Label;

    // Update is called once per frame
    void Update()
    {
        switch(GameManager.instance.LG)
        {
            case GameSetting.Lanaguage.Eng:
                Label.text = "" + Message[0];
                break;
            case GameSetting.Lanaguage.Kor:
                Label.text = "" + Message[1];
                break;
        }
    }
}
