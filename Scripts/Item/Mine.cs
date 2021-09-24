using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : CCompoent
{
    public Transform ExperisionPoistion;
    public float Ranage;
    //public float Damage;

    void Start()
    {
        PlayerManager.instance.IsMineStep = false;
        ExperisionPoistion = gameObject.transform.GetChild(1).transform;
        
        Ranage = 10f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerManager.instance.IsMineStep = true;
            /*
             * physics.overlapsphere 함수는 특정 위치의 반경내의 있는 객체의 collider를 반환해주는 함수
             */
            PlayerManager.instance.Colls = Physics.OverlapSphere(transform.position, Ranage);
            foreach (Collider enemy in PlayerManager.instance.Colls)
            {
                other = enemy;
                //Debug.Log(other);
                if(other.gameObject.tag == "Enemy")
                {
                    PlayerManager.instance.MineDamageCount.Add(other.gameObject);
                }
            }
            ObjectPool.instance.CreateExperision(ExperisionPoistion.position, Vector3.zero);
            gameObject.SetActive(false);
        }
    }
}