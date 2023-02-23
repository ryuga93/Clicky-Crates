using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> targets;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] Slider volumeSlider;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject pausePanel;
    
    DontDestroy audioSource;
    bool isGameActive = false;
    bool isGamePaused = false;
    public bool IsGameActive
    {
        get => isGameActive;
        set => isGameActive = value;
    }
    public bool IsGamePaused => isGamePaused;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<DontDestroy>();
        volumeSlider.value = audioSource.Volume;
    }

    // Update is called once per frame
    void Update()
    {
        volumeSlider.onValueChanged.AddListener((volume) => audioSource.ChangeBgmVolume(volume));

        if(Input.GetKeyDown(KeyCode.P))
        {
            if (isGameActive && !isGamePaused)
            {
                pausePanel.SetActive(true);
                isGamePaused = true;
                Time.timeScale = 0f;
            }
            else if(isGameActive && isGamePaused)
            {
                pausePanel.SetActive(false);
                isGamePaused = false;
                Time.timeScale = 1f;
            }
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnDelay);
            int targetIndex = Random.Range(0, targets.Count);
            Instantiate(targets[targetIndex]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame(int difficulty)
    {
        spawnDelay /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        DisplayLives();

        titleScreen.SetActive(false);
    }

    public void ReduceLives()
    {
        if (playerLives > 0)
        {
            playerLives--;
            DisplayLives();
        }

        if (playerLives == 0)
        {
            GameOver();
        }
    }

    void DisplayLives()
    {
        livesText.text = "Lives: " + playerLives;
    }
}
