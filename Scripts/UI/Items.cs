using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Items : CCompoent,IPointerClickHandler
{

    public GameSetting.Item Item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
        {

            switch(Item)
            {
                case GameSetting.Item.HpRecover:
                    GameManager.instance.item = GameSetting.Item.HpRecover;
                    GameManager.instance.ItemStat("HpRecover");
                    PlayerManager.instance.PurchasePanel.SetActive(true);
                    break;
                case GameSetting.Item.Sheild:
                    GameManager.instance.item = GameSetting.Item.Sheild;
                    GameManager.instance.ItemStat("Shield");
                    PlayerManager.instance.PurchasePanel.SetActive(true);
                    break;
                case GameSetting.Item.Mine:
                    GameManager.instance.item = GameSetting.Item.Mine;
                    GameManager.instance.ItemStat("Mine");
                    PlayerManager.instance.PurchasePanel.SetActive(true);
                    break;
                case GameSetting.Item.Scissors:
                    GameManager.instance.item = GameSetting.Item.Scissors;
                    GameManager.instance.ItemStat("Scissors");
                    PlayerManager.instance.PurchasePanel.SetActive(true);
                    break;
                case GameSetting.Item.GunLevelUp:
                    GameManager.instance.item = GameSetting.Item.GunLevelUp;
                    GameManager.instance.ItemStat("GunLevelUp");
                    PlayerManager.instance.PurchasePanel.SetActive(true);
                    break;
                case GameSetting.Item.Bullet:
                    GameManager.instance.item = GameSetting.Item.Bullet;
                    GameManager.instance.ItemStat("Bullet");
                    if(GameManager.instance.Gun == GameSetting.Guns.Ssg)
                    {
                        if(GameManager.instance.Player == GameSetting.Player.Girl)
                        {
                            return;
                        }
                        else
                        {
                            PlayerManager.instance.PurchasePanel.SetActive(true);
                        }
                    }
                    else
                    {
                        PlayerManager.instance.PurchasePanel.SetActive(true);
                    }
                    break;
            }
        }
    }
}
