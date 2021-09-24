using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : CCompoent
{
    public void SSg()
    {
        GameManager.instance.Gun = GameSetting.Guns.Ssg;
        if(GameSetting.SSgUpgrade >= 1)
        {
            PlayerManager.instance.Damage = PlayerManager.instance.GunDamage[0];
        }
        else
        {
            PlayerManager.instance.Damage = GameSetting.PlayerPower + GameSetting.SsgPower;
        }
        PlayerManager.instance.AttackRate = (GameSetting.PlayerAttackRate - GameSetting.SsgRate);
    }

    public void LaserGun()
    {
        GameManager.instance.Gun = GameSetting.Guns.Laser;
        if (GameSetting.LaserUpgrade >= 1)
        {
            PlayerManager.instance.Damage = PlayerManager.instance.GunDamage[1];
        }
        else
        {
            PlayerManager.instance.Damage = GameSetting.PlayerPower + GameSetting.LaserPower;
        }
        PlayerManager.instance.AttackRate = (GameSetting.PlayerAttackRate - GameSetting.AsultRate);
    }

    public void ShotGun()
    {
        GameManager.instance.Gun = GameSetting.Guns.ShotGun;
        if (GameSetting.ShotGunUpgrade >= 1)
        {
            PlayerManager.instance.Damage = PlayerManager.instance.GunDamage[2];
        }
        else
        {
            PlayerManager.instance.Damage = GameSetting.PlayerPower + GameSetting.ShotGunPower;
        }
        PlayerManager.instance.AttackRate = (GameSetting.PlayerAttackRate - GameSetting.ShotGunRate);
    }

    public void FireGun()
    {
        GameManager.instance.Gun = GameSetting.Guns.FireGun;
        if (GameSetting.FireGunUpgrade >= 1)
        {
            PlayerManager.instance.Damage = PlayerManager.instance.GunDamage[3];
        }
        else
        {
            PlayerManager.instance.Damage = GameSetting.PlayerPower + GameSetting.FireGunPower;
        }
        PlayerManager.instance.AttackRate = (GameSetting.PlayerAttackRate - GameSetting.FireGunRate);
    }

    public void CreateMine()
    {
        PlayerManager.instance.MineCount -= 1;
        ObjectPool.instance.CreateMineObject(PlayerManager.instance.CreateMinePosition.transform.position, Vector3.zero);
    }

    private void Update()
    {
#if UNITY_STANDALONE
        PcGunChange();
#endif
    }

    private void PcGunChange()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            SSg();
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            LaserGun();
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            ShotGun();
        }
        if(Input.GetKey(KeyCode.Alpha4))
        {
            FireGun();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            if(PlayerManager.instance.MineCount > 0)
            {
                CreateMine();
            }
        }
    }
}