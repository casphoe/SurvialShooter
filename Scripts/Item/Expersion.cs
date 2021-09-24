using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expersion : CCompoent
{
    public float Damage;
    public GameObject ExpersionEffectObject;
    private void Start()
    {
        GameManager.instance.ItemStat("Mine");
        Damage = GameSetting.Damage;
        

        foreach(GameObject damageEnemy in PlayerManager.instance.MineDamageCount)
        {
            ExpersionEffectObject = damageEnemy;

            ExpersionEffectObject.GetComponent<Enemy>().TakeDamage(Damage);
        }

        if (PlayerManager.instance.MineDamageCount.Count > 0)
        {
            /*
             * 순회하는 과정에서 List 사이즈가 변경되어 잘못된 인덱스에 접근할 가능성이 있기 때문에
             * 거꾸로 순회하면서 제거하면 안전하게 작업이 가능하다.
             */
            for(int i = PlayerManager.instance.MineDamageCount.Count - 1; i >= 0; i--)
            {
                PlayerManager.instance.MineDamageCount.Remove(PlayerManager.instance.MineDamageCount[i]);
            }

            /*for (int i = 0; i <= PlayerManager.instance.MineDamageCount.Count; i++)
            {
                //배열 안의 요소들 중 지정된 범위의 요소들을 제거
                PlayerManager.instance.MineDamageCount.RemoveRange(0,i);               
            }*/
        }

        Function.LateCallFunc(this, 0.2f, (CCompoent) =>
          {
              gameObject.SetActive(false);
          });
    }
    
}