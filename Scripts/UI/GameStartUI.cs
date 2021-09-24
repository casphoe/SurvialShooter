using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStartUI : CCompoent
{
    private float GameStartTimer = 0f;
    private Text GameCountText;
    private Image CanvasEffects;

    public GameObject MessageCanvas;
    public GameObject StartEffectCanvas;

    // Start is called before the first frame update
    void Start()
    {
        GameStartTimer = 0f;
        GameCountText = GetComponent<Text>();
        CanvasEffects = StartEffectCanvas.GetComponent<Image>();
        StartCoroutine(EffectCanvas());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameStartTimer == 0)
        {
            GameSetting.IsGameStart = false;
        }

        if(GameStartTimer <= 400)
        {
            GameStartTimer++;

            if(GameStartTimer < 80)
            {
                GameCountText.text = "3";
            }

            if(GameStartTimer > 160)
            {
                GameCountText.text = "2";
            }
            if(GameStartTimer > 240)
            {
                GameCountText.text = "1";
            }

            switch(GameManager.instance.LG)
            {
                case GameSetting.Lanaguage.Eng:
                    if(GameStartTimer > 320)
                    {
                        GameCountText.text =  "Mission Start";
                    }
                    if(GameStartTimer >= 400)
                    {
                        GameCountText.text = "Survive the enemy to end";
                        StartCoroutine(LoadingEnd());
                    }
                    break;
                case GameSetting.Lanaguage.Kor:
                    if(GameStartTimer > 320)
                    {
                        GameCountText.text = "미션 시작";
                    }
                    if(GameStartTimer >= 400)
                    {
                        GameCountText.text = "적한테 끝까지 살아남으세요";
                        StartCoroutine(LoadingEnd());
                    }
                    break;
            }
        }
    }

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1f);
        GameSetting.IsGameStart = true;
        MessageCanvas.SetActive(false);
    }

    IEnumerator EffectCanvas()
    {
        CanvasEffects.color = new Color(CanvasEffects.color.r, CanvasEffects.color.g, CanvasEffects.color.b, 1);

        while(CanvasEffects.color.a > 0.0f)
        {
            CanvasEffects.color = new Color(CanvasEffects.color.r, CanvasEffects.color.g, CanvasEffects.color.b, CanvasEffects.color.a - (GameStartTimer / 80000f));
            yield return null;
        }
    }
}
