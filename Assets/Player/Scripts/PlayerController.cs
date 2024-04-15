//Script for a player controller, for both first person and third person
// - Casper Huikeshoven

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Camera")]
    public Camera camera;
    public bool firstPersonCamera;

    [Header("Key Inputs")]
    public string forwardKey;
    public string backKey;
    public string rightKey;
    public string leftKey;
    public string sprintKey;
    public string jumpKey;

    [Header("Movement")]
    public float movementSpeed;
    public float jumpHeight;
    public bool sprint;
    public float sprintSpeed;

    [Header("Gravity")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.2f;
    bool isGrounded;
    public float gravityStrength;
    float yVelocity = 1f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    [Header("Planet Gravity")]
    public bool planetGravity;
    public Transform planetTransform;
    public Transform globalPlayerTransform;
    public float rotateSpeed = 5f;
    
    void Start()
    {
        
    }

    void Update()
    {
        Movement();
        MouseLook();
    }

    private void Movement(){
        //movement input for moving to the front, the back, the left and the right
        if(Input.GetKey(forwardKey)) transform.position += transform.forward*movementSpeed*Time.deltaTime; //forward
        if(Input.GetKey(forwardKey) && Input.GetKey(sprintKey) && sprint) transform.position += transform.forward*Time.deltaTime*sprintSpeed; //sprinting forward
        if(Input.GetKey(backKey)) transform.position -= transform.forward*movementSpeed*Time.deltaTime; //back
        if(Input.GetKey(rightKey)) transform.position += transform.right*movementSpeed*Time.deltaTime; //right
        if(Input.GetKey(leftKey)) transform.position -= transform.right*movementSpeed*Time.deltaTime; //left
        if(Input.GetKey(jumpKey)) Jump(); //jump
        AddGravity();
    }

    private void MouseLook(){
        Cursor.lockState = CursorLockMode.Locked;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY; 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

    }

    private void AddGravity(){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        yVelocity += gravityStrength * Time.deltaTime;
        if(planetGravity) RotateForPlanetGravity();
        if(isGrounded && yVelocity < 0) yVelocity = 0f;

        transform.position += transform.up*yVelocity*Time.deltaTime;

    }

    private void RotateForPlanetGravity(){

        Vector3 gravityDirection = (transform.position - planetTransform.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

    }

    private void Jump(){
        if(isGrounded){
            yVelocity = Mathf.Sqrt(jumpHeight * 2f * -gravityStrength);
            Debug.Log("Jump");
        }
    }
}
