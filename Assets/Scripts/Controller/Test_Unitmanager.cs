using System.Collections.Generic;
using UnityEngine;

public class Test_Unitmanager : MonoBehaviour
{
    public static Test_Unitmanager Instance;

    public List<SelectableUnit> UnitList = new List<SelectableUnit>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    public void AddOnUnitList(SelectableUnit unit)
    {
        UnitList.Add(unit);
    }

    public void RemoveOnUnitList(SelectableUnit unit)
    {
        UnitList.Remove(unit);
    }
}
