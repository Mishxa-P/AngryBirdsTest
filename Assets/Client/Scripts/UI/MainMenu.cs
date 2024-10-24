using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 30;

        if (AudioManager.Singleton != null)
        {
            if (!AudioManager.Singleton.IsPlaying("Main_Music"))
            {
                AudioManager.Singleton.Play("Main_Music");
            }
        }
        else
        {
            Debug.Log("Audio manager is not created!");
        }
    }
    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
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
    public void CloseGame()
    {
        Application.Quit();
    }
}
