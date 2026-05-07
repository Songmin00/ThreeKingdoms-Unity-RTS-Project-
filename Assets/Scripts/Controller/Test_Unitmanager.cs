using System.Collections.Generic;
using UnityEngine;

public class Test_Unitmanager : MonoBehaviour
{
    public static Test_Unitmanager Instance;

    public List<SelectableUnit> UnitList = new List<SelectableUnit>();
    public List<SelectableUnit> SelectedUnitList = new List<SelectableUnit>();

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

    //전체 유닛 관리
    public void AddOnUnitList(SelectableUnit unit) => UnitList.Add(unit);
    public void RemoveOnUnitList(SelectableUnit unit)
    {
        UnitList.Remove(unit);
        RemoveOnSelectionList(unit);
    }
    

    //선택 유닛 관리
    public void SetUnitSelected(SelectableUnit unit, bool isSelected)
    {
        if (isSelected == true) AddOnSelectionList(unit);
        else RemoveOnSelectionList(unit);
    }

    private void AddOnSelectionList(SelectableUnit unit)
    {
        SelectedUnitList.Add(unit);
        unit.SetUnitSelected(true);
    }
    private void RemoveOnSelectionList(SelectableUnit unit)
    {
        SelectedUnitList.Remove(unit);
        unit.SetUnitSelected(false);
    }    
}
