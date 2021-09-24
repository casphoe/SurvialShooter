using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerSetting
{
    public string PlayerName;
    public float Hp;
    public float Power;
    public float Defence;
    public float Speed;
    public float AttackRate;
    public float MaxMagRate;
    public int Mine;

    public void PlayerDB(string Name,float hp,float attack,float De,float Sp, float Rate, int Remain,float Mag)
    {
        PlayerName = Name;
        Hp = hp;
        Power = attack;
        Defence = De;
        Speed = Sp;
        AttackRate = Rate;
        Mine = Remain;
        MaxMagRate = Mag;
    }
}

[System.Serializable]
public class EnemySetting
{
    public string EnemyName;
    public float Hp;
    public float Power;
    public float Defence;
    public float Speed;
    public int AddScore;
    public int BuffDrop;

    public void EnemyDB(string Name,float EnemyHp,float EnemyPower,float EnemyDefence,float speed,int Score,int Drop)
    {
        EnemyName = Name;
        Hp = EnemyHp;
        Power = EnemyPower;
        Defence = EnemyDefence;
        Speed = speed;
        AddScore = Score;
        BuffDrop = Drop;
    }
}

[System.Serializable]
public class BossSetting
{
    public string BossName;
    public float Hp;
    public float Power;
    public int Defence;
    public float Speed;
    public float AddScore;
}

[System.Serializable]
public class ItemSetting
{
    public string ItemName;
    public float HpRecover;
    public int MaxItemCount;
    public int Purchase;
    public float RemateTime;
    public float Damage;
    public float DamageUp;
    public int UpgradeCount;
    public bool IsPosionClear;

    public float TimeUp;
    public float BulletCount;
    public float AttackUp;

    public void ItemDB(string Name, float Recover,int ItCount,int pucharse,float time,float damage,float damageup, int UpCount, bool Clear,float UpTime,float Bullet,float Up)
    {
        ItemName = Name;
        HpRecover = Recover;
        MaxItemCount = ItCount;
        Purchase = pucharse;
        RemateTime = time;
        Damage = damage;
        DamageUp = damageup;
        UpgradeCount = UpCount;
        IsPosionClear = Clear;
        TimeUp = UpTime;
        BulletCount = Bullet;
        AttackUp = Up;
    }
}

[System.Serializable]
public class GameSetting
{
    //플레이어 설정
    public static string PlayerName;
    public static float PlayerHp;
    public static float PlayerPower;
    public static float PlayerDefnece;
    public static float PlayerSpeed;
    public static float PlayerAttackRate;
    public static int MineRemain;
    public static float PlayerMaxMag;
    //


    //적 설정
    public static string EnemyName;
    public static float EnemyHp;
    public static float EnemyPower;
    public static float EnemyDeffence;
    public static float EnemyMoveSpeed;
    public static int AddScore;
    public static int BuffDropValue;
    //

    public static float GameTime = 0;

    public static int Score = 0;
    public static int CoinCount = 10000;
    public static int DifficultyBuffMax = 0;
    public static int BossCreateNum = 0;

    public static float BGM;
    public static float Effect;

    public static bool IsGameOver = false;
    public static bool IsGameStart = false;
    public static bool IsStageClear = false;

    //아이템 설정
    public static string ItemName;
    public static float Recover;
    public static int MaxItem;
    public static int Purchase;
    public static float RemateTime;
    public static float Damage;
    public static float DamageUp;
    public static int UpgradeCount;
    public static bool IsPoisonClear;
    public static float TimeUp;
    public static float BulletCount;
    public static float AttackUp;
    //

    //총 총알 설정
    public static float SsgPower = 1.2f;
    public static float LaserPower = 1.5f;
    public static float ShotGunPower = 2.5f;
    public static float FireGunPower = 2f;
    
    public static int LaserGunBullet = 400;
    public static int ShotGunBullet = 60;
    public static int FireGunBullet = 400;
    public static int SsgGunBullet = 200;

    public static int SSgBullet = 50;
    public static int LaserBullet = 100;
    public static int ShotBullet = 5;
    public static int FireBullet = 100;

    public static float SsgRate = 0.9f;
    public static float AsultRate = 0.75f;
    public static float ShotGunRate = 0.6f;
    public static float FireGunRate = 0.5f;

    public static float SsgReload = 0.6f;
    public static float LaserReload = 1.5f;
    public static float ShotGunReload = 3f;
    public static float FireGunReload = 4f;
    //

    //총 업그레이드
    public static int SSgUpgrade = 0;
    public static int LaserUpgrade = 0;
    public static int ShotGunUpgrade = 0;
    public static int FireGunUpgrade = 0;

    public enum Player
    {
        Girl,Man,Solider
    }

