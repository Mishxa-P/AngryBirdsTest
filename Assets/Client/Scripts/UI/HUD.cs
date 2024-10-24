using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _resultScoreText;
    [SerializeField] private CanvasGroup _staticHUD;
    [SerializeField] private CanvasGroup _activeHUD;
    [SerializeField] private CanvasGroup _ResultScreen;

    private void OnEnable()
    {
        LevelEventManager.OnScoreUpdated.AddListener(UpdateScore);
        LevelEventManager.OnLevelCompleted.AddListener(ShowResults);
    }
    public void UpdateScore(int newScore)
    {
        Debug.Log("Score = " + newScore);
        _scoreText.text = newScore.ToString();
        _resultScoreText.text = "Score: " + newScore.ToString();
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            LoadMainMenu();
        } 
    }
    public void PlayButtonClickSound()
    {
        if (AudioManager.Singleton != null)
        {
            AudioManager.Singleton.Play("Button_Click");
        }
        else
        {
            Debug.Log("Audio manager is not created!");
        }
    }
    private void ShowResults()
    {
        StartCoroutine(ShowResultsCoroutine());
    }
    private IEnumerator ShowResultsCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        if (AudioManager.Singleton != null)
        {
            AudioManager.Singleton.Play("Level_Completed");
        }
        else
        {
            Debug.Log("Audio manager is not created!");
        }
        _staticHUD.alpha = 0.0f;
        _staticHUD.blocksRaycasts = false;
        _activeHUD.alpha = 0.0f;
        _ResultScreen.alpha = 1.0f;
        _ResultScreen.blocksRaycasts = true;
    }
}
