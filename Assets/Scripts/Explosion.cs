using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float TimeToDestroy = 1f;

    private void Start()
    {
        Destroy(gameObject, TimeToDestroy);
    }
}
