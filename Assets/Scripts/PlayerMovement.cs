using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField]
    bool enableCheatConsole;
    public Material PlayerMaterial;
    [Header("Movement")]
    public float currentDashCooldown;
    [SerializeField]
    float maxSpeed;
    public float Speed, SpeedOverMax;
    public LayerMask GroundMask;

    public Transform camAnchor;

    public Vector2 xMinMax;

    public float xAxis, yAxis;
    public float xSpeed, ySpeed;

    [SerializeField]
    float jumpStr;

    [Space]
    [SerializeField]
    Camera mainCam;

    [Header("Health And Collision")]
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Transform playerHitBox;

    Vector2 m_moveDir = new Vector2();
    float walkingDistance;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        rb.maxAngularVelocity = 0.5f;
        //mainCam = Camera.main;
    }

    void Update()
    {
        //Makes the player look to the mouse
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, 200, GroundMask))
        {
            Vector3 pointToLook = hit.point;
            playerHitBox.transform.LookAt(new Vector3(pointToLook.x, playerHitBox.transform.position.y, pointToLook.z), Vector3.up);
        }

        RotatePlayer();
        Jump();

    }

    private void FixedUpdate()
    {
        Movement();
        MovePlayer();
        RotatePlayer();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(jumpStr * Vector3.up);
        }
    }

    public void RotatePlayer()
    {
        yAxis -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        yAxis = Mathf.Clamp(yAxis, xMinMax.x, xMinMax.y);
        camAnchor.transform.localEulerAngles = Vector3.right * yAxis;

        xAxis += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        transform.eulerAngles = xAxis * Vector3.up;
    }

    private void MovePlayer()
    {
        //rb.AddForce(new Vector3((m_moveDir.y * -0.66f) + (m_moveDir.x * 0.66f), 0, (m_moveDir.y * 0.66f) + (m_moveDir.x * 0.66f)) * Speed, ForceMode.VelocityChange);
        if (maxSpeed < rb.linearVelocity.sqrMagnitude)
        {
            //rb.AddForce(new Vector3(m_moveDir.x, 0, m_moveDir.y) * transform.forward * SpeedOverMax, ForceMode.VelocityChange);
            rb.AddForce(m_moveDir.y * transform.forward * SpeedOverMax, ForceMode.VelocityChange);
            rb.AddForce(m_moveDir.x * transform.right * SpeedOverMax, ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(m_moveDir.y * transform.forward * Speed, ForceMode.VelocityChange);
            rb.AddForce(m_moveDir.x * transform.right * Speed, ForceMode.VelocityChange);
        }

        if (m_moveDir != Vector2.zero)
        {
            walkingDistance += Time.deltaTime;
            if (walkingDistance >= 0.3f)
            {
                walkingDistance = 0;
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    #region InputMethods
    public void Movement()
    {
        Vector2 mDir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            mDir.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            mDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mDir.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mDir.x += 1;
        }
        m_moveDir = mDir;
    }
   
        }
#endregion