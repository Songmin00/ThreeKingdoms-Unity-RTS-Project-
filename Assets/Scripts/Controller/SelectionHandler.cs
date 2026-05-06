using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class SelectionHandler : MonoBehaviour
{
    [Tooltip("드래그 표시용 사각형 박스 외형")]
    [SerializeField] private RectTransform _selectionBoxVisual;
    [Tooltip("단일타겟 감지용 최소 드래그 거리")]
    [SerializeField] private float _minimumDragDistance = 5f;
    private InputSystem_Actions _inputActions;
    private Vector2 _dragStartPos;
    private bool _isDragging = false;
    private bool _isFirstFrame = false;
    private bool _isShiftPressed => _inputActions.Player.Shift.IsPressed();

    private Test_Unitmanager _unitmanager;


    private void Awake()
    {
        _inputActions = new InputSystem_Actions();        
        _selectionBoxVisual.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _inputActions.Player.Select.started += OnSelectStarted;
        _inputActions.Player.Select.canceled += OnSelectCanceled;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Start()
    {
        _unitmanager = Test_Unitmanager.Instance;
    }


    private void OnSelectStarted(InputAction.CallbackContext context)
    {        
        _isDragging = true;
        _isFirstFrame = true;             
    }

    private void Update()
    {
        if (!_isDragging) return;
        
        Vector2 currentMousePos = _inputActions.Player.MousePosition.ReadValue<Vector2>();

        if (_isFirstFrame)
        {
            _dragStartPos = _inputActions.Player.MousePosition.ReadValue<Vector2>();
            _selectionBoxVisual.gameObject.SetActive(true);
            _selectionBoxVisual.sizeDelta = Vector2.zero;
            _selectionBoxVisual.anchoredPosition = _dragStartPos;
            _isFirstFrame = false;
            return;
        }

        float width = currentMousePos.x - _dragStartPos.x;
        float height = currentMousePos.y - _dragStartPos.y;
        
        _selectionBoxVisual.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        _selectionBoxVisual.anchoredPosition = _dragStartPos + new Vector2(width / 2, height / 2);
    }

    private void OnSelectCanceled(InputAction.CallbackContext context)
    {
        _isDragging = false;
        _isFirstFrame = false;

        float distance = Vector2.Distance(_dragStartPos, _inputActions.Player.MousePosition.ReadValue<Vector2>());

        if (distance < _minimumDragDistance)
        {
            SelectSingleUnit();
        }
        else
        {
            SelectUnits();
        }            
        _selectionBoxVisual.gameObject.SetActive(false);
    }

    private void SelectUnits()
    {
        if (!_isShiftPressed)
        {
            ClearAllSelection();
        }

        Vector2 min = Vector2.Min(_dragStartPos, _inputActions.Player.MousePosition.ReadValue<Vector2>());
        Vector2 max = Vector2.Max(_dragStartPos, _inputActions.Player.MousePosition.ReadValue<Vector2>());
        Rect selectionRect = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);

        foreach (SelectableUnit unit in _unitmanager.UnitList)
        {            
            Vector2 unitScreenPos = Camera.main.WorldToScreenPoint(unit.transform.position);

            if (selectionRect.Contains(unitScreenPos))
            {                
                unit.SetUnitSelected(true);                                        
            }            
        }
    }

    private void SelectSingleUnit()
    {
        bool isShiftPressed = _isShiftPressed;

          

        Ray ray = Camera.main.ScreenPointToRay(_inputActions.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200f))
        {
            SelectableUnit unit = hit.collider.GetComponent<SelectableUnit>();
            if (unit == null)
            {
                return;                
            }

            if (isShiftPressed)
            {
                unit.SetUnitSelected(!unit.IsSelected);
            }
            else
            {
                ClearAllSelection();
                unit.SetUnitSelected(true);
            }
            
        }
    }
    private void ClearAllSelection()
    {
        foreach (var unit in _unitmanager.UnitList)
        {
            unit.SetUnitSelected(false);
        }
    }
}
