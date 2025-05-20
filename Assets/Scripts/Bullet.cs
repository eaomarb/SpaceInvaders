using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y > Camera.main.orthographicSize + 1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}