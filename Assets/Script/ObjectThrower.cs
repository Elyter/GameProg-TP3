using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    public GameObject throwablePrefab;
    public float minThrowInterval = 2f;
    public float maxThrowInterval = 5f;
    public float minSpeed = 5f;
    public float maxSpeed = 10f;
    public float minThrowForce = 5f;
    public float maxThrowForce = 10f;

    private float throwTimer;

    void Start()
    {
        throwTimer = Random.Range(minThrowInterval, maxThrowInterval);
    }

    void Update()
    {
        throwTimer -= Time.deltaTime;

        if (throwTimer <= 0)
        {
            ThrowObject();
            throwTimer = Random.Range(minThrowInterval, maxThrowInterval);
        }
    }

    void ThrowObject()
    {
        GameObject throwableObject = Instantiate(throwablePrefab, GetRandomSpawnPosition(), Quaternion.identity);
        Rigidbody2D rb = throwableObject.GetComponent<Rigidbody2D>();

        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        float randomThrowForce = Random.Range(minThrowForce, maxThrowForce);

        // Objects are thrown from right to left
        Vector2 throwDirection = Vector2.left;

        // Apply initial force to make the object move diagonally
        rb.velocity = throwDirection * randomSpeed + new Vector2(-1, -1).normalized * randomThrowForce;
    }

    Vector2 GetRandomSpawnPosition()
    {
        float randomY = Random.Range(0f, 10f); // Adjust the Y range based on your scene

        // Objects are thrown from right to left, so they should appear on the right side
        float randomX = Camera.main.ViewportToWorldPoint(new Vector2(1.1f, Random.Range(0f, 1f))).x;

        return new Vector2(randomX, randomY);
    }
}
