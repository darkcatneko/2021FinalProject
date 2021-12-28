using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprtieColorChange : MonoBehaviour
{
    [SerializeField] GameObject System;
     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerColorChange()
    {
       SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in sprites)
        {
            item.color = new Color(1f-(1f-0.5f)/120*(System.GetComponent<InGameTime>().PassMin - 1020), 1f - (1f - 0.5f) / 120f * (System.GetComponent<InGameTime>().PassMin - 1020f), 1f - (1f - 0.5f) / 120f * (System.GetComponent<InGameTime>().PassMin - 1020f) );
        }
    }
}
