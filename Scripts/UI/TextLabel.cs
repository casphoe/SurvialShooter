using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextLabel : MonoBehaviour
{
    public string[] Message = new string[2];

    public Text Label;
    
    void Start()
    {
        switch(GameManager.instance.LG)
        {
            case GameSetting.Lanaguage.Eng:
                Label.text = Message[0];
                break;
            case GameSetting.Lanaguage.Kor:
                Label.text = Message[1];
                break;
        }
    }
}
