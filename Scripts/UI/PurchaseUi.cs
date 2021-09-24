using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PurchaseUi : CCompoent
{
    public float Timer;

    private void Update()
    {
        if(Timer < 4f)
        {
            PlayerManager.instance.PurchaseText.color = new Color(1, 1, 1, 1f - Timer / 4f);
        }
        else
        {
            Timer = 0f;
            gameObject.SetActive(false);
        }
        Timer += Time.deltaTime;
    }
}