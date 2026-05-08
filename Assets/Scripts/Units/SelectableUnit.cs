using Google.GData.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] GameObject _selectionCircle;
    private NavMeshAgent _agent;
    private bool _isSelected = false;

    public bool IsSelected => _isSelected;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        Test_Unitmanager.Instance.AddOnUnitList(this);
    }

    private void Update()
    {
        if (_agent != null && _agent.hasPath)
        {
            if (_agent.remainingDistance < 3.0f)
            {
                if (_agent.velocity.sqrMagnitude < 0.1f)
                {
                    _agent.ResetPath();
                }
            }
        }
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
