using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameText : CCompoent
{
    public Text Game;

    // Update is called once per frame
    void Update()
    {
        switch(GameManager.instance.LG)
        {
            case GameSetting.Lanaguage.Eng:
                Game.text = "SurivalShooter Game";
                break;
            case GameSetting.Lanaguage.Kor:
                Game.text = "서바이벌 슈터 게임";
                break;
        }
    }
}
