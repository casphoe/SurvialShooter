using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DifficultyText : CCompoent
{
    public Text Difficulty;
    public string Message;

    void Start()
    {
        switch(GameManager.instance.LG)
        {
            case GameSetting.Lanaguage.Eng:
                if(GameManager.instance.difficulty == GameSetting.Difficulty.Easy)
                {
                    Message = "Easy";
                }
                else if(GameManager.instance.difficulty == GameSetting.Difficulty.Normal)
                {
                    Message = "Normal";
                }
                else if(GameManager.instance.difficulty == GameSetting.Difficulty.Hard)
                {
                    Message = "Hard";
                }
                Difficulty.text = Message;
                break;
            case GameSetting.Lanaguage.Kor:
                if (GameManager.instance.difficulty == GameSetting.Difficulty.Easy)
                {
                    Message = "쉬움";
                }
                else if (GameManager.instance.difficulty == GameSetting.Difficulty.Normal)
                {
                    Message = "보통";
                }
                else if (GameManager.instance.difficulty == GameSetting.Difficulty.Hard)
                {
                    Message = "어려움";
                }
                Difficulty.text = Message;
                break;
        }
    }

    void DifficultyChange()
    {
        
    }
}