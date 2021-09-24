using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CCompoent
{
    public float[] EnemyCreateTime = new float[3];
    public float[] CreateTime = new float[3];
    public int RandomPoistion;

    public static EnemyManager instance;

    public Transform[] EnemyCreatePoistion = new Transform[3];
    public GameObject[] Enemys = new GameObject[3];

    public Transform CreatePoisiton;

    public List<GameObject> EnemyList;
    public float EnemyDamageAmout;

    public override void Awake()
    {
        base.Awake();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            return;
        }
    }

    private void Start()
    {
        ObjectPool.instance.EnemyDamageTextCount(90);
    }

    private void Update()
    {
        RandomPoistion = Random.Range(0, 3);
        EnemyCreateTimeSetting();
        CreatePoistionSetting();
        if (GameSetting.IsGameStart == true || GameSetting.IsGameOver == false)
        {
            for (int i = 0; i < CreateTime.Length; i++)
            {
                CreateTime[i] += Time.deltaTime;
            }
        }
        else
        {
            for (int i = 0; i < CreateTime.Length; i++)
            {
                CreateTime[i] = 0;
            }
        }
        ZomyBunyCreate();
        ZomyBearCreate();
        ZomyHellephantCreate();
    }

    private void EnemyCreateTimeSetting()
    {
        switch(GameManager.instance.difficulty)
        {
            case GameSetting.Difficulty.Easy:
                for(int i = 0; i < EnemyCreateTime.Length; i++)
                {
                    EnemyCreateTime[i] = 8f + (0.5f * i);
                }
                break;
            case GameSetting.Difficulty.Normal:
                for (int i = 0; i < EnemyCreateTime.Length; i++)
                {
                    EnemyCreateTime[i] = 8f + (0.5f * i);
                }
                
                for(int j = 0; j < 3; j++)
                {
                    EnemyCreateTime[j] -= 0.2f;
                }
                break;
            case GameSetting.Difficulty.Hard:
                for (int i = 0; i < EnemyCreateTime.Length; i++)
                {
                    EnemyCreateTime[i] = 7f + (0.5f * i);
                }

                for (int j = 0; j < 3; j++)
                {
                    EnemyCreateTime[j] -= 0.4f;
                }
                break;
            case GameSetting.Difficulty.VeryHard:
                for (int i = 0; i < EnemyCreateTime.Length; i++)
                {
                    EnemyCreateTime[i] = 8f + (0.5f * i);
                }

                for (int j = 0; j < 3; j++)
                {
                    EnemyCreateTime[j] -= 0.6f;
                }
                break;
        }
    }

    void CreatePoistionSetting()
    {
        switch(RandomPoistion)
        {
            case 0:
                CreatePoisiton = EnemyCreatePoistion[0];
                break;
            case 1:
                CreatePoisiton = EnemyCreatePoistion[1];
                break;
            case 2:
                CreatePoisiton = EnemyCreatePoistion[2];
                break;
        }
    }

    private void ZomyBunyCreate()
    {
        if(CreateTime[0] >= EnemyCreateTime[0])
        {
            GameObject ZomyBunny = Instantiate(Enemys[0], CreatePoisiton.position, Quaternion.identity);
            ZomyBunny.transform.parent = this.gameObject.transform.GetChild(0).transform; //오브젝트 자식 0번째를 부모로 하고생성
            CreateTime[0] = 0;
            EnemyList.Add(ZomyBunny);
        }
    }

    private void ZomyBearCreate()
    {
        if (CreateTime[1] >= EnemyCreateTime[1])
        {
            GameObject ZomyBear = Instantiate(Enemys[1], CreatePoisiton.position, Quaternion.identity);
            ZomyBear.transform.parent = this.gameObject.transform.GetChild(1).transform;
            CreateTime[1] = 0;
            EnemyList.Add(ZomyBear);
        }
    }

    private void ZomyHellephantCreate()
    {
        if (CreateTime[2] >= EnemyCreateTime[2])
        {
            GameObject ZomyHellephant = Instantiate(Enemys[2], CreatePoisiton.position, Quaternion.identity);
            ZomyHellephant.transform.parent = this.gameObject.transform.GetChild(2).transform;
            CreateTime[2] = 0;
            EnemyList.Add(ZomyHellephant);
        }
    }
}