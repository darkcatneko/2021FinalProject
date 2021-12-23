using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantState
{
    seed,
    young,
    mature,
    grown,
}

public enum WhichPlant
{
    cabbage,
    tomato,
    corn,
}

public class PlantPerform : MonoBehaviour
{
    public Material SeedMat;
    public Material YoungMat;
    public Material MatureMat;
    public Material grownMat;
    
    public PlantState plantState = PlantState.seed;
    [SerializeField] WhichPlant which;
    void Update()
    {        
        
    }

    public void TimePass()
    {
        plantState++;
        PlantUpdate();
        Mathf.Clamp((int)plantState, 0, 3);
    }

    public int GetPlantState()
    {
        int state;
        state = (int)plantState;
        return state;
    }

    void PlantUpdate()
    {
        switch ((int)plantState)
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
}
