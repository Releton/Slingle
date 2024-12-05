using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BallScript : MonoBehaviour
{
    public Ball type;
    public static new string name;
    public GameObject objGraphics;
    public float maxSpeed;
    public float windSpeed;
    private bool isChildCreated = false;
    public bool isDestroyed = false;
    public static bool isActivated = false;
    private GameManager GM;
    public Sprite icon;
    private void Start()
    {
        name = type.name;
        objGraphics = type.objGraphics;
        maxSpeed = type.maxSpeed;
        windSpeed = type.windSpeed;
        icon = type.icon;
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    private void Update()
    {
        if(objGraphics != null && !isChildCreated)
        {
            GameObject obj = Instantiate(objGraphics, this.transform);
            obj.transform.localPosition = Vector3.zero;
            isChildCreated = true;
        }
        if(this.transform.position.y < 0)
        {
            Debug.Log("Called ball finished Positiion");
            BallFinished();
        }
    }

    public void activateBall()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        isActivated = true;
        
    }

    public void deActivate()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        isActivated = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!isActivated) return;

        if(collision.gameObject.tag == "Obstacle")
        {
            PointManager.score += 1;
            Destroy(collision.gameObject);
        }
        Debug.Log("Called ball FInish by collision" + collision.collider.gameObject);
        BallFinished();

    }


    public void BallFinished()
    {
        isActivated = false;
        isDestroyed = true;
        Destroy(this.gameObject);
        GM.DeleteFromCurrent();
        Debug.Log("Deleted");
    }

}
