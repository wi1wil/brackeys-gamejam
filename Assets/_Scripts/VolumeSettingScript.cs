using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VolumeSettingScript : MonoBehaviour
{
    [Header("Audio Mixer + Volume Settings")]
    [SerializeField] AudioMixer mainMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public Button settingsButton;
    public Button closeButton;

    public GameObject menuPanel;

    void OnEnable()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        settingsButton.onClick.AddListener(openMenu);
        closeButton.onClick.AddListener(openMenu);
        ApplyAllVolumes();
    }

    void Update()
    {
        ApplyAllVolumes();
    }

    public void openMenu()
    { 
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(true);
        }
    }


    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            openMenu();
        }
    }

    public void SetMasterVolume()
    {
        float masterVolume = Mathf.Clamp(masterSlider.value, 0.0001f, 1f);
        mainMixer.SetFloat("master", Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
    }

    public void SetMusicVolume()
    {
        float musicVolume = Mathf.Clamp(musicSlider.value, 0.0001f, 1f);
        mainMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = Mathf.Clamp(sfxSlider.value, 0.0001f, 1f);
        mainMixer.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    private void ApplyAllVolumes()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}