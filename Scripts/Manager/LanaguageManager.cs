using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanaguageManager : Singleton<LanaguageManager>
{
    public Toggle[] LanageToggle = new Toggle[2];

    public bool IsKor = false;
    public bool IsEng = false;

    public override void Awake()
    {
        base.Awake();

        string IsKr = PlayerPrefs.GetString("Kor", IsKor.ToString());
        IsKor = System.Convert.ToBoolean(IsKr);

        string IsEg = PlayerPrefs.GetString("Eng", IsEng.ToString());
        IsEng = System.Convert.ToBoolean(IsEg);
    }

    private void Start()
    {
        LanageToggle[0].onValueChanged.AddListener(Function_Toggle);
        LanageToggle[1].onValueChanged.AddListener(Function_Toggle1);

        if(IsKor == false)
        {
            LanageToggle[1].isOn = true;
            LanageToggle[0].isOn = false;
        }
        else
        {
            LanageToggle[1].isOn = false;
            LanageToggle[0].isOn = true;
        }
    }

    private void Function_Toggle(bool _bool)
    {
        IsKor = _bool;
        IsEng = !IsKor;
        LanageToggle[0].isOn = IsKor;
        LanageToggle[1].isOn = IsEng;
        GameManager.instance.LG = GameSetting.Lanaguage.Kor;
        PlayerPrefs.SetString("Kor", IsKor.ToString());
        PlayerPrefs.SetString("Eng", IsEng.ToString());
    }

    private void Function_Toggle1(bool _bool)
    {
        IsEng = _bool;
        IsKor = !IsEng;
        LanageToggle[0].isOn = IsKor;
        LanageToggle[1].isOn = IsEng;
        PlayerPrefs.SetString("Kor", IsKor.ToString());
        PlayerPrefs.SetString("Eng", IsEng.ToString());
        GameManager.instance.LG = GameSetting.Lanaguage.Eng;
    }
}
