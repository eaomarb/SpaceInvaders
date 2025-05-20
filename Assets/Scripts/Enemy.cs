using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    bool hasCollided = false;
    Collider2D col2d;

    void Start()
    {
        col2d = GetComponent<Collider2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided) return;

        // marcamos colisión y desactivamos el collider para evitar reentradas
        hasCollided = true;
        col2d.enabled = false;

        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            GameManager.instance.AddScore(1);
        }
    }
}