using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 게임이 시작 할 때 Count값 만큼 생성하고 Create함수를 사용해서 생성될 때 켜줌
 */
public class ObjectPool : CCompoent
{
    public List<GameObject> BulletAmmuniton = new List<GameObject>(); //리로드 할 때 마다 생성되는 파티클을 담아둘 리스트 생성
    public List<GameObject> MineList = new List<GameObject>();
    public List<GameObject> ExperisonParticle = new List<GameObject>();
    public List<GameObject> PlayerDamageCountText = new List<GameObject>();
    public List<GameObject> EnemyDamageCountText = new List<GameObject>();
    public List<GameObject> BloodParticle = new List<GameObject>();
    public List<GameObject> ShotAmmutions = new List<GameObject>();
    public List<GameObject> FireAmmutions = new List<GameObject>();
    public List<GameObject> BloodDamageText = new List<GameObject>();

    public GameObject Mine;
    public GameObject[] AummuntionObject = new GameObject[3];
    public GameObject Experison;
    public GameObject DamgaeText;
    public GameObject EnemyDamageText;
    public GameObject BloodParticleObject;
    public GameObject BloodDamgetext;

    public GameObject PlayerDamageTextcanvas;
    public GameObject EnemyDamageTextcanvas;

    public static ObjectPool instance;

