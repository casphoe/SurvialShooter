using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public enum Enemys
{
    ZomBunny, ZomBear, ZomHellephant
}

public class Enemy : CCompoent
{
    public Enemys enemy;

    public float Hp;
    public float MaxHp;
    public float AttackPower;
    public float Defenece;
    public float Speed;
    public int AddScore;
    public int BuffDropValue;

    private NavMeshAgent Agent;

    public Vector3 Target;
    public Slider HpBar;
    public Animator EnemyAnim;
    public GameObject CreateDamagePoistion;
    public float animSpeed = 3f;
    public ParticleSystem[] hitParticles = new ParticleSystem[2];

    void Start()
    {
        EnemySetting();
        Agent = GetComponent<NavMeshAgent>();
        DifficultyEnemySetting();
        HpBar.maxValue = MaxHp;
        Hp = MaxHp;
        HpBar.value = Hp;
        EnemyAnim = GetComponent<Animator>();        
    }

    void Update()
    {
        Target = PlayerManager.instance.PlayerPosition;
        if(GameSetting.IsGameOver == false)
        {
            Move();
            PlayerCollision();
        }
        else
        {
            return;
        }
    }

    void EnemySetting()
    {
        switch (enemy)
        {
            case Enemys.ZomBunny:
                GameManager.instance.EnemyStat("ZomBunny");
                break;
            case Enemys.ZomBear:
                GameManager.instance.EnemyStat("ZomBear");
                break;
            case Enemys.ZomHellephant:
                GameManager.instance.EnemyStat("ZomHellephant");
                break;
        }
    }

    void DifficultyEnemySetting()
    {
        BuffDropValue = GameSetting.BuffDropValue;
        switch(GameManager.instance.difficulty)
        {
            case GameSetting.Difficulty.Easy:
                MaxHp = GameSetting.EnemyHp;
                AttackPower = GameSetting.EnemyPower;
                Defenece = GameSetting.EnemyDeffence;
                Speed = GameSetting.EnemyMoveSpeed;
                AddScore = GameSetting.AddScore;
                break;
            case GameSetting.Difficulty.Normal:
                MaxHp = GameSetting.EnemyHp +  (GameSetting.EnemyHp * 0.1f);
                AttackPower = GameSetting.EnemyPower + (GameSetting.EnemyPower * 0.1f);
                Defenece = GameSetting.EnemyDeffence + (GameSetting.EnemyDeffence * 0.1f);
                Speed = GameSetting.EnemyMoveSpeed + (GameSetting.EnemyMoveSpeed * 0.1f);
                AddScore = GameSetting.AddScore + 15;                
                break;
            case GameSetting.Difficulty.Hard:
                MaxHp = GameSetting.EnemyHp + (GameSetting.EnemyHp * 0.2f);
                AttackPower = GameSetting.EnemyPower + (GameSetting.EnemyPower * 0.2f);
                Defenece = GameSetting.EnemyDeffence + (GameSetting.EnemyDeffence * 0.2f);
                Speed = GameSetting.EnemyMoveSpeed + (GameSetting.EnemyMoveSpeed * 0.2f);
                AddScore = GameSetting.AddScore + 30;                
                break;
            case GameSetting.Difficulty.VeryHard:
                MaxHp = GameSetting.EnemyHp + (GameSetting.EnemyHp * 0.4f);
                AttackPower = GameSetting.EnemyPower + (GameSetting.EnemyPower * 0.4f);
                Defenece = GameSetting.EnemyDeffence + (GameSetting.EnemyDeffence * 0.4f);
                Speed = GameSetting.EnemyMoveSpeed + (GameSetting.EnemyMoveSpeed * 0.4f);
                AddScore = GameSetting.AddScore + 30;          
                break;
        }
    }

    private void Move()
    {
        Agent.destination = new Vector3(Target.x, Target.y, Target.z);

        transform.LookAt(Target);

    }

    void PlayerCollision()
    {
        Vector3 EnemyRange = this.gameObject.transform.position;

        Vector3 dir = EnemyRange - Target;

        float range = dir.magnitude;

        float EnemyL = 0.5f;
        float PlayerL = 1f;

        if (range < EnemyL + PlayerL)
        {
            if (PlayerManager.instance.IsInvincibility == false)
            {
                PlayerManager.instance.TakeDamage(AttackPower);
            }
        }
    }

    public void TakeDamage(float Amout)
    {
        Hp -= (Amout - Defenece);
        HpBar.value -= (Amout - Defenece);
        EnemyManager.instance.EnemyDamageAmout = (Amout - Defenece);
        Vector3 EnemyDamageUIPoistion = Camera.main.WorldToScreenPoint(CreateDamagePoistion.transform.position);
        ObjectPool.instance.CreateEnemyDamageEffect(EnemyDamageUIPoistion, Vector3.zero);
        
        if (Hp <= 0)
        {
            /*
             * ZomBunny(Clone)' AnimationEvent 'StartSinking' on animation 'Death' has no receiver! Are you missing a component?
             * 오류가 발생 적 캐릭터 모델링 Death에 이벤트가 있는데 이벤트를 처리할 리시버를 찾기 못해서 발생하는 문제
             * 이벤트를 제거해주면 됨
             */
            EnemyAnim.SetFloat("DeadSpeed", animSpeed);
            EnemyAnim.SetBool("Death",true);
            Agent.isStopped = true;
            GameSetting.Score += AddScore;
            GameSetting.CoinCount += AddScore / 4;
            //죽는 소리 실행 한번

            Function.LateCallFunc(this, 0.3f, (CCompoent) =>
            {
                Destroy(this.gameObject);
            });
        }
    }

    public void GunTakeDamage(float Amout, Vector3 hitPoint)
    {
        Hp -= (Amout - Defenece);
        HpBar.value -= (Amout - Defenece);
        EnemyManager.instance.EnemyDamageAmout = (Amout - Defenece);
        Vector3 EnemyDamageUIPoistion = Camera.main.WorldToScreenPoint(CreateDamagePoistion.transform.position);
        ObjectPool.instance.CreateEnemyDamageEffect(EnemyDamageUIPoistion, Vector3.zero);

        hitParticles[0].transform.position = hitPoint;

        if(!hitParticles[0].isPlaying)
        {
            hitParticles[0].Play();
        }
        if (Hp <= 0)
        {
            /*
             * ZomBunny(Clone)' AnimationEvent 'StartSinking' on animation 'Death' has no receiver! Are you missing a component?
             * 오류가 발생 적 캐릭터 모델링 Death에 이벤트가 있는데 이벤트를 처리할 리시버를 찾기 못해서 발생하는 문제
             * 이벤트를 제거해주면 됨
             */
            EnemyAnim.SetFloat("DeadSpeed", animSpeed);
            EnemyAnim.SetBool("Death", true);
            Agent.isStopped = true;
            GameSetting.Score += AddScore;
            GameSetting.CoinCount += AddScore / 4;
            //죽는 소리 실행 한번

            Function.LateCallFunc(this, 0.3f, (CCompoent) =>
            {
                Destroy(this.gameObject);
            });
        }
    }
}