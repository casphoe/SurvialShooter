using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Lanagauge : CCompoent
{
    public Text Change;

    // Update is called once per frame
    void Update()
    {
        switch(GameManager.instance.LG)
        {
            case GameSetting.Lanaguage.Eng:
                Change.text = "Lanaguage";
                break;
            case GameSetting.Lanaguage.Kor:
                Change.text = "언어";
                break;
        }
    }
}
