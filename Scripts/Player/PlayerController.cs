using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;
    private Vector2 currentMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")] public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook=true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Start is called before the first frame update

    //components

    private Rigidbody rig;
    public static PlayerController instance;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

        instance = this;
    }



    //fixeduptade The reason I use it is to make the physics work better by running in 0.02 seconds and not every frame.
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook == true)
        {
            CameraLook();
        }
        
    }

    private void Move()
    {
        Vector3 dir = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;

        rig.velocity = dir;

    }

    private void CameraLook()
    {
        //look up
        camCurXRot += mouseDelta.y * lookSensitivity;
        //clamp the max and min look
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //WASD movement.
        if (context.phase == InputActionPhase.Performed)
        {
            //will walk in the specified direction...
            currentMovementInput = context.ReadValue<Vector2>();
        }
        //if we leave space
        else if (context.phase == InputActionPhase.Canceled)
        {   
            //will stop
            currentMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        //if we press space..
        if (context.phase == InputActionPhase.Started)
        {
            //Are we on the ground? ground layer
            if (isGrounded())
            {
                //jump
                rig.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
                
            }
        }
    }

    bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray (transform.position + (transform.forward * 0.2f)+(Vector3.up*0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+(Vector3.up*0.01f),Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f)+(Vector3.up*0.01f),Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f)+(Vector3.up*0.01f),Vector3.down)

        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f),Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f),Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f),Vector3.down);
        
    }

    public void ToggleCursor(bool toggle)
    {
        //if the cursor is locked set it to none otherwise set the cursor to lock mode
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        //can look set to opposite of toggle
        canLook = !toggle;
    }
}
