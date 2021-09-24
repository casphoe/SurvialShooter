using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffImage : CCompoent
{
    public Image Buff;
    public GameSetting.Buff buff;
    public float BuffTime;

    float time = 0f;
    float xTime = 0;
    float blinktime = 0.1f;
    float waitTime = 0.2f;

    private void Start()
    {
        switch(buff)
        {
            case GameSetting.Buff.Sheild:
                GameManager.instance.ItemStat("Shield");
                BuffTime = GameSetting.RemateTime;
                break;
            case GameSetting.Buff.AttackUp:
                GameManager.instance.ItemStat("AttackUp");
                BuffTime = GameSetting.RemateTime;
                break;
        }
    }

    private void Update()
    {
        if(time < BuffTime - 3f)
        {
            Buff.color = new Color(1, 1, 1, 1); //켜짐
        }
        else
        {
            if(xTime < blinktime) //깜빡
            {
                Buff.color = new Color(1, 1, 1, 1 - xTime * 10); //꺼졌다가
            }
            else if(xTime < waitTime * blinktime)
            {

            }
            else
            {
                Buff.color = new Color(1, 1, 1, (xTime - (waitTime + blinktime)) * 10);
                if(xTime > waitTime + blinktime * 2)
                {
                    xTime = 0;
                    waitTime *= 0.8f; //깜빡이는 시간 줄어들기
                    if(waitTime < 0.02f)
                    {
                        time = 0;
                        waitTime = 0.2f;
                        gameObject.SetActive(false);
                    }
                }
            }
            xTime += Time.deltaTime;
        }
        time += Time.deltaTime;
    }
}
