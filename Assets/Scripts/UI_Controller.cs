using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public GameObject BackPack;
    public PlayerMovement player;
    private void Update()
    {
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
}
