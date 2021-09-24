using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoyStick : CCompoent
{
    public Transform Stick; //조이스틱

    private Vector3 StickFirstPos; //조이스틱 처음 위치
    private Vector3 JoyStickVec; //조이스틱 방향
    private float Radius; //조이스틱 배경의 반지름

    public float Horizontal
    {
        get
        {
            return JoyStickVec.y;
        }
    }

    public float Vertical
    {
        get
        {
            return JoyStickVec.x;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;

        StickFirstPos = Stick.position;
        
        //캔버스 크기에 대한 반지름 조절
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;

        Radius *= Can;

        PlayerManager.instance.IsMoveStick = false;
    }

    public void OnDrag(BaseEventData Event)
    {
        PlayerManager.instance.IsMoveStick = true;

        PointerEventData Data = Event as PointerEventData;

        Vector3 Pos = Data.position;

        //조이스틱을 이동시킬 방향을 구함(오른쪽,왼쪽,위,아래)
        JoyStickVec = (Pos - StickFirstPos).normalized;

        //조이스틱의 처음 위치와 현재 내가 터치하고 있는 위치의 거리를 구함
        float Distance = Vector3.Distance(Pos, StickFirstPos);

        //거리가 반지름 보다 작으면 조이스틱을 현재 터치하고 있는 곳으로 이동
        if(Distance < Radius)
        {
            Stick.position = StickFirstPos + JoyStickVec * Distance;
        }
        else //거리가 반지름 보다 커지면 조이스틱을 반지름의 크기만큼만 이동시킴
        {
            Stick.position = StickFirstPos + JoyStickVec * Radius;
        }
    }

    public void DragEnd()
    {
        Stick.position = StickFirstPos;

        JoyStickVec = Vector3.zero;

        PlayerManager.instance.IsMoveStick = false;
    }

    
}