using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundImage : CCompoent
{

    public Sprite[] SoundImages = new Sprite[2];
    public Image Sound;

    void Start()
    {
        if(GameManager.instance.IsSoundOff)
        {
            Sound.sprite = SoundImages[1];
        }
        else
        {
            Sound.sprite = SoundImages[0];
        }
    }
}
