using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTaker : MonoBehaviour
{
    public TMP_Text scoreField;
    // Update is called once per frame
    private void Start()
    {
        scoreField = this.GetComponent<TMP_Text>();
    }
    void Update()
    {
        scoreField.text = PointManager.score.ToString();
    }
}
