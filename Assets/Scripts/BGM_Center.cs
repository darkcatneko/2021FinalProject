using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Center : MonoBehaviour
{
    public static BGM_Center instance;
    public float volume = 0.5f;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        this.GetComponent<AudioSource>().volume = volume;
    }
}
