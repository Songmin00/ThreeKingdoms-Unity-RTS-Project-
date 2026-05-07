using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] GameObject _selectionCircle;
    private bool _isSelected = false;

    public bool IsSelected => _isSelected;
    private void Start()
    {
        Test_Unitmanager.Instance.AddOnUnitList(this);
    }    

    private void OnDestroy()
    {
        Test_Unitmanager.Instance.RemoveOnUnitList(this);
    }

    public void SetUnitSelected(bool isSelected)
    {
        _isSelected = isSelected;

        if (_selectionCircle == null)
        {
            Debug.LogError($"{gameObject.name} : 선택 확인 원형 이미지 미할당");
            return;
        }
        _selectionCircle.gameObject.SetActive(isSelected);        
    }
}
