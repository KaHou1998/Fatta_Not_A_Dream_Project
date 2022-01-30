using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum Status
{
    OnAir,
    OnGround
}

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public float normalSpeed = 6f;
    public float sprintSpeed = 100f;
    public float dodgeSpeed = 1000f;
    public float jump = 10f;
    public float turnSmoothTime = 0.1f;
    
    public float dashTime;
    public float dashSpeed;
    public CharacterBase characterBase;

    private Animator anim;
    private Transform cam;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private float movementSpeed;
    private Status status;
    private float gravityStrength = -9.8f;

    private Vector3 moveDir;

    private bool isDashing;
    public void Awake()
    {
        anim = characterBase.animator;
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UpdateStatus();

        switch (status)
        {
            case Status.OnAir:
                Gravity();
                break;

            case Status.OnGround:
                if(!isDashing)
                {
                    Dash();
                    Movement();
                    Sprint();
                    Jump();
                }             
                break;
        }       
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

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
        }
        anim.SetFloat("NormalizedSpeed", direction.magnitude);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            velocity.y = jump;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateStatus()
    {
        if (transform.localPosition.y < -2f)
        {
            status = Status.OnGround;
        }
        else
        {
            status = Status.OnAir;
        }
    }

    void Gravity()
    {
        velocity.y += gravityStrength * Time.deltaTime;
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

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) )
        {
            isDashing = true;
            StartCoroutine("Dashing");
        }      
    }

    IEnumerator Dashing()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            controller.Move(moveDir * dashSpeed * Time.deltaTime);
            isDashing = false;
            yield return null;
        }
        
    }
}
