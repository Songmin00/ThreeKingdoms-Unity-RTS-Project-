using UnityEngine;
using UnityEngine.InputSystem;



public class SelectionHandler : MonoBehaviour
{
    [Tooltip("드래그 표시용 사각형 박스 외형")]
    [SerializeField] private RectTransform _selectionBoxVisual;
    private InputSystem_Actions _inputActions;
    private Vector2 _dragStartPos;
    private bool _isDragging = false;
    private bool _isFirstFrame = false;

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
        _selectionBoxVisual.gameObject.SetActive(false);
    }
}
