using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    public GameObject[] Skulls;
    public Transform[] SpawnPoint;
    private void Start()
    {
        
    }
    private int wave;
    private float time;
    private void Update()
    {
        time += Time.deltaTime;

        if(time >= 6f)
        {
            SpawnSkull(0,12f,40f,1);
            time = 0;
        }
    }
    


    public void SpawnSkull(int quality, float speed, float jumping, int numSkulls)
    {
        for(int i = 0; i < numSkulls; i++)
        {
            int spPoints = Random.Range(0, SpawnPoint.Length);
            GameObject go = Instantiate(Skulls[quality], SpawnPoint[spPoints].position, Quaternion.Euler(0, -90, 0));
            go.GetComponent<Skull>().SetJumping(jumping);
            go.GetComponent<Skull>().SetSpeed(speed);
        }
        wave++;
    }
    

}
