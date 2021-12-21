using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFarmSpace : MonoBehaviour
{
    public GameObject CabbagePrefab;
    public GameObject TomatoPrefab;
    public GameObject CornPrefab;

    public static bool InEmptyFarmRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InEmptyFarmRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.name + "is out");
        }
    }

    
    
}
