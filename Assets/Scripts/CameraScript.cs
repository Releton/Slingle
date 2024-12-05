using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 initCam;
    public Quaternion initRotation;
    public Vector3 offsetCam;
    public static Vector3 offset;
    private void Awake()
    {
        offset = offsetCam;
    }
}
