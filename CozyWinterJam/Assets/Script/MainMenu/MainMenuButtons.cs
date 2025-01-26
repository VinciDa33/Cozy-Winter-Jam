using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Transform Title;
    [SerializeField] private Transform StartButton;
    [SerializeField] private Transform OptionsButton;
    [SerializeField] private Transform ExitButton;
    [SerializeField] private GameObject SettingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayMusic("little");
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void Options()
    {
        SettingsMenu.SetActive(true);
    }
    
    public void Back()
    {
        SettingsMenu.SetActive(false);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}