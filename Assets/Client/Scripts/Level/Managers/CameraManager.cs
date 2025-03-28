using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraManager : MonoBehaviour
{
    public static CameraManager Singleton { get; private set; }

    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        if (Singleton == null)
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            Singleton = this;
        }
        else
        {
            Debug.LogError("Camera manager is already exist!");
        }
    }
    public void SetFollowTarget(Transform target)
    {
        _virtualCamera.Follow = target;
        _virtualCamera.PreviousStateIsValid = false;
    }
}
