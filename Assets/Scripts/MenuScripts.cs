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
    public void showCredits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void loadMenu()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }
    //Ends the app
    public void quitApp()
    {
        Application.Quit();
    }
}
