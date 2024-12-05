using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject GArrow;
    private float power;
    private float time;
    public float maxSpeed;
    [SerializeField]
    private Rigidbody Rb;

    [SerializeField]
    private float dragFactor;

    [SerializeField]
    private Transform Parent;
    private Vector2 inMouse = new Vector2();
    private Vector2 fnMouse = new Vector2();
    private double ang;
    private double angz;
    public float windAcc;
    private bool isGnd;
    void Start()
    {
        power = 0f;
    }
    void Update()
    {

        isGnd = IsGrounded();
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = Vector3.zero;
        }

        if (!isGnd)
        {
            Rb.AddForce(new Vector3(0,0,-windAcc));
        }

        if (Input.GetMouseButtonDown(1))
        {
            inMouse = Input.mousePosition;
            time = Time.fixedTime;
        }

        if (Input.GetMouseButton(1))
        {
            fnMouse = Input.mousePosition;
            ang = Math.Clamp((Math.Abs((fnMouse - inMouse).y) / Screen.height * 100) * 9 / 5, 0, 90);
            angz = Math.Clamp(((fnMouse - inMouse).x / Screen.width * 100) * 9 / 5, -90, 90);
            Debug.Log(angz);
            Parent.SetPositionAndRotation(Parent.transform.position, Quaternion.Euler(0, -(float)angz, (float)ang));
        }

        if(Input.GetMouseButtonUp(1))
        {

            time = Time.fixedTime - time;
            power = powerTime() ;

            Rb.velocity = new Vector3(power* (float)Math.Cos(ang * Mathf.Deg2Rad), power*(float)Math.Sin(ang * Mathf.Deg2Rad), power * (float)Math.Sin(angz * Mathf.Deg2Rad));
            //Rb.velocity = -powerDrag();
        }

    }

    Vector2 powerDrag() 
    {
        Vector2 deltaMouse = fnMouse - inMouse;
        Vector2 velo = (deltaMouse).normalized * Math.Clamp( (dragFactor * deltaMouse.magnitude / (Screen.height * Screen.width)),0, 50);
        return velo;
    }

    float powerTime()
    {
         return (float)Math.Abs((Math.Sin(Math.Clamp(time, 0, 5) * (Math.PI/10f))) * maxSpeed);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
    }
}