    public enum Difficulty
    {
        Easy,Normal,Hard,VeryHard
    }

    public enum Lanaguage
    {
        Eng,Kor
    }

    public enum Item
    {
        HpRecover,Sheild,Mine,Bullet,GunLevelUp,Scissors
    }

    public enum Buff
    {
        AttackUp,Sheild
    }

    public enum Guns
    {
        Ssg,Laser,ShotGun,FireGun
    }

    public static void DifficultyStageTime()
    {
        switch(GameManager.instance.difficulty)
        {
            case Difficulty.Easy:
                GameTime = 200f;
                DifficultyBuffMax = 10;
                BossCreateNum = 2;
                break;
            case Difficulty.Normal:
                GameTime = 150f;
                DifficultyBuffMax = 8;
                BossCreateNum = 3;
                break;
            case Difficulty.Hard:
                GameTime = 120f;
                DifficultyBuffMax = 6;
                BossCreateNum = 4;
                break;
            case Difficulty.VeryHard:
                DifficultyBuffMax = 4;
                BossCreateNum = 4;
                break;
        }
    }
}
/*
 * 게임 객체에 한개만 존재하는 스크립트이고 어떤 클래스에서도 접근이 가능해서 필요한 기능을 사용하게 할 수 있음
 */
public class GameManager : Singleton<GameManager>
{

    public Dictionary<string, PlayerSetting> Players = new Dictionary<string, PlayerSetting>();
    public Dictionary<string, ItemSetting> Items = new Dictionary<string, ItemSetting>();
    public Dictionary<string, EnemySetting> Enemys = new Dictionary<string, EnemySetting>();


    public GameSetting.Difficulty difficulty = GameSetting.Difficulty.Easy;
    public GameSetting.Player Player;
    public GameSetting.Lanaguage LG;
    public GameSetting.Item item;
    public GameSetting.Guns Gun;

    public Sprite[] SoundImages = new Sprite[2];

    public Image Sound;

    public bool IsSoundOff = false;

    public override void Awake()
    {
        base.Awake();
        PlayerData();
        PlayerMGR();
        ItemData();
        ItemMGR();
        EnemyData();
        //EnemyMGR();

        GameSetting.BGM = PlayerPrefs.GetFloat("BGM", GameSetting.BGM);
        GameSetting.Effect = PlayerPrefs.GetFloat("Effect", GameSetting.Effect);
        
        /*
         * PlayerPrefs 함수는 string,float,int 함수만 저장 불려오기가 가능하다. bool형을 받아오기 위해서는 
         * bool 형식을 int로 사용하거나 또는 bool형을 유지하게 string으로 형변환 해서 저장한다음 stirng 함수에 대입
         * string 함수를 boolean형으로 형변환 시켜줘야 합니다.
         */
        string IsSoundOnOFF = PlayerPrefs.GetString("Sound", IsSoundOff.ToString());
        IsSoundOff = System.Convert.ToBoolean(IsSoundOnOFF);
    }

    public void StartButton()
    {
        SceneLoader.instance.LoadScene(CDefine.SCENE_NAME_SURVIALSHOOTER_PLAYERSELECT);
    }

    public void ExitButton()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    void PlayerData()
    {
        PlayerSetting Girl = new PlayerSetting();
        Girl.PlayerDB("Girl", 180, 10, 4, 6, 1.5f,3,1);
        Players.Add(Girl.PlayerName, Girl);

        PlayerSetting Man = new PlayerSetting();
        Man.PlayerDB("Man", 240, 15, 6, 4, 1.2f,2,0.75f);
        Players.Add(Man.PlayerName, Man);

        PlayerSetting Solider = new PlayerSetting();
        Solider.PlayerDB("Solider", 300, 20, 8, 2, 0.9f,1,0.5f);
        Players.Add(Solider.PlayerName, Solider);
    }

