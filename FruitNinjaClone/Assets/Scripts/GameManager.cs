using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{   
    [Header("Score Elements")]
    public int score;
    public int highScore;
    public Text scoreText;
    public Text highScoreText;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public Text gameOverPanelScoreText;
    public Text gameOverPanelHighScoreText;

    [Header("Sounds")]
    public AudioClip[] sliceSounds;
    private AudioSource audioSource;
    private void Awake()
    {
        Advertisement.Initialize("3774549", true);
        audioSource = GetComponent<AudioSource>();
        gameOverPanel.SetActive(false);
        GetHighScore();
    }
    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Best: " + score.ToString(); 
        }
    }

    public void OnBombHit()
    {
        Advertisement.Show();
        Time.timeScale = 0; //reduce la velocidad del juego, al ingresar un 0 lo detenemos por completo
        gameOverPanelScoreText.text = "Score: " + score.ToString();
        GetHighScore();
        gameOverPanelHighScoreText.text = "Best: " + highScore.ToString();
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(false);
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(g);
        }
        Time.timeScale = 1;
    }
    public void PlayRandomSlicedSound()
    {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }
    private void GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Best: " + highScore;
    }
}
