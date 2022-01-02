using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;
  
    public Text Daytext;
    public Text Timetext;

    public GameObject DayCycleUI;
    public GameObject PlayerStatusUI;

    public GameObject BackPack;
    public PlayerMovement player;
    public Sprite[] DayCircle;
    public Sprite[] Player_status;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        Daytext.text ="4 / " +InGameTime.instance.GameDay.ToString();
        PlayerStatusUpdate();
        DaycycleUIUpdate();
        if (Input.GetKeyDown(KeyCode.Escape)&& BackPack.activeSelf == true)
        {
            BackPack.SetActive(false);
            player.playerState = PlayerState.FreeMove;
        }
    }
    public void BackOpen()
    {
        if (BackPack.activeSelf == true)
        {
            BackPack.SetActive(false);
            player.playerState = PlayerState.FreeMove;
        }
        else if(BackPack.activeSelf == false && player.playerState == PlayerState.FreeMove)
        {
            BackPack.SetActive(true);
            player.movement.x = 0;
            player.movement.z = 0;
            player.setAnimate();
            player.playerState = PlayerState.BackpackChoosing;
        }
    }
    public void PlayerStatusUpdate()
    {
        switch (InGameTime.instance.EnergyWaste)
        {
            case 0:
                PlayerStatusUI.GetComponent<Image>().sprite = Player_status[0];
                return;
            case 1:
                PlayerStatusUI.GetComponent<Image>().sprite = Player_status[1];
                return;
            case 2:
                PlayerStatusUI.GetComponent<Image>().sprite = Player_status[2];
                return;
        }
    }
    public void DaycycleUIUpdate()
    {
        if (InGameTime.instance.PassMin < 900)
        {
            DayCycleUI.GetComponent<Image>().sprite = DayCircle[0];
        }
        else if (InGameTime.instance.PassMin >= 900 && InGameTime.instance.PassMin <= 1020)
        {
            DayCycleUI.GetComponent<Image>().sprite = DayCircle[1];
        }
        else if (InGameTime.instance.PassMin > 1020 && InGameTime.instance.PassMin <= 1140)
        {
            DayCycleUI.GetComponent<Image>().sprite = DayCircle[2];
        }
    }
}
