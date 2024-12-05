using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance= this;
    }

    public Sprite SpikeBall;
    public Sprite StrongBall;
    public Sprite NoWindBall;
    public Sprite PlasticBall;

    public Transform pfItemWorld;
    public Transform spawnPoint;

}
