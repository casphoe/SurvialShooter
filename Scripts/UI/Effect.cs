using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Effect : CCompoent
{
    float timer = 0f;

    Text TextEffect;

    private void Start()
    {
        TextEffect = GetComponent<Text>();
    }

    private void Update()
    {
        if (timer < 0.5f)
        {
            TextEffect.color = new Color(1, 1, 1, 1 - timer);
        }
        else
        {
            TextEffect.color = new Color(1, 1, 1, timer);
            if (timer > 1f)
            {
                timer = 0f;
            }
        }

        timer += Time.deltaTime;
    }
}
