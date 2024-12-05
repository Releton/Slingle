using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrajectoryPrediction : MonoBehaviour
{

    [SerializeField] private LineRenderer trajectoryLine;
    [SerializeField] private int points;
    [SerializeField, Range(0,0.75f)] private float rangeCovered;
    public float HeightOfLaunch;
    public Transform RubberMid;


    void Start()
    {
        trajectoryLine.positionCount = points;
    }

    void Update()
    {

    }
    public void PredictTrajectory(float ang, float angz, float initXV, float initYV, float initZV, GameObject projectile)
    {
        Rigidbody projectRB = projectile.GetComponent<Rigidbody>();
        float rangeXY = initXV * 2 * initYV / Physics.gravity.y;
        float intervalXY = rangeXY * rangeCovered / points;
        float accWind = -projectile.GetComponent<BallScript>().windSpeed * GameManager.windLevel/projectRB.mass;
        if (initXV == 0) return;
        float timeInterval = intervalXY / initXV;
        float rangeZ = (accWind != 0) ?  initXV * 2 * initZV / accWind : 0;
        int pnt = 0;

        /*if( rangeXY != 0 && rangeZ != 0)
        {
            for (float x = 0; pnt < points; x += intervalXY, pnt++)
            {
                float y = x * Mathf.Tan(ang * Mathf.Deg2Rad) * (1f - x / rangeXY) + HeightOfLaunch;
                float z = x * Mathf.Tan(angz * Mathf.Deg2Rad) * (1f - x / rangeZ);

                trajectoryLine.SetPosition(pnt, new Vector3(-x + RubberMid.position.x,-y+RubberMid.position.y,-z+RubberMid.position.z));
            }
        }

        if(rangeXY == 0 && rangeZ != 0)
        {
            for (float x = 0; pnt < points; x += intervalXY, pnt++)
            {   
                
            }
        }*/
        for(float t = 0; pnt < points; pnt++, t+= timeInterval)
        {
            float y = initYV * t - (0.5f * Physics.gravity.y * t * t);
            float x = initXV * t;
            float z = initZV * t - (0.5f * accWind * t * t);
            trajectoryLine.SetPosition(pnt, new Vector3(-x + RubberMid.position.x, -y + RubberMid.position.y, -z + RubberMid.position.z));
        }

    }
}
