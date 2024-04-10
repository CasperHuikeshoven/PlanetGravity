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

    [Header("Movement")]
    public float movementSpeed;
    public float jumpHeight;
    
    void Start()
    {
        
    }

    void Update()
    {
        //movement input for moving to the front, the back, the left and the right
        if(Input.GetKey(forwardKey))    transform.position += transform.forward*movementSpeed*Time.deltaTime;
        if(Input.GetKey(backKey))       transform.position -= transform.forward*movementSpeed*Time.deltaTime;
        if(Input.GetKey(rightKey))      transform.position += transform.right*movementSpeed*Time.deltaTime;
        if(Input.GetKey(leftKey))       transform.position -= transform.right*movementSpeed*Time.deltaTime;
    }
}
