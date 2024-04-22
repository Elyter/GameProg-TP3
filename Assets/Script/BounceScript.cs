using UnityEngine;

public class BounceScript : MonoBehaviour
{
    public int maxBounces = 3;

    private int bouncesCount = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a collider tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            Bounce();
        }
    }

    void Bounce()
    {
        if (bouncesCount < maxBounces)
        {
            // Reflect the velocity upon collision with the ground
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);

            bouncesCount++;
        }

    }
}
