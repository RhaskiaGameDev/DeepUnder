using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool inMech;

    public Rigidbody rb;
    public Transform cameraHolder;
    public PlayerManager manager;
    public DisturbanceManager distManager;

    [Header("In-Base Variables")]
    public float speed;
    public float mouseSense;
    public float jumpForce;
    public float gravity;

    [Header("Mech Variables")]
    public float speedMech;
    public float turnMech;
    public float jumpForceMech;
    public float gravityMech;

    Vector2 input;

    float disturbance;

    private void Start()
    {
        Invoke("ManageDisturb", 0.5f);
    }

    void Update()
    {
        //Input
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (inMech) ManageMechMovement();
        else ManageMovement();
    }

    void ManageMovement()
    {
        //Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Calculations
        var hMove = (input.x * Time.deltaTime * speed) * transform.right;
        var vMove = (input.y * Time.deltaTime * speed) * transform.forward;

        //Move player
        rb.velocity = hMove + vMove;

        //Camera Rotate
        rb.angularVelocity = Vector3.zero;
        float x = Input.GetAxis("Mouse X");
        float y = -Input.GetAxis("Mouse Y");

        cameraHolder.Rotate(y * Time.deltaTime * mouseSense, 0, 0);
        transform.Rotate(0, x * Time.deltaTime * mouseSense, 0);
    }

    void ManageMechMovement()
    {
        //Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Speed Calculations
        var speedMult = (input.y * Time.deltaTime * speedMech);
        var turnMult = (input.x * Time.deltaTime * turnMech);

        //Slow down if rotating
        if (input.x > 0.1f || input.x < -0.1f)
            speedMult /= 5f;
        print(speedMult);

        //Setting
        rb.velocity = speedMult * transform.forward;
        rb.angularVelocity = turnMult * Vector3.up;

        //Power 
        manager.power.current -= manager.power.decrease * (Mathf.Abs(turnMult) + Mathf.Abs(speedMult));

        //Disturbances
        disturbance += Mathf.Abs(turnMult) + Mathf.Abs(speedMult);
    }

    void ManageDisturb()
    {
        distManager.CreateDisturbance(disturbance, 0.1f, transform.position);
        disturbance = 0;

        Invoke("ManageDisturb", 0.5f);
    }
}
