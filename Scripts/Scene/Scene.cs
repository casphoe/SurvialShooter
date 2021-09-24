using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : CCompoent
{
    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 유니티의 모든 게임 객체는 자기 자신이 포함 되어 잇는 씬에 접근하는 것이 가능
         * (모든 게임 객체 Scene 프로퍼티를 지니고 있음)
         */
        var StartScene = this.gameObject.scene;
    }
}
