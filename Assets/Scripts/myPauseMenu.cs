using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    private bool pressed =true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Checks if the right key was hit to pause the game and uses a bool to prevent it from being activated multiple times
        if (Input.GetKeyDown(KeyCode.Escape) && pressed)
        {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
            pressed = false;

            //Stops the physics of the game from running
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            } else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }//letting go of the escape key makes it so your able to activate or deactivate the pause menu again 
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            pressed = true;
        }
    }

    //Special resume from the pause menu
    public void resumeButton()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);

        //Stops the physics of the game from running
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }
}
