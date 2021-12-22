using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFarmSpace : MonoBehaviour
{
    public GameObject CabbagePrefab;
    //public GameObject TomatoPrefab;
    //public GameObject CornPrefab;

    public GameObject testVFX;

    public static bool InEmptyFarmRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InEmptyFarmRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
           Instantiate(CabbagePrefab, this.transform.position, Quaternion.Euler(45f,180f,0));
           GameObject vfx = Instantiate(testVFX, this.transform.position, Quaternion.Euler(45f, 0, 0));
            Destroy(vfx, 1f);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InEmptyFarmRange = false;
            Debug.Log(other.name + "is out");
        }
    }

    
    
}
