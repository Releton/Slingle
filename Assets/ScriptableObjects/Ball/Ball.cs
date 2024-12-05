using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Ball", menuName = "Ball")]
public class Ball : ScriptableObject
{
    public new string name;
    public GameObject objGraphics;
    public float maxSpeed;
    public float windSpeed;
    public Sprite icon;
}
