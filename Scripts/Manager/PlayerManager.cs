using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : CCompoent
{
    public Slider HpSlider;

    public Text HpText;

    public Text Score;
    public Text GameClockText;
    public Text CoinCount;
    public Text PurchaseText;
    public Text MineReamin;
    public Text BulltText;

    public static PlayerManager instance;

    public GameObject PausePanel;
    public GameObject ShopPanel;
    public GameObject PurchasePanel;
    public GameObject PlayerCreatePoint;
    public GameObject CreateMinePosition;
    public GameObject DamageCreatePosition;
    public GameObject bloodParticlePoistion;
    public Transform AmmuntionTrans;
    public GameObject[] Buff = new GameObject[2];

    public GameObject[] Players = new GameObject[3];
    public Collider[] Colls;

    public float[] GunDamage = new float[4];

    public Button Mine;

    public float GT;

    public int Minute = 0;

    public Sprite[] GunSprite = new Sprite[4];
    public Player P;

    public Vector3 PlayerPosition;

    public float PlayerHp;
    public int MineCount;
    public float Damage;
    public float AttackRate;
    public bool bleeding;
    public bool IsInvincibility;
    public float InvincibilityTime;

    public float DamageAmout;
    public float BloodDamageAmout;

    public int LaserGunBullet;
    public int ShotgunBullet;
    public int FireGunBullet;
    public int SsgGunBullet;

    public int SsgBullet;
    public int LaserBullet;
    public int ShotBullet;
    public int FireBullet;

    public bool IsShotStick = false;
    public bool IsMoveStick = false;
    public bool IsMineStep = false;

    public List<GameObject> MineDamageCount = new List<GameObject>();

    private int Count;
    float DebuffTime = 0;

    public override void Awake()
    {
        base.Awake();

        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            return;
        }
        PlayerStat();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameSetting.DifficultyStageTime();
        PlayerHp = GameSetting.PlayerHp;
        HpSlider.maxValue = GameSetting.PlayerHp;
        HpSlider.value = GameSetting.PlayerHp;
        PausePanel.SetActive(false);
        ShopPanel.SetActive(false);
        PurchasePanel.SetActive(false);
        GT = GameSetting.GameTime;
        LaserGunBullet = GameSetting.LaserGunBullet - GameSetting.LaserBullet;
        ShotgunBullet = GameSetting.ShotGunBullet - GameSetting.ShotBullet;
        FireGunBullet = GameSetting.FireGunBullet - GameSetting.FireBullet;
        MineCount = GameSetting.MineRemain;
        Damage = GameSetting.PlayerPower + GameSetting.SsgPower;
        AttackRate = GameSetting.PlayerAttackRate - GameSetting.SsgRate;
        CreatePlayer(PlayerCreatePoint.transform);
        LaserBullet = GameSetting.LaserBullet;
        ShotBullet = GameSetting.ShotBullet;
        FireBullet = GameSetting.FireBullet;
        SsgBullet = GameSetting.SSgBullet;
        BulltText.gameObject.SetActive(false);
        ObjectPool.instance.PlayerDamageTextCount(9);
        ObjectPool.instance.BloddDamgeTextCount(9);
        for (int i = 0; i < Buff.Length; i++)
        {
            Buff[i].SetActive(false);
        }
    }

    private void Update()
    {
        if(GameSetting.IsGameStart == true)
        {
            if(GameSetting.IsGameOver == false)
            {
                if(GT != 0)
                {
                    GT -= Time.deltaTime;
                    if(GT >= 60)
                    {
                        Minute++;
                        GT -= 60;
                    }
                    if(GT < 0)
                    {
                        if(Minute > 0)
                        {
                            Minute--;
                            GT += 60;
                        }
                        if(GT <= 0)
                        {
                            GT = 0;
                            GameSetting.IsGameOver = true;
                        }
                    }
                }
            }
        }
        GameClockText.text = string.Format("{0:00} : {1:00}", Minute, (int)GT);

        Score.text = string.Format("{0:#,0}",GameSetting.Score);
        CoinCount.text = string.Format("{0:#,0}", GameSetting.CoinCount);
        MineReamin.text = "x " + MineCount;
        HpText.text = "" + PlayerHp;
        if (MineCount > 0)
        {
            Mine.interactable = true;
        }
        else
        {
            Mine.interactable = false;
        }
       

        if(bleeding == true)
        {
            IsBleeding();
        }

        if(GameSetting.IsGameOver == true)
        {
            PlayerPrefs.SetInt("Coin", GameSetting.CoinCount);
            PlayerPrefs.SetInt("Score", GameSetting.Score);           
        }
    }


    public void PlayerStat()
    {
        switch(GameManager.instance.Player)
        {
            case GameSetting.Player.Girl:
                GameManager.instance.PlayerStat("Girl");
                break;
            case GameSetting.Player.Man:
                GameManager.instance.PlayerStat("Man");
                GameSetting.LaserGunBullet -= GameSetting.LaserGunBullet / 4;
                GameSetting.FireGunBullet  -= GameSetting.FireGunBullet / 4;
                GameSetting.ShotGunBullet -= GameSetting.ShotGunBullet / 4;
                SsgGunBullet = GameSetting.SsgGunBullet - GameSetting.SSgBullet;
                break;
            case GameSetting.Player.Solider:
                GameSetting.LaserGunBullet -= GameSetting.LaserGunBullet / 2;
                GameSetting.FireGunBullet -= GameSetting.FireGunBullet / 2;
                GameSetting.ShotGunBullet -= GameSetting.ShotGunBullet / 2;
                SsgGunBullet = GameSetting.SsgGunBullet / 2;
                GameManager.instance.PlayerStat("Solider");
                break;
        }
    }

    public void TakeDamage(float Amout)
    {
        
        PlayerHp -= Amout;
        HpSlider.value -= Amout;
        int BleedingValue = Random.Range(0, 10);
        DamageAmout = Amout;
        P.Invisble();
        Vector3 Uipoistion = Camera.main.WorldToScreenPoint(DamageCreatePosition.transform.position);
        ObjectPool.instance.CreatePlayerDamageEffect(Uipoistion, Vector3.zero);

        if (BleedingValue < 2)
        {
            bleeding = true;
        }

        if(PlayerHp <= 0)
        {       
            GameSetting.IsGameOver = true;
            P.PlayerAnim.SetTrigger("Die");

            //게임 오버 사운드와 플레이어 죽는 소리 실행
        }
    }

    public void IsBleeding()
    {        
        if(GameSetting.IsGameOver == false)
        {
            DebuffTime += Time.deltaTime;
            if (DebuffTime >= 2f)
            {
                BloodDamageAmout = (PlayerHp * 0.05f);
                PlayerHp = PlayerHp - (PlayerHp * 0.05f);
                Vector3 Uipoistion = Camera.main.WorldToScreenPoint(DamageCreatePosition.transform.position);
                ObjectPool.instance.CreateBloodTextEffect(Uipoistion, Vector3.zero);
                HpSlider.value = PlayerHp;
                DebuffTime = 0f;
            }
            /*
             * 부모 오브젝트 영향을 받아서 내가 비활성상태인지 체크하기 위해서 activeInHierarchy을 사용
             * activeSelf가 true여도 부모가 false면 activeInHierarchy 는 false
             */
            if (ObjectPool.instance.BloodParticleObject.activeInHierarchy == false)
            {
                ObjectPool.instance.CreateBloodParticle(bloodParticlePoistion.transform.position, Vector3.zero);
            }
        }
        else
        {
            return;
        }
    }

    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Cancel()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneLoader.instance.LoadScene(CDefine.SCENE_NAME_SURVIALSHOOTER_GAMESCENE);
    }

    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.instance.LoadScene(CDefine.SCENE_NAME_SURVIALSHOOTER_STARTSCENE);
    }

    public void PlayerSelectScene()
    {
        Time.timeScale = 1f;
        SceneLoader.instance.LoadScene(CDefine.SCENE_NAME_SURVIALSHOOTER_PLAYERSELECT);
    }

    public void ShopButton()
    {
        Time.timeScale = 0f;
        ShopPanel.SetActive(true);
    }

    public void ShopCancelButton()
    {
        Time.timeScale = 1f;
        ShopPanel.SetActive(false);
    }

    public void PurchaseButton()
    {
        PurchaseText.gameObject.SetActive(true);
        PurchaseText.color = new Color(1, 1, 1, 1);
        if(GameSetting.CoinCount >= GameSetting.Purchase)
        {
            switch(GameManager.instance.item)
            {
                case GameSetting.Item.GunLevelUp:
                    switch(GameManager.instance.Gun)
                    {
                        case GameSetting.Guns.Ssg:
                            if(GameSetting.SSgUpgrade >= GameSetting.UpgradeCount)
                            {
                                return;
                            }
                            else
                            {                               
                                GameSetting.CoinCount -= GameSetting.Purchase;
                                GameSetting.SsgPower += (GameSetting.DamageUp * GameSetting.SsgPower);
                                Damage += GameSetting.SsgPower;
                                GunDamage[0] = Damage;
                                GameSetting.SSgUpgrade += 1;
                            }
                            break;
                        case GameSetting.Guns.Laser:
                            if (GameSetting.LaserUpgrade >= GameSetting.UpgradeCount)
                            {
                                return;
                            }
                            else
                            {
                                GameSetting.CoinCount -= GameSetting.Purchase;
                                GameSetting.LaserPower += (GameSetting.DamageUp * GameSetting.LaserPower);
                                Damage += GameSetting.LaserPower;
                                GunDamage[1] = Damage;
                                GameSetting.LaserUpgrade += 1;
                            }
                            break;
                        case GameSetting.Guns.ShotGun:
                            if (GameSetting.ShotGunUpgrade >= GameSetting.UpgradeCount)
                            {
                                return;
                            }
                            else
                            {
                                GameSetting.CoinCount -= GameSetting.Purchase;
                                GameSetting.ShotGunPower += (GameSetting.DamageUp * GameSetting.ShotGunPower);
                                Damage += GameSetting.ShotGunPower;
                                GunDamage[2] = Damage;
                                GameSetting.ShotGunUpgrade += 1;
                            }
                            break;
                        case GameSetting.Guns.FireGun:
                            if (GameSetting.FireGunUpgrade >= GameSetting.UpgradeCount)
                            {
                                return;
                            }
                            else
                            {
                                GameSetting.CoinCount -= GameSetting.Purchase;
                                GameSetting.FireGunPower += (GameSetting.DamageUp * GameSetting.FireGunPower);
                                Damage += GameSetting.FireGunPower;
                                GunDamage[3] = Damage;
                                GameSetting.FireGunUpgrade += 1;
                            }
                            break;
                    }
                    break;
                case GameSetting.Item.HpRecover:
                    if(PlayerHp >= GameSetting.PlayerHp)
                    {
                        return;
                    }
                    else
                    {
                        GameSetting.CoinCount -= GameSetting.Purchase;
                        PlayerHp = PlayerHp + (GameSetting.PlayerHp * GameSetting.Recover);
                        if(PlayerHp >= GameSetting.PlayerHp)
                        {
                            PlayerHp = GameSetting.PlayerHp;
                        }
                        HpSlider.value = HpSlider.value + (GameSetting.PlayerHp * GameSetting.Recover);
                    }
                    break;
                case GameSetting.Item.Mine:
                    if(MineCount >= GameSetting.MaxItem)
                    {
                        return;
                    }
                    else
                    {
                        GameSetting.CoinCount -= GameSetting.Purchase;
                        MineCount += 1;
                    }
                    break;
                case GameSetting.Item.Scissors:
                    if(bleeding == false)
                    {
                        return;
                    }
                    else
                    {
                        GameSetting.CoinCount -= GameSetting.Purchase;
                        bleeding = !GameSetting.IsPoisonClear;
                    }
                    break;
                case GameSetting.Item.Sheild:
                    {
                        if(IsInvincibility == true)
                        {
                            return;
                        }
                        else
                        {
                            GameSetting.CoinCount -= GameSetting.Purchase;
                            InvincibilityTime = GameSetting.RemateTime;
                            Buff[0].SetActive(true);
                        }
                    }
                    break;
                case GameSetting.Item.Bullet:
                    switch(GameManager.instance.Gun)
                    {
                        case GameSetting.Guns.Ssg:
                            if(GameManager.instance.Player == GameSetting.Player.Girl)
                            {
                                return;
                            }
                            else if(GameManager.instance.Player == GameSetting.Player.Man)
                            {
                                if(SsgGunBullet >= GameSetting.SsgGunBullet)
                                {
                                    return;
                                }
                                else
                                {
                                    GameSetting.CoinCount -= GameSetting.Purchase;
                                    if(SsgBullet < GameSetting.SSgBullet)
                                    {
                                        if(SsgBullet <= 0)
                                        {
                                            if(SsgGunBullet > 0)
                                            {
                                                SsgGunBullet = SsgGunBullet + (GameSetting.SsgGunBullet / 2);
                                            }
                                            else
                                            {
                                                SsgGunBullet = (GameSetting.SsgGunBullet / 2) - GameSetting.SSgBullet;
                                            }
                                        }
                                        else
                                        {
                                            if(SsgGunBullet > 0)
                                            {
                                                SsgGunBullet = SsgGunBullet + ((GameSetting.SsgGunBullet / 2) - SsgBullet);
                                            }
                                            else
                                            {
                                                SsgGunBullet -= ((GameSetting.SsgGunBullet / 2) - SsgGunBullet);
                                            }
                                        }
                                    }
                                    SsgBullet = GameSetting.SSgBullet;
                                    if(SsgGunBullet >= (GameSetting.SsgGunBullet - GameSetting.SSgBullet))
                                    {
                                        SsgGunBullet = GameSetting.SsgGunBullet - GameSetting.SSgBullet;
                                    }
                                }
                            }
                            else if(GameManager.instance.Player == GameSetting.Player.Solider)
                            {
                                if (SsgGunBullet >= GameSetting.SsgGunBullet)
                                {
                                    return;
                                }
                                else
                                {
                                    GameSetting.CoinCount -= GameSetting.Purchase;
                                    if (SsgBullet < GameSetting.SSgBullet)
                                    {
                                        if (SsgBullet <= 0)
                                        {
                                            if (SsgGunBullet > 0)
                                            {
                                                SsgGunBullet = SsgGunBullet + (GameSetting.SsgGunBullet / 2);
                                            }
                                            else
                                            {
                                                SsgGunBullet = (GameSetting.SsgGunBullet / 2) - GameSetting.SSgBullet;
                                            }
                                        }
                                        else
                                        {
                                            if (SsgGunBullet > 0)
                                            {
                                                SsgGunBullet = SsgGunBullet + ((GameSetting.SsgGunBullet / 2) - SsgBullet);
                                            }
                                            else
                                            {
                                                SsgGunBullet -= ((GameSetting.SsgGunBullet / 2) - SsgGunBullet);
                                            }
                                        }
                                    }
                                    SsgBullet = GameSetting.SSgBullet;
                                    if (SsgGunBullet >= (GameSetting.SsgGunBullet - GameSetting.SSgBullet))
                                    {
                                        SsgGunBullet = GameSetting.SsgGunBullet - GameSetting.SSgBullet;
                                    }
                                }
                            }
                            break;
                        case GameSetting.Guns.Laser:
                            if(LaserGunBullet >= GameSetting.LaserGunBullet)
                            {
                                return;
                            }
                            else //샷건 총알 구매 기능
                            {
                                GameSetting.CoinCount -= GameSetting.Purchase; //코인 감소                      
                                if (LaserBullet < GameSetting.LaserBullet) // 레이저 총알이 설정된 총알보다 작을 때 
                                {
                                    if (LaserBullet <= 0) //레이저 총알이 아에 없을 때
                                    {
                                        if (LaserGunBullet > 0) //레이저 탄창의 탄약이 존재 했을 경우 
                                        {
                                            LaserGunBullet = LaserGunBullet + (GameSetting.LaserGunBullet / 2);
                                        }
                                        else //존재 안 했을 경우
                                        {
                                            LaserGunBullet = (GameSetting.LaserGunBullet / 2) - GameSetting.LaserBullet;
                                        }
                                    }
                                    else
                                    {
                                        if (LaserGunBullet > 0)
                                        {
                                            LaserGunBullet = LaserGunBullet + ((GameSetting.LaserGunBullet / 2) - LaserBullet);
                                        }
                                        else
                                        {
                                            LaserGunBullet -= ((GameSetting.LaserGunBullet / 2) - LaserBullet);
                                        }
                                    }
                                }
                                LaserBullet = GameSetting.LaserBullet;
                                if (LaserGunBullet >= (GameSetting.LaserGunBullet - GameSetting.LaserBullet))
                                {
                                    LaserGunBullet = GameSetting.LaserGunBullet - GameSetting.LaserBullet;
                                }
                            }
                            break;
                        case GameSetting.Guns.ShotGun:
                            if(ShotgunBullet >= GameSetting.ShotGunBullet) //샷건 탄창이 최대 탄창보다 클 때 return을 해서 빠져나옴
                            {
                                return;
                            }
                            else //샷건 총알 구매 기능
                            {
                                GameSetting.CoinCount -= GameSetting.Purchase; //코인 감소                      
                                if(ShotBullet < GameSetting.ShotBullet) // 샷건 총알이 설정된 총알보다 작을 때 
                                {                                   
                                    if (ShotBullet <= 0) //샷건 총알이 아에 없을 때
                                    {
                                        if(ShotgunBullet > 0) //샷건 탄창의 탄약이 존재 했을 경우 
                                        {
                                            ShotgunBullet = ShotgunBullet + (GameSetting.ShotGunBullet / 2);
                                        }
                                        else //존재 안 했을 경우
                                        {
                                            ShotgunBullet = (GameSetting.ShotGunBullet / 2) - GameSetting.ShotBullet;
                                        }                                                                               
                                    }
                                    else
                                    {
                                        if(ShotgunBullet > 0)
                                        {
                                            ShotgunBullet = ShotgunBullet + ((GameSetting.ShotGunBullet / 2) - ShotBullet);
                                        }
                                        else
                                        {
                                            ShotgunBullet -= ((GameSetting.ShotGunBullet / 2) - ShotBullet);
                                        }                       
                                    }
                                }
                                ShotBullet = GameSetting.ShotBullet;
                                if (ShotgunBullet >= (GameSetting.ShotGunBullet - GameSetting.ShotBullet))
                                {
                                    ShotgunBullet = GameSetting.ShotGunBullet - GameSetting.ShotBullet;
                                }
                            }
                            break;
                        case GameSetting.Guns.FireGun:
                            if(FireGunBullet >= GameSetting.FireGunBullet)
                            {
                                return;
                            }
                            else //샷건 총알 구매 기능
                            {
                                GameSetting.CoinCount -= GameSetting.Purchase; //코인 감소                      
                                if (FireBullet < GameSetting.FireBullet) // 화염방사기 탄약이 설정된 탄약보다 작을 때 
                                {
                                    if (FireBullet <= 0) //화염 방사기 탄약이 아에 없을 때
                                    {
                                        if (FireGunBullet > 0) //화염 방사기 탄창의 탄약이 존재 했을 경우 
                                        {
                                            FireGunBullet = FireGunBullet + (GameSetting.FireGunBullet / 2);
                                        }
                                        else //존재 안 했을 경우
                                        {
                                            FireGunBullet = (GameSetting.FireGunBullet / 2) - GameSetting.FireBullet;
                                        }
                                    }
                                    else
                                    {
                                        if (FireGunBullet > 0)
                                        {
                                            FireGunBullet = FireGunBullet + ((GameSetting.FireGunBullet / 2) - FireBullet);
                                        }
                                        else
                                        {
                                            FireGunBullet -= ((GameSetting.FireGunBullet / 2) - FireBullet);
                                        }
                                    }
                                }
                                FireBullet = GameSetting.FireBullet;
                                if (FireGunBullet >= (GameSetting.FireGunBullet - GameSetting.FireBullet))
                                {
                                    FireGunBullet = GameSetting.FireGunBullet - GameSetting.FireBullet;
                                }
                            }
                            break;
                    }
                    break;
            }
        }
        else
        {
            Count = GameSetting.Purchase - GameSetting.CoinCount;
            switch (GameManager.instance.LG)
            {
                case GameSetting.Lanaguage.Eng:
                    PurchaseText.text = "There is not enough " + Count + " coins to purchase";
                    break;
                case GameSetting.Lanaguage.Kor:
                    PurchaseText.text = "구매의 필요한 동전이 " + Count + " 부족합니다";
                    break;
            }
        }
    }

    public void PurchaseCancelButton()
    {
        PurchasePanel.SetActive(false);
    }

    private void CreatePlayer(Transform Pos)
    {
        GameObject player;
        switch(GameManager.instance.Player)
        {
            case GameSetting.Player.Girl:
                player = Instantiate(Players[0], Pos.position, Quaternion.identity);
                break;
            case GameSetting.Player.Man:
                player = Instantiate(Players[1], Pos.position, Quaternion.identity);
                break;
            case GameSetting.Player.Solider:
                player = Instantiate(Players[2], Pos.position, Quaternion.identity);
                break;
        }
    }
}