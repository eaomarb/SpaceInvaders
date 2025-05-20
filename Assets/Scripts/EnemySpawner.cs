using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 0.6f;

    private float timer;
    private bool spawning = true;
    private Camera cam;
    private float xMin, xMax, ySpawnMax, ySpawnMin;

    void Start()
    {
        cam = Camera.main;

        // Calcula los límites en el eje X para generar enemigos dentro del rango visible de la cámara
        float halfWidth = cam.orthographicSize * cam.aspect;
        xMin = cam.transform.position.x - halfWidth;
        xMax = cam.transform.position.x + halfWidth;

        // Configura los límites Y para el spawn (por encima de la cámara)
        ySpawnMax = cam.transform.position.y + cam.orthographicSize + 1f; // Un poco por encima de la cámara
        ySpawnMin = cam.transform.position.y + cam.orthographicSize + 0.5f; // Un poco por encima de la cámara
    }

    void Update()
    {
        if (!spawning) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            float xPos = Random.Range(xMin, xMax);  // Posición X aleatoria dentro de los límites visibles
            float yPos = ySpawnMax;                  // Genera enemigos justo por encima de la cámara
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0); // Genera la posición
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Crea al enemigo
            Debug.Log("Spawning enemy at: " + spawnPosition);  // Verifica la posición de spawn
            timer = 0f; // Reinicia el temporizador
        }
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
