using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController Instance { get; private set; }
    [SerializeField] private CinemachineVirtualCamera virtualCamera;



    private void Awake()
    {
        Instance = this;
    }

    public void SetCameraTarget(Transform target)
    {
        // Set the virtual camera's "Look At" target to the target transform
        virtualCamera.LookAt = target;
        virtualCamera.Follow = target;
    }
}