    void ItemData()
    {
        ItemSetting Item1 = new ItemSetting();
        Item1.ItemDB("HpRecover",0.2f,0,100,0,0,0,0,false,0,0,0);
        Items.Add(Item1.ItemName, Item1);

        ItemSetting Item2 = new ItemSetting();
        Item2.ItemDB("Shield", 0, 0, 250, 7, 0, 0, 0, false, 0,0,0);
        Items.Add(Item2.ItemName, Item2);

        ItemSetting Item3 = new ItemSetting();
        Item3.ItemDB("Mine", 0, 3, 150, 0, 100, 0, 0, false, 0,0,0);
        Items.Add(Item3.ItemName, Item3);

        ItemSetting Item4 = new ItemSetting();
        Item4.ItemDB("GunLevelUp", 0, 0, 150, 0, 0, 0.1f, 5, false, 0,0,0);
        Items.Add(Item4.ItemName, Item4);

        ItemSetting Item5 = new ItemSetting();
        Item5.ItemDB("Bullet", 0, 0, 50, 0, 0, 0, 0, false, 0,0.5f,0);
        Items.Add(Item5.ItemName, Item5);

        ItemSetting Item6 = new ItemSetting();
        Item6.ItemDB("Scissors", 0, 0, 50, 0, 0, 0, 0, true, 0, 0,0);
        Items.Add(Item6.ItemName, Item6);

        ItemSetting Item7 = new ItemSetting();
        Item7.ItemDB("AttackUp", 0, 0, 0, 10, 0, 0, 0, false, 0, 0,0.5f);
        Items.Add(Item7.ItemName, Item7);

        ItemSetting Item8 = new ItemSetting();
        Item8.ItemDB("TimeUp", 0, 0, 0, 0, 0, 0, 0, false, 30f, 0,0);
        Items.Add(Item8.ItemName, Item8);
    }
    //난이도 Easy(쉬움)에 맞추어서 적들의 스텟을 구성
    void EnemyData()
    {
        EnemySetting Enemy1 = new EnemySetting();
        Enemy1.EnemyDB("ZomBunny", 60, 10, 2, 0.8f, 10, 1);
        Enemys.Add(Enemy1.EnemyName, Enemy1);

        EnemySetting Enemy2 = new EnemySetting();
        Enemy2.EnemyDB("ZomBear",75,12,4,0.6f,20, 1);
        Enemys.Add(Enemy2.EnemyName, Enemy2);

        EnemySetting Enemy3 = new EnemySetting();
        Enemy3.EnemyDB("ZomHellephant", 90, 16, 8, 0.5f, 40, 1);
        Enemys.Add(Enemy3.EnemyName, Enemy3);
    }

    public Dictionary<string,PlayerSetting> PlayerMGR()
    {
        return Players;
    }

    public Dictionary<string,ItemSetting> ItemMGR()
    {
        return Items;
    }

    public Dictionary<string, EnemySetting> EnemyMGR()
    {
        return Enemys;
    }

    public void PlayerStat(string Name)
    {
        PlayerSetting Ps;
        Players.TryGetValue(string.Format("{0}", Name), out Ps);

        GameSetting.PlayerName = Ps.PlayerName;
        GameSetting.PlayerHp = Ps.Hp;
        GameSetting.PlayerPower = Ps.Power;
        GameSetting.PlayerDefnece = Ps.Defence;
        GameSetting.PlayerSpeed = Ps.Speed;
        GameSetting.PlayerAttackRate = Ps.AttackRate;
        GameSetting.MineRemain = Ps.Mine;
        GameSetting.PlayerMaxMag = Ps.MaxMagRate;
    }

    public void ItemStat(string Name)
    {
        ItemSetting It;
        Items.TryGetValue(string.Format("{0}", Name), out It);

        GameSetting.ItemName = It.ItemName;
        GameSetting.Recover = It.HpRecover;
        GameSetting.MaxItem = It.MaxItemCount;
        GameSetting.Purchase = It.Purchase;
        GameSetting.RemateTime = It.RemateTime;
        GameSetting.Damage = It.Damage;
        GameSetting.DamageUp = It.DamageUp;
        GameSetting.UpgradeCount = It.UpgradeCount;
        GameSetting.IsPoisonClear = It.IsPosionClear;
        GameSetting.TimeUp = It.TimeUp;
        GameSetting.BulletCount = It.BulletCount;
        GameSetting.AttackUp = It.AttackUp;
    }

    public void EnemyStat(string Name)
    {
        EnemySetting enemys;
        Enemys.TryGetValue(string.Format("{0}", Name), out enemys);

        GameSetting.EnemyName = enemys.EnemyName;
        GameSetting.EnemyHp = enemys.Hp;
        GameSetting.EnemyPower = enemys.Power;
        GameSetting.EnemyDeffence = enemys.Defence;
        GameSetting.EnemyMoveSpeed = enemys.Speed;
        GameSetting.AddScore = enemys.AddScore;
        GameSetting.BuffDropValue = enemys.BuffDrop;
    }

    public void IsSoundOnOff()
    {
        IsSoundOff = !IsSoundOff;

        if(IsSoundOff)
        {
            Sound.sprite = SoundImages[1];
            GameSetting.BGM = 1;
            GameSetting.Effect = 1;
        }
        else
        {
            Sound.sprite = SoundImages[0];
            GameSetting.BGM = 0;
            GameSetting.Effect = 0;
        }

        PlayerPrefs.SetFloat("BGM", GameSetting.BGM);
        PlayerPrefs.SetFloat("Effect", GameSetting.Effect);
        PlayerPrefs.SetString("Sound", IsSoundOff.ToString());
    }
}