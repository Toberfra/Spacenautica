using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 8f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    public float prevMovementX = 0;
    public float prevMovementZ = 0;
    public float prevOrientationr = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private bool ngravity = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        if (ngravity)
        {
            curSpeedX = prevMovementX;
            curSpeedY = prevMovementZ;
        }
        else
        {   
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        }
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded && !ngravity)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        if (characterController.isGrounded && ngravity)
        {
            moveDirection.y = 0;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);
	
	    if (Input.GetKey(KeyCode.F) && !ngravity)
	    {
	        gravity = 0f;
	        ngravity = true;
            prevMovementX = curSpeedX;
            prevMovementZ = curSpeedY;
	    }

	    if (Input.GetKey(KeyCode.G) && ngravity)
	    {
	        gravity = 8f;
	        ngravity = false;
            prevMovementX = 0;
            prevMovementZ = 0;
	    }
	    if (ngravity)
	    {
            curSpeedX = prevMovementX;
        }
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
    public void Stop()
    {
        moveDirection = new Vector3 (0, 0, 0);
    }
}