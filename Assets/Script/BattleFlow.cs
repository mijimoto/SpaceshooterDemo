using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlow : MonoBehaviour
{
    [Header("UI / Audio")]
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject bgMusic;

    [Header("Player")]
    public PlayerHealth playerHealth;

    [Header("Survival Win Condition")]
    [Tooltip("Seconds the player must survive to win the level.")]
    public float surviveDuration = 30f;

    private float _elapsed = 0f;
    private bool _ended = false;

    private void Start()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);

        _elapsed = 0f;
        _ended = false;

        if (playerHealth != null)
            playerHealth.onDead += OnGameOver;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.onDead -= OnGameOver;
    }

    private void Update()
    {
        if (_ended) return; 
        // If the player is dead don't continue counting
        if (playerHealth == null || playerHealth.healthPoint <= 0)
            return;

        _elapsed += Time.deltaTime;

 
        if (_elapsed >= surviveDuration)
        {
            OnGameWin();
        }
    }

    private void OnGameOver()
    {
        if (_ended) return;
        _ended = true;

        gameOverUI.SetActive(true);
        if (bgMusic != null) bgMusic.SetActive(false);
        
    }

    private void OnGameWin()
    {
        if (_ended) return;
        _ended = true;

        gameWinUI.SetActive(true);
        if (bgMusic != null) bgMusic.SetActive(false);

        // Optionally hide player or freeze them
        if (playerHealth != null && playerHealth.gameObject != null)
            playerHealth.gameObject.SetActive(false);

        // Optional: stop enemy spawns or other mechanics here
    }

    public void ReturnToMainMenu() => SceneManager.LoadScene("MainMenu");
}
