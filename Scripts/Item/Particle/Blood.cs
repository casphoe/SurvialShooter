using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : CCompoent
{

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.instance.bleeding == false)
        {
            this.gameObject.SetActive(false);
        }
    }
}
