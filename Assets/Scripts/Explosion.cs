using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float TimeToDestroy = 2f;

    private void Start()
    {
        Destroy(gameObject, TimeToDestroy);
    }
}
