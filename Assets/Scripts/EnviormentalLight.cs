using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentalLight : MonoBehaviour
{
    [SerializeField] GameObject System;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void UpdateLight()
    {
        this.GetComponent<Light>().color = new Color(1, (1 - ((1 - 0.73f) / 120) * (System.GetComponent<InGameTime>().PassMin -900)), (1 - ((1 - 0.25f) / 120) * (System.GetComponent<InGameTime>().PassMin - 900)));
    }
    public void UpdateLightStrong()
    {
        this.GetComponent<Light>().intensity =(6f - (6f / 120f * (System.GetComponent<InGameTime>().PassMin - 1020f)));
    }
}
