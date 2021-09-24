using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Singleton<T> : CCompoent where T : Singleton<T>
{
    public static T m_instance = null;

    public static T instance
    {
        get
        {
            if(m_instance == null)
            {

                /*
                 * typeof 키워드는 명시된 자료형의 정보를 지니고 있는 type객체를 반환하는 역활을 함
                 * type 객체는 특정 자료형에 대한 정보를 모두 지니고 있기 때문에 실행 중에 특정 자료형에 특정 함수가 있는 지 없는 지에 대한 판단
                 * 하는 것도 가능하며 호출 또한 가능
                 * 
                 * ps.
                 * type 객체를 사용하기 위해 system.Reflection 네임 스페이스를 포함
                 */
                 
                var gameobject = new GameObject(typeof(T).ToString());

                m_instance = gameobject.AddComponent<T>();

                //DontDestroyOnLoad(gameobject);
            }
            return m_instance;
        }
    }

    //인스턴스를 생성
    /*public static T creatinstance()
    {
        return Singleton<T>.instance;
    }*/

    public override void Awake()
    {
        base.Awake();

        if(m_instance == null)
        {
            m_instance = this as T;
        }
        else if(m_instance != this)
        {
            Destroy(m_instance.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}