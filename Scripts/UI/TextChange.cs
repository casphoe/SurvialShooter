using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextChange : CCompoent
{
    public Text tx;

    public string[] Message = new string[2];
    
    void Start()
    {
        switch(GameManager.instance.LG)
        {
            case GameSetting.Lanaguage.Eng:
                tx.text = Message[0] +  GameSetting.PlayerName;
                break;
            case GameSetting.Lanaguage.Kor:
                if(GameManager.instance.Player == GameSetting.Player.Girl)
                {
                    tx.text = Message[1] + "소녀";
                }
                else if(GameManager.instance.Player == GameSetting.Player.Man)
                {
                    tx.text = Message[1] + "남자";
                }
                else if(GameManager.instance.Player == GameSetting.Player.Solider)
                {
                    tx.text = Message[1] + "군인";
                }
                break;
        }
    }
}
