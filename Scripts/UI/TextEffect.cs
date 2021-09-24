using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextEffect : CCompoent
{
    private Text Effect;

    public string Message;

    public float MessageSpeed;

    float time;
   
    void Start()
    {
        Effect = GetComponent<Text>();
        MessageSpeed = 0.1f;

        switch (GameManager.instance.LG)
        {
            case GameSetting.Lanaguage.Eng:
                Message = "Click to start the game";
                break;
            case GameSetting.Lanaguage.Kor:
                Message = "게임을 시작하시려면 클릭하세요";
                break;
        }

        StartCoroutine(Typing(Effect, Message, MessageSpeed));
    }

    IEnumerator Typing(Text typeText, string Message, float Speed)
    {
        for(int i = 0; i <= Message.Length; i++)
        {
            typeText.text = Message.Substring(0, i * 1);
            yield return new WaitForSeconds(Speed);
        }
    }

    private void Update()
    {
        if(time < 0.5f)
        {
            Effect.color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            Effect.color = new Color(1, 1, 1, time);
            if(time > 1f)
            {
                time = 0f;
            }
        }

        time += Time.deltaTime;

        switch(GameManager.instance.LG)
        {
            case GameSetting.Lanaguage.Eng:
                Effect.text = "Click to start the game";
                break;
            case GameSetting.Lanaguage.Kor:
                Effect.text = "게임을 시작하시려면 클릭하세요";
                break;
        }
    }
}
