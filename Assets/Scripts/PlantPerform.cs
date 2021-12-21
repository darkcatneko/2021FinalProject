using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPerform : MonoBehaviour
{
    public Material SeedMat;
    public Material YoungMat;
    public Material MatureMat;
    public Material grownMat;
    public enum PlantState
    {
        seed,
        young,
        mature,
        grown,
    }
    PlantState plantState = PlantState.seed;       
    void Update()
    {        
        switch((int)plantState)
        {
            case 0:
                this.gameObject.GetComponent<MeshRenderer>().material = SeedMat;
                break;
            case 1:
                this.gameObject.GetComponent<MeshRenderer>().material = YoungMat;
                break;
            case 2:
                this.gameObject.GetComponent<MeshRenderer>().material = MatureMat;
                break;
            case 3:
                this.gameObject.GetComponent<MeshRenderer>().material = grownMat;
                break;

        }
    }

    public void TimePass()
    {
        plantState++;
        Mathf.Clamp((int)plantState, 0, 3);
    }

    public int GetPlantState()
    {
        int state;
        state = (int)plantState;
        return state;
    }
}
