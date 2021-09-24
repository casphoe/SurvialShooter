using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function
{
    public static IEnumerator CreateWaitSecond(float fDelay,bool RealTime = false)
    {
        /*
         * RealTime이란
         * 
         * 유니티 엔진에서 RealTime은 게임상에 시간이 아니라 현실 세계 상에 시간을 의미
         * (즉, RealTime은 Time.tiescale에 영향을 받지 않는 순수한 현실 세계 상세 시간을 뜻함)
         * 
         */
         if(RealTime)
         {
            yield return new WaitForSecondsRealtime(fDelay);
         }
         else
         {
            yield return new WaitForSeconds(fDelay);
         }
    }
    
    public static void LateCallFunc(CCompoent componet,float fDealy,System.Action<CCompoent> Callback, bool bIsRealTime = false)
    {
        var oEnumerator = Function.DoLateCallFunc(componet, fDealy, Callback, bIsRealTime);

        /*
         * 코루틴이란
         * 
         * 일반적인 함수(서브 루틴)와 달리 함수의 특정 지점부터 실행 가능한 함수를 의미 
         * 
         */
        componet?.StartCoroutine(oEnumerator);
    }
    public static IEnumerator DoLateCallFunc(CCompoent a_oComponet, float a_fDelay, System.Action<CCompoent> a_oCallback, bool a_bIsRealTime)
    {
        //Debug.LogFormat("BeFore Time : {0}", System.DateTime.Now); //System.DateTime.Now : 현재시간을 알 수 있음
        yield return Function.CreateWaitSecond(a_fDelay, a_bIsRealTime);

        //Debug.LogFormat("After Time : {0}", System.DateTime.Now);

        a_oCallback?.Invoke(a_oComponet);
    }
}