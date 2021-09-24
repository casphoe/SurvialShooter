using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammuntion : CCompoent
{
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        
        //탄약 소리가 나고
    }

    private void Update()
    {
        target = new Vector3(PlayerManager.instance.AmmuntionTrans.position.x - 2f, PlayerManager.instance.AmmuntionTrans.position.y, PlayerManager.instance.AmmuntionTrans.position.z);

        transform.position = Vector3.Slerp(transform.position, target, 0.005f);
    }

    void AmmutionFalse()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            AmmutionFalse();
        }
    }
}