    private new void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            return;
        }
    }

    public void BloodParticleCount(int Count)
    {
        for(int i = 0; i < Count; i++)
        {
            GameObject blood = Instantiate(BloodParticleObject) as GameObject;

            blood.transform.parent = this.gameObject.transform.GetChild(3).transform;

            blood.SetActive(false);

            BloodParticle.Add(blood);
        }
    }

    public void PlayerDamageTextCount(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject text = Instantiate(DamgaeText) as GameObject;

            text.transform.parent = PlayerDamageTextcanvas.transform;

            text.SetActive(false);

            PlayerDamageCountText.Add(text);
        }
    }

    public void BloddDamgeTextCount(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject BloodText = Instantiate(BloodDamgetext) as GameObject;

            BloodText.transform.parent = PlayerDamageTextcanvas.transform.GetChild(0).transform;

            BloodText.SetActive(false);

            BloodDamageText.Add(BloodText);
        }
    }

    public void EnemyDamageTextCount(int Count)
    {
        for(int i = 0; i < Count; i++)
        {
            GameObject EnemyText = Instantiate(EnemyDamageText) as GameObject;

            EnemyText.transform.parent = EnemyDamageTextcanvas.transform;

            EnemyText.SetActive(false);

            EnemyDamageCountText.Add(EnemyText);
        }
    }

    public void ParticeleExpersion(int Count)
    {
        for(int i = 0; i< Count; i++)
        {
            GameObject Ep = Instantiate(Experison) as GameObject;

            Ep.transform.parent = this.gameObject.transform.GetChild(2).transform;

            Ep.SetActive(false);

            ExperisonParticle.Add(Ep);
        }
    }

    public void GunAmmuntion(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject GR = Instantiate(AummuntionObject[0]) as GameObject;

            GR.transform.parent = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform; //스크립트가 있는 오브젝트의 첫번째 자식에 생성

            GR.SetActive(false);

            BulletAmmuniton.Add(GR);
        }
    }

    public void ShotGunAmmution(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject ShotAn = Instantiate(AummuntionObject[1]) as GameObject;

            ShotAn.transform.parent = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform;

            ShotAn.SetActive(false);

            ShotAmmutions.Add(ShotAn);
        }
    }

    public void FireGunAmmution(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject FireGunAn = Instantiate(AummuntionObject[2]) as GameObject;

            FireGunAn.transform.parent = this.gameObject.transform.GetChild(0).transform.GetChild(2).transform;

            FireGunAn.SetActive(false);

            FireAmmutions.Add(FireGunAn);
        }
    }

    public void MineCount(int Count)
    {
        for(int i = 0; i < Count; i++)
        {
            GameObject M = Instantiate(Mine) as GameObject;

            M.transform.parent = this.gameObject.transform.GetChild(1).transform;

            M.SetActive(false);

            MineList.Add(M);
        }
    }

    public GameObject CreateMineObject(Vector3 pos, Vector3 rot)
    {
        GameObject MO = null;

        for(int i = 0; i < MineList.Count; i++)
        {
            if(MineList[i].activeSelf == false)
            {
                MO = MineList[i];
                break;
            }
        }

        if(MO == null)
        {
            GameObject NewMine = Instantiate(Mine, pos, Quaternion.Euler(rot));

            NewMine.transform.parent = this.gameObject.transform.GetChild(1).transform;

            MineList.Add(NewMine);

            MO = NewMine;
        }
        MO.SetActive(true);
        MO.transform.position = pos;
        return MO;
    }

    public GameObject CreatePlayerDamageEffect(Vector3 Pos,Vector3 Rot)
    {
        GameObject PD = null;

        for(int i = 0; i < PlayerDamageCountText.Count; i++)
        {
            PD = PlayerDamageCountText[i];
            break;
        }

        if(PD == null)
        {
            GameObject NewPD = Instantiate(DamgaeText, Pos, Quaternion.Euler(Rot));

            NewPD.transform.parent = PlayerDamageTextcanvas.transform;

            PlayerDamageCountText.Add(NewPD);

            PD = NewPD;
        }
        PD.SetActive(true);
        PD.transform.position = Pos;
        return PD;
    }

    public GameObject CreateBloodTextEffect(Vector3 pos,Vector3 rot)
    {
        GameObject BloodEffectText = null;

        for(int i = 0; i < BloodDamageText.Count; i++)
        {
            BloodEffectText = BloodDamageText[i];
            break;
        }

        if(BloodEffectText == null)
        {
            GameObject Newblood = Instantiate(BloodDamgetext, pos, Quaternion.Euler(rot));

            Newblood.transform.parent = PlayerDamageTextcanvas.transform.GetChild(0).transform;

            BloodDamageText.Add(Newblood);

            BloodEffectText = Newblood;
        }
        BloodEffectText.SetActive(true);
        BloodEffectText.transform.position = pos;
        return BloodEffectText;
    }

    public GameObject CreateEnemyDamageEffect(Vector3 Pos,Vector3 rot)
    {
        GameObject Enemydamage = null;

        for(int i = 0; i < EnemyDamageCountText.Count; i++)
        {
            Enemydamage = EnemyDamageCountText[i];

            break;
        }

        if(Enemydamage == null)
        {
            GameObject NewEnemyDamage = Instantiate(EnemyDamageText, Pos, Quaternion.Euler(rot));

            NewEnemyDamage.transform.parent = EnemyDamageTextcanvas.transform;

            EnemyDamageCountText.Add(NewEnemyDamage);

            Enemydamage = NewEnemyDamage;
        }
        Enemydamage.SetActive(true);
        Enemydamage.transform.position = Pos;
        return Enemydamage;
    }

    public GameObject CreateAmmuntionSystem(Vector3 pos, Vector3 rot)
    {
        GameObject AmmuntionSystem = null;

        for(int i = 0; i < BulletAmmuniton.Count; i++)
        {
            if(BulletAmmuniton[i].activeSelf == false)
            {
                AmmuntionSystem = BulletAmmuniton[i];

                break;
            }
        }

        if(AmmuntionSystem == null)
        {
            GameObject NewAmmuntionSystem = Instantiate(AummuntionObject[0], pos, Quaternion.Euler(rot));

            NewAmmuntionSystem.transform.parent = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform;

            BulletAmmuniton.Add(NewAmmuntionSystem);

            AmmuntionSystem = NewAmmuntionSystem;
        }
        AmmuntionSystem.SetActive(true);
        AmmuntionSystem.transform.position = pos;
        return AmmuntionSystem;
    }

    public GameObject CreateFireAmmuntion(Vector3 pos, Vector3 rot)
    {
        GameObject fireammution = null;

        for(int i = 0; i < FireAmmutions.Count; i++)
        {
            fireammution = FireAmmutions[i];

            break;
        }

        if(fireammution == null)
        {
            GameObject Newfire = Instantiate(AummuntionObject[2], pos, Quaternion.Euler(rot));

            Newfire.transform.parent = this.gameObject.transform.GetChild(0).transform.GetChild(2).transform;

            FireAmmutions.Add(Newfire);

            fireammution = Newfire;
        }
        fireammution.SetActive(true);
        fireammution.transform.position = pos;
        return fireammution;
    }

    public GameObject CreateShotAmmuntion(Vector3 pos,Vector3 rot)
    {
        GameObject Shotammution = null;

        for(int i = 0; i < ShotAmmutions.Count; i++)
        {
            Shotammution = ShotAmmutions[i];

            break;
        }

        if(Shotammution == null)
        {
            GameObject NewShot = Instantiate(AummuntionObject[1], pos, Quaternion.Euler(rot));

            NewShot.transform.parent = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform;

            ShotAmmutions.Add(NewShot);

            Shotammution = NewShot;
        }
        Shotammution.SetActive(true);
        Shotammution.transform.position = pos;
        return Shotammution;
    }

    public GameObject CreateExperision(Vector3 Pos,Vector3 rot)
    {
        GameObject ParticleExpersion = null;

        for(int i = 0; i < ExperisonParticle.Count; i++)
        {
            if(ExperisonParticle[i].activeSelf == false)
            {
                ParticleExpersion = ExperisonParticle[i];

                break;
            }
        }

        if(ParticleExpersion == null)
        {
            GameObject NewParticleExpersion = Instantiate(Experison, Pos, Quaternion.Euler(rot));

            NewParticleExpersion.transform.parent = this.gameObject.transform.GetChild(2).transform;

            ExperisonParticle.Add(NewParticleExpersion);

            ParticleExpersion = NewParticleExpersion;
        }
        ParticleExpersion.SetActive(true);
        ParticleExpersion.transform.position = Pos;
        return ParticleExpersion;
    }

    public GameObject CreateBloodParticle(Vector3 pos, Vector3 rot)
    {
        GameObject BP = null;

        for(int i = 0; i < BloodParticle.Count; i++)
        {
            BP = BloodParticle[i];
            break;
        }

        if(BP == null)
        {
            GameObject Newblood = Instantiate(BloodParticleObject, pos, Quaternion.Euler(rot));

            Newblood.transform.parent = this.gameObject.transform.GetChild(3).transform;

            BloodParticle.Add(Newblood);

            BP = Newblood;
        }
        BP.SetActive(true);
        BP.transform.position = pos;
        return BP;
    }
}