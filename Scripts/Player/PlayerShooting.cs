using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : CCompoent
{
    public Material[] BulletMaterial = null;
    public float Damage;
    public float range = 100f;
    public int ShootableMask = 0;
    public ParticleSystem gunParticles = null;
    public LineRenderer gunLine;
    public Light Gunlight = null;
    public float EffectsDisplayTime = 0.03f;
    public Transform ShotPoistion;
    public float ShootEffectTimer;
    private Color gunColor = new Color(1, 1, 0, 255); // 노란색

    Ray ShootRay = new Ray(); //총에서 나가는 레이저
    RaycastHit ShootHit; //레이저가 맞았다는 것을 알려주는 변수

    public Light faceLight;

    private void Start()
    {
        ShootableMask = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        ShootEffectTimer += Time.deltaTime;
    }

    public void SsgGunColorSetting()
    {
        gunLine.SetWidth(0.05f, 0.05f);
        Gunlight.color = gunColor;
        faceLight.color = gunColor;

        ParticleSystem.MainModule bullet = gunParticles.main;
        bullet.startSize = 0.75f;
        bullet.startColor = gunColor;
        gunLine.material = BulletMaterial[0];

    }

    public void Disableeffects()
    {
        gunLine.enabled = false; // 총알 경로 삭제
        faceLight.enabled = false; // 총쏘는 캐릭터 얼굴방향으로 나오는 빛 삭제
        Gunlight.enabled = false; // 총쏠때 바닥을 비추는 빛
    }

    public void LaserColorSetting()
    {
        Invoke("BigWidthSetting", 0.1f);
        Invoke("SmallWidthSetting", 0.15f);
        Gunlight.color = gunColor;
        faceLight.color = gunColor;

        ParticleSystem.MainModule bullet = gunParticles.main;
        bullet.startSize = 1.5f;
        bullet.startColor = gunColor;
    }

    public void ShotGunColorSetting()
    {
        Gunlight.color = gunColor;
        faceLight.color = gunColor;

        ParticleSystem.MainModule bullet = gunParticles.main;
        bullet.startSize = 1.5f;
        bullet.startColor = gunColor;
    }

    private void BigWidthSetting()
    {
        gunLine.SetWidth(0.1f, 0.2f);
        gunLine.material = BulletMaterial[1];
    }

    private void SmallWidthSetting()
    {
        gunLine.SetWidth(0.2f, 0.1f);
        gunLine.material = BulletMaterial[2];
    }

    public void BasicGunShooting()
    {
        //총을 쏘았을 때 빚을 켜줌
        Gunlight.enabled = true;
        faceLight.enabled = true;

        if (!gunParticles.isPlaying)
        {
            gunParticles.Play();
        }

        gunLine.enabled = true;
        gunLine.SetPosition(0, ShotPoistion.position);

        ShootRay.origin = ShotPoistion.position;
        ShootRay.direction = ShotPoistion.forward; //총을 쏘는 거리가 직선으로 나가게 함

        if (Physics.Raycast(ShootRay, out ShootHit, range, ShootableMask))
        {
            Enemy enemys = ShootHit.collider.GetComponent<Enemy>();

            if (enemys != null)
            {
                enemys.GunTakeDamage(PlayerManager.instance.Damage, ShootHit.point);
            }

            gunLine.SetPosition(1, ShootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, ShootRay.origin + ShootRay.direction * range);
        }
    }

    public void LaserGunShooting()
    {

    }
}
