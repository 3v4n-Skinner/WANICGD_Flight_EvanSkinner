using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadAScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void quitApp()
    {
        Application.Quit();
    }
}
