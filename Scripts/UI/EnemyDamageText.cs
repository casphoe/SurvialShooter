using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDamageText : CCompoent
{
    private Text Damage;
    Vector3 dir;
    Vector3 Prevdir;
    float time;

    public float Amout;

    void Start()
    {
        Damage = GetComponent<Text>();
        dir = gameObject.transform.position;
        Prevdir = dir;
        time = 0f;
        Amout = EnemyManager.instance.EnemyDamageAmout;

        Damage.text = "- " + Amout;
    }

    // Update is called once per frame
    void Update()
    {
        dir.y += 0.3f;

        transform.position = dir;

        if (time < 3f)
        {
            Damage.color = new Color(1, 0, 0, time / 3);
        }
        else
        {
            time = 0;
            gameObject.SetActive(false);
            dir = Prevdir;
        }
        time += Time.deltaTime;
    }
}
