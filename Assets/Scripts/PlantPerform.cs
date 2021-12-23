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
    EmptySpace,
    cabbage,
    tomato,
    corn,
}
[System.Serializable]
public class PlantIdentity
{
   public int plantspaceID;
   public PlantState plantState;
   public WhichPlant which;
   //還需要建構PID
   public PlantIdentity(PlantState plant_state, WhichPlant _which,int id)
    {
        plantState = plant_state;
        which = _which;
        plantspaceID = id;
    }
}

public class PlantPerform : MonoBehaviour
{
    public Material SeedMat;
    public Material YoungMat;
    public Material MatureMat;
    public Material grownMat;

    public PlantIdentity This_Plant;
    void Update()
    {        
        
    }

    public void TimePass()
    {
        This_Plant.plantState++;
        PlantUpdate();
        Mathf.Clamp((int)This_Plant.plantState, 0, 3);
    }

    public int GetPlantState()
    {
        int state;
        state = (int)This_Plant.plantState;
        return state;
    }

    void PlantUpdate()
    {
        switch ((int)This_Plant.plantState)
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
    public void SetPlantIdentity(PlantState plant_state, WhichPlant _which,int id)
    {
        This_Plant = new PlantIdentity(plant_state, _which,id);
    }
}
