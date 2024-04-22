using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    void Update()
    {
        if (IsOutsideScreen())
        {
            Destroy(gameObject);
        }
    }

    bool IsOutsideScreen()
    {
        float screenEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - 1f;
        return transform.position.x < screenEdge;
    }
}
