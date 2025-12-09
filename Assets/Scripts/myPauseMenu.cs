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
        if (Input.GetKeyDown(KeyCode.Escape) && pressed)
        {
            pauseCanvas.SetActive(!pauseCanvas.active);
            pressed = false;
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            } else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        } else if (Input.GetKeyUp(KeyCode.Escape))
        {
            pressed = true;
        }
    }
}
