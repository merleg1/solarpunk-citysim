using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public PlayerInputActions inputActions;

    public float panSpeed = 20f;
    public float zoomSpeed = 2f;
    public float rotationSpeed = 50f;

    private Vector2 _horizontalMovement;
    private Vector2 _rotation;
    private bool _zoomIn;
    private bool _zoomOut;

    #region Enable/Disable

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    #endregion

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += ctx => _horizontalMovement = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => _horizontalMovement = Vector2.zero;
        inputActions.Player.Look.performed += ctx => _rotation = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => _rotation = Vector2.zero;
        inputActions.Player.ZoomIn.performed += ctx => _zoomIn = true;
        inputActions.Player.ZoomIn.canceled += ctx => _zoomIn = false;
        inputActions.Player.ZoomOut.performed += ctx => _zoomOut = true;
        inputActions.Player.ZoomOut.canceled += ctx => _zoomOut = false;
    }
    private void Update()
    {
        Vector3 panDirection = new Vector3(_horizontalMovement.x, 0, _horizontalMovement.y).normalized;
        transform.Translate(panSpeed * Time.deltaTime * panDirection, Space.World);

        if (_zoomIn)
        {
            Vector3 zoom = transform.position + Time.deltaTime * zoomSpeed * transform.forward;
            transform.position = zoom;
        }
        else if (_zoomOut)
        {
            Vector3 zoom = transform.position - Time.deltaTime * zoomSpeed * transform.forward;
            transform.position = zoom;
        }

        if (Mathf.Abs(_rotation.x) > 0.2f)
        {
            float rotationAmountX = _rotation.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationAmountX);
        }

        if (Mathf.Abs(_rotation.y) > 0.2f)
        {
            float rotationAmountY = _rotation.y * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right * -rotationAmountY);
        }
    }

}
