using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _globalVolume;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundsVolume;
    private void OnEnable()
    {
        _globalVolume.onValueChanged.AddListener(SetGlobalVolume);
        _musicVolume.onValueChanged.AddListener(SetMusicVolume);
        _soundsVolume.onValueChanged.AddListener(SetSoundsVolume);
    }
    private void Start()
    {
        _globalVolume.value = PlayerPrefs.GetFloat("GlobalVolume", 0.5f);
        _musicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _soundsVolume.value = PlayerPrefs.GetFloat("SoundsVolume", 0.5f);
    }
    private void SetGlobalVolume(float value)
    {
        PlayerPrefs.SetFloat("GlobalVolume", value);
        if (AudioManager.Singleton != null)
        {
            AudioManager.Singleton.UpdateVolumeSettings();
        }
        else
        {
            Debug.Log("Audio manager is not created!");
        } 
    }
    private void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        if (AudioManager.Singleton != null)
        {
            AudioManager.Singleton.UpdateVolumeSettings();
        }
        else
        {
            Debug.Log("Audio manager is not created!");
        }
    }
    private void SetSoundsVolume(float value)
    {
        PlayerPrefs.SetFloat("SoundsVolume", value);
        if (AudioManager.Singleton != null)
        {
            AudioManager.Singleton.UpdateVolumeSettings();
        }
        else
        {
            Debug.Log("Audio manager is not created!");
        }
    }
    public void LoadUrlByUniWebView(string url)
    {
        UniWebView webView;
        var webViewGameObject = new GameObject("UniWebView");
        webView = webViewGameObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        webView.Load(url); 
        webView.Show();
        webView.OnShouldClose += (view) => 
        {
            webView = null;
            return true;
        };
    }
}
