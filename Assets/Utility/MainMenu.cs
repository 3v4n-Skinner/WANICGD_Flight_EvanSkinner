using System;
using UnityEngine;
using UnityEngine.SceneManagement;
/* Class: MainMenu
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/27/24
 * Last Modified: 12/2/2024 
 * 
 * Purpose: Main class for driving menu functions
 */
public class MainMenu : MonoBehaviour
{
    [Tooltip("Prefab for confirmation of destrutive action UI")]
    public GameObject CODAPrefab;

    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1.0f; 
    }

    /// <summary>
    /// Creates a new confirmation of destructive action popup.
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="action">Action perfomed on confirm</param>
    public void MakeCODA(string message,  Action action)
    {
        ConformationOfDestructiveAction coda = Instantiate(CODAPrefab).GetComponent<ConformationOfDestructiveAction>();
        coda.Initilize(message, action);
    }

    /// <summary>
    /// Creates Coda with Quit action
    /// </summary>
    public void Quit()
    {
        MakeCODA("Are you sure you want to close the game?", () => Application.Quit());
    }
}
