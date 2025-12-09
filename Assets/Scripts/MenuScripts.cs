using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    public GameObject credits;
    public GameObject mainMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Loads a scene and makes sure the time scale is 1
    public void loadAScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
        
    }

    //This unhides the credits and hides the menu
    public void showCredits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }
    //This hides the credits and unhides the menu
    public void loadMenu()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }
    //Ends the game
    public void quitApp()
    {
        Application.Quit();
    }
}
