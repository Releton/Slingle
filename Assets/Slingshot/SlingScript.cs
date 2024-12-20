using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingScript : MonoBehaviour
{
    private GameObject GArrow;
    private float power;
    private float time = 0;
    public float maxSpeed;
    private Rigidbody Rb;

    public GameObject GameMan;
    private GameManager GM;
    [SerializeField]
    private Transform Parent;
    private Vector2 inMouse = new Vector2();
    private Vector2 fnMouse = new Vector2();
    private double ang;
    private double angz;
    [SerializeField]
    private Transform midRubber;
    public TrajectoryPrediction trajectory;
    public static Transform posn;
    public float overallMaxSpeed;
    public int displayLife;
    public static int life;
    public GameObject Light;
        public Jumpscaring jumpscaring;
    public static bool isGameEnded = false;

    [SerializeField] UI_Inventory uiInventory;
    public float maxRubber;

    private void Awake()
    {
        resetSling();
        posn = this.gameObject.transform;
        life = displayLife;
    }

    void Start()
    {
        uiInventory.SetInventory();
        GM = GameMan.GetComponent<GameManager>();
        power = 0f;
        Rb = GameManager.CurrentStack.Peek().GetComponent<Rigidbody>();
    }
    private void End()
    {
        jumpscaring.JumpScare();
    }
    void Update()
    {
        if (isGameEnded) return;
        if (life == 0 && isGameEnded == false)
        {
            End();
            isGameEnded = true;
            return;
        }//End

        if ((Rb == null  || GameManager.hasCurChanged) && GameManager.isBallLeft)
        {
            Rb = GameManager.CurrentStack.Peek()?.GetComponent<Rigidbody>();
            Debug.Log(GameManager.CurrentStack.Peek().GetComponent<BallScript>().maxSpeed);
            maxSpeed = GameManager.CurrentStack.Peek().GetComponent<BallScript>().maxSpeed;
            GameManager.curChanged();
            uiInventory.SetInventory();
        }
        if (Rb != null && GameManager.CurrentStack.Peek() != null && GameManager.isBallLeft)
        {

            if (BallScript.isActivated)
            {
                Rb.AddForce(new Vector3(0, 0, -GameManager.CurrentStack.Peek().GetComponent<BallScript>().type.windSpeed * GameManager.windLevel));
            }


                if (Input.GetMouseButtonDown(1))
                {
                    inMouse = Input.mousePosition;
                    time += Time.deltaTime;
                }

                if (Input.GetMouseButton(1))
                {
                    time += Time.deltaTime;
                    GameManager.CurrentStack.Peek().transform.position = midRubber.position;
                    fnMouse = Input.mousePosition;
                    ang = Math.Clamp(((fnMouse - inMouse).y) / -Screen.height * 100 * 9 / 5, 0, 58);
                    angz = Math.Clamp(((fnMouse - inMouse).x / Screen.width * 100) * 9 / 5 , -69, 69);
                    power = powerTime(time);

                    Debug.Log("Power" + power);
                    if(power > 0)
                    {
                        Parent.transform.localScale = new Vector3(percentPower(time) * maxRubber / 100f, Parent.transform.localScale.y, Parent.transform.localScale.z);
                        trajectory.PredictTrajectory((float)ang, (float)angz, power * (float)Math.Cos(ang * Mathf.Deg2Rad), power * (float)Math.Sin(ang * Mathf.Deg2Rad), power * (float)Math.Sin(angz * Mathf.Deg2Rad), GameManager.CurrentStack.Peek());

                    }
                    Parent.SetPositionAndRotation(Parent.transform.position, Quaternion.Euler(0, -(float)angz, (float)ang));
                }

                if (Input.GetMouseButtonUp(1))
                {
                    time += Time.deltaTime;
                    power = powerTime(time);
                    time = 0;
                    GameManager.CurrentStack.Peek().GetComponent<BallScript>().activateBall();
                    Rb.velocity = new Vector3(power * (float)Math.Cos(ang * Mathf.Deg2Rad), power * (float)Math.Sin(ang * Mathf.Deg2Rad), power * (float)Math.Sin(angz * Mathf.Deg2Rad));
                    resetSling();
                    //Rb.velocity = -powerDrag();

                }
        }
        

    }

    float percentPower(float t)
    {
        return powerTime(t)/(5f * maxSpeed * overallMaxSpeed) * 100f;
    }
    float powerTime(float t)
    {
        return (float)((1 / (1 + Math.Pow(Math.E, -t))) - 0.5f) * 10 * maxSpeed * overallMaxSpeed;
    }

    

    public void resetSling()
    {
        Parent.transform.localScale = Vector3.zero;
    }


}
