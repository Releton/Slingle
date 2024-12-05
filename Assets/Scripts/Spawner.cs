using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : MonoBehaviour
{
    public GameObject BallPrefab;
    public Ball[] Balls;
    public static GameObject[] created;
    void Awake()
    {
        created[0] = Instantiate(BallPrefab);
        created[0].GetComponent<BallScript>().type = Balls[0];
        Debug.Log(created[0].GetComponent<BallScript>().type);
        created[1] = Instantiate(BallPrefab);
        created[1].GetComponent<BallScript>().type = Balls[1];
        created[2] = Instantiate(BallPrefab);
        created[2].GetComponent<BallScript>().type = Balls[2];
        created[3] = Instantiate(BallPrefab);
        created[3].GetComponent<BallScript>().type = Balls[3];
        created[4] = Instantiate(BallPrefab);
        created[4].GetComponent<BallScript>().type = Balls[4];
    }

}
