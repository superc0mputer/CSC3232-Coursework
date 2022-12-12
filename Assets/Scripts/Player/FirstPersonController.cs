using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float jumpSpeed = 8.0f;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float gravity = 20.0f;
    
    private Vector3 moveDirection = Vector3.zero;
    
    private float xRotation = 0;

    private CharacterController characterController;
    
    private bool canMove = true;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        //Lock cursor
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
        
    }
    
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        //Left Shift to Run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float currentSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        float moveDirectionY = moveDirection.y;
        moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);
        
        //Jump
        if (Input.GetKey(KeyCode.Space) && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = moveDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        
        //Move controller
        characterController.Move(moveDirection * Time.deltaTime);
        
        //Camera and Player rotation
        if (canMove)
        {
            xRotation -= Input.GetAxis("Mouse Y") * lookSpeed;
            xRotation = Mathf.Clamp(xRotation, -90f, 90); //Limit Viewing Field

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
