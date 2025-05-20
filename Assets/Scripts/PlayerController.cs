using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;

    float xMin, xMax;

    void Start()
    {
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect;
        xMin = cam.transform.position.x - halfWidth + 0.5f;
        xMax = cam.transform.position.x + halfWidth - 0.5f;
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        Vector3 p = transform.position + Vector3.right * h * speed * Time.deltaTime;
        p.x = Mathf.Clamp(p.x, xMin, xMax);
        transform.position = p;
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            GameManager.instance.PlayerHit();
        }
    }
}