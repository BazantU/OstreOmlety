using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;

    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    public DzwiekHandler dzwiekHandler;

    public Transform reka;
    bool graloDzwiekPodnoszenia = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>(); 
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if(canMove){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }else{
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }


        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        characterController.Move(moveDirection * Time.deltaTime);
        
        if(canMove && characterController.isGrounded && (characterController.velocity.x > 0.2f || characterController.velocity.z > 0.2f)){
            dzwiekHandler.graj("kroki");
        }

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if(reka.childCount > 0 && !graloDzwiekPodnoszenia){
            graloDzwiekPodnoszenia = true;
            dzwiekHandler.graj("item");
        }
        if(reka.childCount <= 0){graloDzwiekPodnoszenia = false;}
    }
}