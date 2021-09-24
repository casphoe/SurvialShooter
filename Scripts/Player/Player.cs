using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CCompoent
{
    
    public float Defecne;
    public float Speed;
    public float ReloadTime;
    public float[] AttackRate = new float[4];

    public bool IsReload = false;

    private MoveJoyStick Mj;
    private ShotJoyStick Sj;
    private PlayerShooting Shooting;

    public bool IsPcMove;

    public Animator PlayerAnim;

    public float SsgRe;
    public float LaserRe;
    public float ShotGunRe;
    public float FireGunRe;

    public bool IsSSgReload = true;
    public bool IsLaserReload = true;
    public bool IsShotGunReload = true;
    public bool IsFireGunReload = true;
    public GameObject createDamagePosition;
    public GameObject createbloodParticle;

    public GameObject MineCreatePosition;
    public GameObject createAmmuntionPosition;

    public Transform BulletAmmuntionTrans;
    void Start()
    {
        Defecne = GameSetting.PlayerDefnece;
        Speed = GameSetting.PlayerSpeed;
        ReloadTime = 0f;
        Mj = GameObject.Find("MoveJoyStick").GetComponent<MoveJoyStick>();
        Sj = GameObject.Find("ShotJoyStick").GetComponent<ShotJoyStick>();
        Shooting = GetComponent<PlayerShooting>();
        PlayerAnim = GetComponent<Animator>();
        SsgRe = GameSetting.SsgReload;
        LaserRe = GameSetting.LaserReload;
        ShotGunRe = GameSetting.ShotGunReload;
        FireGunRe = GameSetting.FireGunReload;
        IsReload = false;
        ObjectPool.instance.MineCount(15);
        ObjectPool.instance.GunAmmuntion(120);
        ObjectPool.instance.ShotGunAmmution(75);
        ObjectPool.instance.FireGunAmmution(50);
        ObjectPool.instance.ParticeleExpersion(15);
        ObjectPool.instance.BloodParticleCount(2);

        PlayerManager.instance.CreateMinePosition = MineCreatePosition;
        PlayerManager.instance.P = GetComponent<Player>();
        PlayerManager.instance.DamageCreatePosition = createDamagePosition;
        PlayerManager.instance.bloodParticlePoistion = createbloodParticle;
        PlayerManager.instance.AmmuntionTrans = BulletAmmuntionTrans;
        IsPcMove = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move();

        if(GameSetting.IsGameOver == false || GameSetting.IsGameStart == true)
        {
            MoblieMove();
            MoblieTurn();
            PcMoveBool();
            PcTurn();
            PcMoveAnimator();
            if (IsReload == true)
            {
                ReloadTime += Time.deltaTime;

            }
            ShotAndReload();
        }
        PlayerManager.instance.PlayerPosition = transform.position;
    }

    void Move()
    {
#if UNITY_STANDALONE
        PcMoveBool();
        PcMoveAnimator();
        PcTurn();
#elif UNITY_ANDROID
        MoblieMove();
        MoblieTurn();
#endif
    }

    void PcMoveBool()
    {
        if(Input.GetKey(KeyCode.W))
        {
            IsPcMove = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            IsPcMove = true;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            IsPcMove = true;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            IsPcMove = true;
        }
        else
        {
            IsPcMove = false;
        }
    }

    void PcMoveAnimator()
    {
        if(IsPcMove == true)
        {
            PlayerAnim.SetBool("Walking", true);
            PcMove();
        }
        else
        {
            PlayerAnim.SetBool("Walking", false);
        }
    }

    void PcMove()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * Speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position -= Vector3.left * Speed * Time.deltaTime;
        }
    }

    void PcTurn()
    {
        //마우스 위치를 ScreenPointToRay를 이용해 카메라로 부터 스크린의 점을 통해 레이를 반환
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //월드 좌표로 하늘 방향에 크기가 1인 단위 백터와 원점을 갖음
        Plane groupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        //레이가 평면과 교차했는지 여부를 체크
        if(groupPlane.Raycast(camray,out rayLength))
        {
            Vector3 pointTolook = camray.GetPoint(rayLength); //위치값을 반환

            //pointTolook 의 위치값을 캐릭터가 바라보도록 함
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }
    }

    public void Invisble()
    {
        PlayerManager.instance.IsInvincibility = true;
        Invoke("UnInvisble", 2f);
    }

    public void UnInvisble()
    {
        PlayerManager.instance.IsInvincibility = false;
    }

    void MoblieMove()
    {
        if(PlayerManager.instance.IsMoveStick == true)
        {
            Vector3 UpMoveMent = Vector3.right * Speed * Time.deltaTime * Mj.Vertical;
            Vector3 rightMovement = Vector3.forward * Speed * Time.deltaTime * Mj.Horizontal;

            transform.position += UpMoveMent;
            transform.position += rightMovement;
            PlayerAnim.SetBool("Walking", true);
        }
        else
        {
            PlayerAnim.SetBool("Walking", false);
        }
    }

    void MoblieTurn()
    {
        if(PlayerManager.instance.IsShotStick == true)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, Mathf.Atan2(Sj.Vertical, Sj.Horizontal) * Mathf.Rad2Deg, 0);
        }
    }

    void ShotAndReload()
    {
        switch (GameManager.instance.Gun)
        {
            case GameSetting.Guns.Ssg:
                PlayerManager.instance.BulltText.gameObject.SetActive(false);
                if(IsSSgReload == true)
                {
                    AttackRate[0] += Time.deltaTime;
                }               
                if (AttackRate[0] >= PlayerManager.instance.AttackRate)
                {
                    if (PlayerManager.instance.SsgBullet > 0)
                    {
                        PlayerManager.instance.SsgBullet -= 1;
                        Shooting.BasicGunShooting();
                        Shooting.SsgGunColorSetting();
                        ObjectPool.instance.CreateAmmuntionSystem(createAmmuntionPosition.transform.position, new Vector3(-15f,0f,0f));
                        if (Shooting.ShootEffectTimer >= Shooting.EffectsDisplayTime)
                        {
                            Shooting.Disableeffects();
                        }
                        AttackRate[0] = 0f;                        
                    }
                    else
                    {
                        IsReload = true;
                        IsSSgReload = false;
                    }
                }
                if (ReloadTime >= SsgRe)
                {
                    //재장전 되는 탄약 파티클, 재장전 소리 실행
                    PlayerManager.instance.SsgBullet = GameSetting.SSgBullet;
                    IsReload = false;
                    IsSSgReload = true;
                    ReloadTime = 0f;
                    if (GameManager.instance.Player == GameSetting.Player.Girl)
                    {
                        return;
                    }
                    else
                    {
                        PlayerManager.instance.SsgGunBullet -= GameSetting.SSgBullet;
                    }
                }
                if (GameManager.instance.Player == GameSetting.Player.Girl)
                {
                    return;
                }
                else
                {
                    if(PlayerManager.instance.SsgBullet <= 0 && PlayerManager.instance.SsgGunBullet <= 0)
                    {
                        PlayerManager.instance.BulltText.gameObject.SetActive(true);
                    }
                }
                break;
            case GameSetting.Guns.Laser:
                if (IsLaserReload == true)
                {
                    AttackRate[1] += Time.deltaTime;
                }
                if (AttackRate[1] >= PlayerManager.instance.AttackRate)
                {
                    if (PlayerManager.instance.LaserBullet > 0)
                    {
                        PlayerManager.instance.BulltText.gameObject.SetActive(false);
                        PlayerManager.instance.LaserBullet -= 1;
                        ObjectPool.instance.CreateAmmuntionSystem(createAmmuntionPosition.transform.position, new Vector3(-15f, 0f, 0f));
                        AttackRate[1] = 0;

                        if (PlayerManager.instance.LaserGunBullet > 0 && PlayerManager.instance.LaserBullet == 0)
                        {
                            IsReload = true;
                            IsLaserReload = false;
                        }
                    }
                }
                if (ReloadTime >= LaserRe)
                {
                    PlayerManager.instance.LaserBullet = GameSetting.LaserBullet;
                    PlayerManager.instance.LaserGunBullet -= GameSetting.LaserBullet;
                    IsReload = false;
                    IsLaserReload = true;
                    ReloadTime = 0f;
                }
                if (PlayerManager.instance.LaserBullet <= 0 && PlayerManager.instance.LaserGunBullet <= 0)
                {
                    PlayerManager.instance.BulltText.gameObject.SetActive(true);
                }
                break;
            case GameSetting.Guns.ShotGun:
                if (IsShotGunReload == true)
                {
                    AttackRate[2] += Time.deltaTime;
                }
                if (AttackRate[2] >= PlayerManager.instance.AttackRate)
                {
                    if (PlayerManager.instance.ShotBullet > 0)
                    {
                        PlayerManager.instance.BulltText.gameObject.SetActive(false);
                        PlayerManager.instance.ShotBullet -= 1;
                        ObjectPool.instance.CreateShotAmmuntion(createAmmuntionPosition.transform.position, new Vector3(-125f, 0f, 0f));
                        AttackRate[2] = 0f;

                        if (PlayerManager.instance.ShotgunBullet > 0 && PlayerManager.instance.ShotBullet == 0)
                        {
                            IsReload = true;
                            IsShotGunReload = false;
                        }
                    }
                }
                if (ReloadTime >= ShotGunRe)
                {
                    PlayerManager.instance.ShotBullet = GameSetting.ShotBullet;
                    PlayerManager.instance.ShotgunBullet -= GameSetting.ShotBullet;
                    IsReload = false;
                    IsShotGunReload = true;
                    ReloadTime = 0f;
                }
                if (PlayerManager.instance.ShotBullet <= 0 && PlayerManager.instance.ShotgunBullet <= 0)
                {
                    PlayerManager.instance.BulltText.gameObject.SetActive(true);
                }
                break;
            case GameSetting.Guns.FireGun:
                if (IsFireGunReload == true)
                {
                    AttackRate[3] += Time.deltaTime;
                }
                if (AttackRate[3] >= PlayerManager.instance.AttackRate)
                {
                    if(PlayerManager.instance.FireBullet > 0)
                    {
                        PlayerManager.instance.BulltText.gameObject.SetActive(false);
                        PlayerManager.instance.FireBullet -= 1;
                        ObjectPool.instance.CreateFireAmmuntion(createAmmuntionPosition.transform.position, new Vector3(45f, 0f, 0f));
                        AttackRate[3] = 0f;

                        if(PlayerManager.instance.FireGunBullet > 0 && PlayerManager.instance.FireBullet == 0)
                        {
                            IsReload = true;
                            IsFireGunReload = false;
                        }
                    }
                }
                if(ReloadTime >= FireGunRe)
                {
                    PlayerManager.instance.FireBullet = GameSetting.FireBullet;
                    PlayerManager.instance.FireGunBullet -= GameSetting.FireBullet;
                    IsReload = false;
                    IsFireGunReload = true;
                    ReloadTime = 0f;
                }
                if (PlayerManager.instance.FireBullet <= 0 && PlayerManager.instance.FireGunBullet <= 0)
                {
                    PlayerManager.instance.BulltText.gameObject.SetActive(true);
                }
                break;
        }
    }
}