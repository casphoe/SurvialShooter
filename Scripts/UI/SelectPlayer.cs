using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectPlayer : CCompoent
{
    private bool TurnLeft = false;
    private bool TurnRight = false;

    private Quaternion turn = Quaternion.identity;

    public int value = 0;
    public int charactorNum = 0;

    public Text PlayerNameText;
    public Text HpText;
    public Text PowerText;
    public Text DefenceText;
    public Text SpeedText;
    public Text AttackRateText;
    public Text MaxMagText;

    public Slider HpSlider;
    public Slider PowerSlider;
    public Slider DefenceSlider;
    public Slider SpeedSlider;
    public Slider AttackRateSlider;
    public Slider MaxMagSlider;

    public Text Hp;
    public Text Power;
    public Text Defence;
    public Text Speed;
    public Text AttackRate;
    public Text MaxMag;

    public GameObject DifficultyPanel;

    private void Start()
    {
        turn.eulerAngles = new Vector3(0, value, 0);
        DifficultyPanel.SetActive(false);
    }

    private void Update()
    {
        if(TurnLeft)
        {
            charactorNum++;
            if(charactorNum == 3)
            {
                charactorNum = 0;
            }
            value -= 120;
            TurnLeft = false;
        }
        if(TurnRight)
        {
            charactorNum--;
            if(charactorNum == -1)
            {
                charactorNum = 2;
            }
            value += 120;
            TurnRight = false;
        }
        turn.eulerAngles = new Vector3(0, value, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, turn, Time.deltaTime * 5.0f);
        CharacterStat();
    }

    public void LeftTurn()
    {
        TurnLeft = true;
        TurnRight = false;
    }

    public void RightTurn()
    {
        TurnLeft = false;
        TurnRight = true;
    }

    public void CharacterStat()
    {
        switch(charactorNum)
        {
            case 0:
                GameManager.instance.PlayerStat("Girl");
                switch(GameManager.instance.LG)
                {
                    case GameSetting.Lanaguage.Eng:
                        PlayerNameText.text = "Name : " + GameSetting.PlayerName;
                        Hp.text = "Hp : ";
                        Power.text = "Power : ";
                        Defence.text = "Defence : ";
                        Speed.text = "Speed : ";
                        AttackRate.text = "AttackRate : ";
                        MaxMag.text = "Max magazine rate :";
                        break;
                    case GameSetting.Lanaguage.Kor:
                        PlayerNameText.text = "이름 : 소녀";
                        Hp.text = "체력 : ";
                        Power.text = "공격력 : ";
                        Defence.text = "방어력 : ";
                        Speed.text = "스피드 : ";
                        AttackRate.text = "공격 속도 : ";
                        MaxMag.text = "최대 탄창 비율 :";
                        break;
                }
                HpText.text = "" + GameSetting.PlayerHp;
                PowerText.text = "" + GameSetting.PlayerPower;
                DefenceText.text = "" + GameSetting.PlayerDefnece;
                SpeedText.text = "" + GameSetting.PlayerSpeed;
                AttackRateText.text = "" + GameSetting.PlayerAttackRate;
                MaxMagText.text = "" + GameSetting.PlayerMaxMag;

                HpSlider.value = GameSetting.PlayerHp;
                PowerSlider.value = GameSetting.PlayerPower;
                DefenceSlider.value = GameSetting.PlayerDefnece;
                SpeedSlider.value = GameSetting.PlayerSpeed;
                AttackRateSlider.value = GameSetting.PlayerAttackRate;
                MaxMagSlider.value = GameSetting.PlayerMaxMag;

                GameManager.instance.Player = GameSetting.Player.Girl;
                break;
            case 1:
                GameManager.instance.PlayerStat("Man");
                switch (GameManager.instance.LG)
                {
                    case GameSetting.Lanaguage.Eng:
                        PlayerNameText.text = "Name : " + GameSetting.PlayerName;
                        Hp.text = "Hp : ";
                        Power.text = "Power : ";
                        Defence.text = "Defence : ";
                        Speed.text = "Speed : ";
                        AttackRate.text = "AttackRate : ";
                        MaxMag.text = "Max magazine rate :";
                        break;
                    case GameSetting.Lanaguage.Kor:
                        PlayerNameText.text = "이름 : 남자";
                        Hp.text = "체력 : ";
                        Power.text = "공격력 : ";
                        Defence.text = "방어력 : ";
                        Speed.text = "스피드 : ";
                        AttackRate.text = "공격 속도 : ";
                        MaxMag.text = "최대 탄창 비율 :";
                        break;
                }
                HpText.text = "" + GameSetting.PlayerHp;
                PowerText.text = "" + GameSetting.PlayerPower;
                DefenceText.text = "" + GameSetting.PlayerDefnece;
                SpeedText.text = "" + GameSetting.PlayerSpeed;
                AttackRateText.text = "" + GameSetting.PlayerAttackRate;
                MaxMagText.text = "" + GameSetting.PlayerMaxMag;

                HpSlider.value = GameSetting.PlayerHp;
                PowerSlider.value = GameSetting.PlayerPower;
                DefenceSlider.value = GameSetting.PlayerDefnece;
                SpeedSlider.value = GameSetting.PlayerSpeed;
                AttackRateSlider.value = GameSetting.PlayerAttackRate;
                MaxMagSlider.value = GameSetting.PlayerMaxMag;
                GameManager.instance.Player = GameSetting.Player.Man;
                break;
            case 2:
                GameManager.instance.PlayerStat("Solider");
                switch (GameManager.instance.LG)
                {
                    case GameSetting.Lanaguage.Eng:
                        PlayerNameText.text = "Name : " + GameSetting.PlayerName;
                        Hp.text = "Hp : ";
                        Power.text = "Power : ";
                        Defence.text = "Defence : ";
                        Speed.text = "Speed : ";
                        AttackRate.text = "AttackRate : ";
                        MaxMag.text = "Max magazine rate :";
                        break;
                    case GameSetting.Lanaguage.Kor:
                        PlayerNameText.text = "이름 : 군인";
                        Hp.text = "체력 : ";
                        Power.text = "공격력 : ";
                        Defence.text = "방어력 : ";
                        Speed.text = "스피드 : ";
                        AttackRate.text = "공격 속도 : ";
                        MaxMag.text = "최대 탄창 비율 :";
                        break;
                }
                HpText.text = "" + GameSetting.PlayerHp;
                PowerText.text = "" + GameSetting.PlayerPower;
                DefenceText.text = "" + GameSetting.PlayerDefnece;
                SpeedText.text = "" + GameSetting.PlayerSpeed;
                AttackRateText.text = "" + GameSetting.PlayerAttackRate;
                MaxMagText.text = "" + GameSetting.PlayerMaxMag;

                HpSlider.value = GameSetting.PlayerHp;
                PowerSlider.value = GameSetting.PlayerPower;
                DefenceSlider.value = GameSetting.PlayerDefnece;
                SpeedSlider.value = GameSetting.PlayerSpeed;
                AttackRateSlider.value = GameSetting.PlayerAttackRate;
                MaxMagSlider.value = GameSetting.PlayerMaxMag;
                GameManager.instance.Player = GameSetting.Player.Solider;
                break;
        }
    }

    public void GameStatrtButton()
    {
        DifficultyPanel.SetActive(true);
    }

    public void Easy()
    {
        GameManager.instance.difficulty = GameSetting.Difficulty.Easy;
    }

    public void Normal()
    {
        GameManager.instance.difficulty = GameSetting.Difficulty.Normal;
    }

    public void Hard()
    {
        GameManager.instance.difficulty = GameSetting.Difficulty.Hard;
    }

    public void StartGame()
    {
        SceneLoader.instance.LoadScene(CDefine.SCENE_NAME_SURVIALSHOOTER_GAMESCENE);
    }
}