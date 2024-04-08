﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{

    [Header("GameObjects")]
    public CharacterController controller; 
    public Transform planetT; 
    public Transform playerT;

    [Header("Speed")]
    public float speed = 12f; 
    public float runSpeed = 12f; 
    public float normalSpeed = 6f; 

    [Header("Height")]
    public float crouchHeight = 0.5f; 
    public float normalHeight = 2f; 

    [Header("Gravity")]
    public float gravity = -9.81f; 
    public float jumpHeight = 10f; 
    public Transform groundCheck; 
    public float groundDistance = 0.4f; 
    public LayerMask groundMask; 
    private Vector3 initialOffset;
    public float rotateSpeed = 5f;

    Vector3 velocity; 
    public bool isGrounded; 

    private bool spawnYN = true; 

    private void Start(){
        initialOffset = playerT.position - planetT.position;
    }

    // Update is called once per frame
    private void Update()
    {
        playerT.position = planetT.position + planetT.rotation * initialOffset;
        RotateToCenter();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = 0; 
        }

        //Crouch
        if(!Input.GetKey(KeyCode.LeftControl)){
            controller.height = normalHeight;
        }
        if(Input.GetKey(KeyCode.LeftControl)){
            controller.height = normalHeight*crouchHeight;
        }

        //Sprint
        if(!Input.GetKey(KeyCode.LeftShift)){
            speed = normalSpeed;
            Debug.Log(speed);
        }
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded){
            speed = runSpeed;
                    Debug.Log(speed);
        }
    
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; 

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime; 
                
        controller.Move(velocity * Time.deltaTime);

        
    }

    private void RotateToCenter()
    {
        Vector3 gravityDirection = (playerT.position - planetT.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(playerT.up, gravityDirection) * playerT.rotation;
        playerT.rotation = Quaternion.Slerp(playerT.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
