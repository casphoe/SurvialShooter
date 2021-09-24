using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletRemain : CCompoent
{
    public Text Bullet;
    public GameSetting.Guns Gun;
    void Update()
    {
        switch(Gun)
        {
            case GameSetting.Guns.Ssg:
                if(GameManager.instance.Player == GameSetting.Player.Girl)
                {
                    Bullet.text = "x " + PlayerManager.instance.SsgBullet + " / ∞";
                }
                else if(GameManager.instance.Player == GameSetting.Player.Man)
                {
                    Bullet.text = "x " + PlayerManager.instance.SsgBullet + " / " + PlayerManager.instance.SsgGunBullet;
                }
                else if(GameManager.instance.Player == GameSetting.Player.Solider)
                {
                    Bullet.text = "x " + PlayerManager.instance.SsgBullet + " / " + PlayerManager.instance.SsgGunBullet;
                }
                break;
            case GameSetting.Guns.Laser:
                Bullet.text = "x " + PlayerManager.instance.LaserBullet + " / " + PlayerManager.instance.LaserGunBullet;
                break;
            case GameSetting.Guns.ShotGun:
                Bullet.text = "x " + PlayerManager.instance.ShotBullet + " / " + PlayerManager.instance.ShotgunBullet;
                break;
            case GameSetting.Guns.FireGun:
                Bullet.text = "x " + PlayerManager.instance.FireBullet + " / " + PlayerManager.instance.FireGunBullet;
                break;
        }
    }
}
