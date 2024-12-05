using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberScript : MonoBehaviour
{
    [SerializeField]
    private LineRenderer rubber;
    [SerializeField]
    private Transform midTrans;

    [SerializeField]
    private Transform pointAttach1;
    [SerializeField]
    private Transform pointAttach2;

    void Update()
    {
        rubber.SetPosition(0, pointAttach1.position);
        rubber.SetPosition(2, pointAttach2.position);
        rubber.SetPosition(1, midTrans.position);
    }
}
