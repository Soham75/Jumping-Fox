using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Main_Menu : MonoBehaviour
{
    [SerializeField] private AudioSource Sound_Click;
    public AudioMixer AUM;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;
        for(int i =0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void Play()
    {
        Sound_Click.Play();
        SceneManager.LoadScene("Level_1");
    }
    public void Options()
    {
        Sound_Click.Play();
        SceneManager.LoadScene("Settings_Menu");
    }
    public void Quit()
    {
        Sound_Click.Play();
        Application.Quit();
    }
    public void Back()
    {
        Sound_Click.Play();
        SceneManager.LoadScene("Main_Menu");
    }

    public void SetVolume(float Volume)
    {
        AUM.SetFloat("Master_Volume", Volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int ResIndex)
    {
        Resolution resolution = resolutions[ResIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }



}
