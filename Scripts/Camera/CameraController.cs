using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera ControllerCamera; //플레이어를 따라다닐 카메라
    public GameObject Player; //카메라가 따라다닐 플레이어

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSetting.IsGameStart == true)
        {
            FollowPlayer();
        }
        else
        {
            return;
        }
    }

    void FollowPlayer()
    {
        ControllerCamera.transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z - 22f);

        ControllerCamera.transform.LookAt(Player.transform);
    }
}