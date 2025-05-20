using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverVisual;
    public int lives = 3;

    private int score = 0;
    private bool isGameOver = false;
    private EnemySpawner spawner;
    public static GameManager instance;

    [Header("Vidas")]
    public GameObject heartPrefab;
    public Transform livesContainer;

    [Header("Puntuación")]
    public Transform scoreContainer;
    public GameObject digitPrefab;         // Prefab vacío con Image
    public Sprite[] scoreDigits;           // score0, score1, ..., score9

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        spawner = FindFirstObjectByType<EnemySpawner>();
        UpdateUI();
        gameOverVisual.SetActive(false);
    }

    public void PlayerHit()
    {
        if (isGameOver) return;

        lives--;
        UpdateUI();

        if (lives <= 0) GameOver();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        // Corazones
        foreach (Transform child in livesContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < lives; i++)
            Instantiate(heartPrefab, livesContainer);

        // Puntuación como sprites
        foreach (Transform child in scoreContainer)
            Destroy(child.gameObject);

        string scoreStr = score.ToString();

        foreach (char c in scoreStr)
        {
            int digit = c - '0';
            GameObject digitGO = Instantiate(digitPrefab, scoreContainer);
            Image img = digitGO.GetComponent<Image>();
            img.sprite = scoreDigits[digit];
        }
    }

    void GameOver()
    {
        isGameOver = true;
        spawner.StopSpawning();
        gameOverVisual.SetActive(true);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.SetActive(false);
    }
}