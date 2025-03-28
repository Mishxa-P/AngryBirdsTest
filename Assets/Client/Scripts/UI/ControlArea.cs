using UnityEngine;
using UnityEngine.EventSystems;

public class ControlArea : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private BirdsLauncher _birdsLauncher;

    private Camera _mainCamera;
    private bool _isDragging = false;
    private bool _isDisabled = false;
    private void OnEnable()
    {
        LevelEventManager.OnLevelCompleted.AddListener(DisableInput);
    }
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isDisabled)
        {
            return;
        }
        Vector3 pullPosition = _mainCamera.ScreenToWorldPoint(eventData.position);
        pullPosition.z = _birdsLauncher.transform.position.z;
        if (_birdsLauncher.GetComponent<SpriteRenderer>().bounds.Contains(pullPosition))
        {
            _isDragging = true;
        }

        _birdsLauncher.UseActiveBirdSkill();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_isDisabled)
        {
            return;
        }
        if (_isDragging)
        {
            Vector3 pullPosition = _mainCamera.ScreenToWorldPoint(eventData.position);
            pullPosition.z = _birdsLauncher.transform.position.z;
            _birdsLauncher.UpdateVelocity(pullPosition);
            _birdsLauncher.UpdateTrajectory(pullPosition);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isDisabled)
        {
            return;
        }
        if (_isDragging)
        {
            _isDragging = false;
            _birdsLauncher.Launch();
        }
    }
    private void DisableInput()
    {
        _isDisabled = true;
    }
}
