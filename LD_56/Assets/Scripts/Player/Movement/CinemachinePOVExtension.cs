using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [Header("Look Speed")]
    [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;

    [SerializeField]
    [Tooltip("Max angle to look up and down")]
    private float clampAngle = 80f;

    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void Awake() {
        inputManager = InputManager.Instance;
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow) {
            if (stage == CinemachineCore.Stage.Aim) {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                if (inputManager == null) return;
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
