using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Center : MonoBehaviour
{
    public static BGM_Center instance;
    public float volume = 1f;
    private void Awake()
    {
        instance = this;
    }
}
