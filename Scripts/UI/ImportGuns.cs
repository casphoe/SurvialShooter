using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImportGuns : MonoBehaviour
{
    public Image GunImages;

    public GameObject[] GunLevel = new GameObject[5];

    private void Update()
    {
        GunImageGuI();
    }

    void GunImageGuI()
    {
        switch(GameManager.instance.Gun)
        {
            case GameSetting.Guns.Ssg:
                GunImages.sprite = PlayerManager.instance.GunSprite[0];
                switch (GameSetting.SSgUpgrade)
                {
                    case 0:
                        for(int i = 0; i < 5; i++)
                        {
                            GunLevel[i].SetActive(false);
                        }
                        break;
                    case 1:
                        GunLevel[0].SetActive(true);
                        for (int i = 1; i < 5; i++)
                        {
                            GunLevel[i].SetActive(false);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 2; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 2; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 3; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 3; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 4; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 4; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 5:
                        for (int i = 0; i < 5; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        break;
                }
                break;
            case GameSetting.Guns.Laser:
                GunImages.sprite = PlayerManager.instance.GunSprite[1];
                switch (GameSetting.LaserUpgrade)
                {
                    case 0:
                        for (int i = 0; i < 5; i++)
                        {
                            GunLevel[i].SetActive(false);
                        }
                        break;
                    case 1:
                        GunLevel[0].SetActive(true);
                        for (int i = 1; i < 5; i++)
                        {
                            GunLevel[i].SetActive(false);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 2; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 2; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 3; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 3; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 4; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 4; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 5:
                        for (int i = 0; i < 5; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        break;
                }
                break;
            case GameSetting.Guns.ShotGun:
                GunImages.sprite = PlayerManager.instance.GunSprite[2];
                switch (GameSetting.ShotGunUpgrade)
                {
                    case 0:
                        for (int i = 0; i < 5; i++)
                        {
                            GunLevel[i].SetActive(false);
                        }
                        break;
                    case 1:
                        GunLevel[0].SetActive(true);
                        for (int i = 1; i < 5; i++)
                        {
                            GunLevel[i].SetActive(false);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 2; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 2; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 3; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 3; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 4; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 4; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 5:
                        for (int i = 0; i < 5; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        break;
                }
                break;
            case GameSetting.Guns.FireGun:
                GunImages.sprite = PlayerManager.instance.GunSprite[3];
                switch (GameSetting.FireGunUpgrade)
                {
                    case 0:
                        for (int i = 0; i < 5; i++)
                        {
                            GunLevel[i].SetActive(false);
                        }
                        break;
                    case 1:
                        GunLevel[0].SetActive(true);
                        for(int i = 1; i < 5; i++)
                        {
                            GunLevel[i].SetActive(false);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 2; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 2; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 3; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 3; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 4; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        for (int j = 4; j < 5; j++)
                        {
                            GunLevel[j].SetActive(false);
                        }
                        break;
                    case 5:
                        for (int i = 0; i < 5; i++)
                        {
                            GunLevel[i].SetActive(true);
                        }
                        break;
                }
                break;
        }
    }
}