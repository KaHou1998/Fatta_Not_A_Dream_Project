using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public Rigidbody rb;
    public float normalSpeed = 6f;
    public float sprintSpeed = 100f;
    public float dodgeSpeed = 1000f;
    public float jump = 10f;
    public float turnSmoothTime = 0.1f;
    public float Gravity = -9.8f;
    public CharacterBase characterBase;

    private Animator anim;
    private Transform cam;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private float movementSpeed;

    public void Awake()
    {
        anim = characterBase.animator;
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Sprint();
        Movement();
        Jump();
        
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {           
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
        }
        anim.SetFloat("NormalizedSpeed", direction.magnitude);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && transform.localPosition.y < -2f)
        { 
            velocity.y = jump;
        }
        else
        {
            velocity.y += Gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void Sprint()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintSpeed;
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = normalSpeed;
        }
    }

    void Dodge(Vector3 direction)
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Dodging");
            rb.AddForce(direction * dodgeSpeed, ForceMode.VelocityChange);
        }      
    }
}
