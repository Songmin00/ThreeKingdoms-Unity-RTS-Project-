using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] GameObject _selectionCircle;

    private void OnEnable()
    {
        Test_Unitmanager.Instance.AddOnUnitList(this);
    }

    private void OnDisable()
    {
        Test_Unitmanager.Instance.RemoveOnUnitList(this);
    }

    public void SetUnitSelected(bool isSelected)
    {
        if (_selectionCircle == null)
        {
            Debug.LogError($"{gameObject.name} : 선택 확인 원형 이미지 미할당");
            return;
        }
        _selectionCircle.gameObject.SetActive(isSelected);
    }
}
