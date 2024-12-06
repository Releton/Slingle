using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jumpscaring : MonoBehaviour
{
    public float duration;
    public AudioSource audioSource;
    public float magnitude;
    public void JumpScare()
    {
        gameObject.SetActive(true);
        audioSource.Play();
        StartCoroutine(DisableImg(duration));
    }
    IEnumerator DisableImg(float duration)
    {
        float elapsed = 0.0f;
        Vector3 originalPosition = transform.localPosition;
        while(elapsed < duration )
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x,y,originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
        audioSource.Stop();
        gameObject.SetActive(false);
    }
}
