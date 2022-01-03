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
        if ((System.GetComponent<InGameTime>().PassMin - 900) >= 0)
        {
            this.GetComponent<Light>().color = new Color(1, (1 - ((1 - 0.73f) / 120) * (System.GetComponent<InGameTime>().PassMin - 900)), (1 - ((1 - 0.25f) / 120) * (System.GetComponent<InGameTime>().PassMin - 900)));
        }
        else
        {
            this.GetComponent<Light>().color = new Color(1, 1f, 1f, 1);
        }
    }
    public void UpdateLightStrong()
    {
        if ((System.GetComponent<InGameTime>().PassMin - 1020f)>=0)
        {
            this.GetComponent<Light>().intensity =(6f - (6f / 120f * (System.GetComponent<InGameTime>().PassMin - 1020f)));
        }
        else
        {
            this.GetComponent<Light>().intensity = 6;
        }
    }
    public void ResetLight()
    {
        this.GetComponent<Light>().intensity = 6;
        this.GetComponent<Light>().color = new Color(1, 1, 1, 1);
    }
}
