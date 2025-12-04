using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class Player_Controller : MonoBehaviour
{

    //Player related Variables
    public float thrustForce = 1f;
    public Rigidbody2D player_rb2d;
    public GameObject explosionEffect;

    //UI Used/related Variables
    [Header("UI related")]
    public UIDocument uiDoc;
    private Button restartButton;
    private Label scoreLabel;
    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMultiplyer;

    
    [Header("SFX related")]
    public AudioClip asteroid_Collision;
    public AudioSource rocketMoving;
    private bool rocketActive;
    void Start()
    {
        scoreLabel = uiDoc.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDoc.rootVisualElement.Q<Button>("RestartButton");
        restartButton.clicked += ReloadScene;
        restartButton.visible = false;
    }

    void Update()
    {
        //Increments the score and changes the score label
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplyer);
        scoreLabel.text = ("Score: " + score);

        if (Mouse.current.leftButton.isPressed)
        {
            //Calculate mouse direction and moves the rocket
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 Direction = (mouse_pos - transform.position).normalized;
            transform.up = Direction;
            player_rb2d.AddForce(Direction * thrustForce * Time.deltaTime, ForceMode2D.Impulse);
        }

        //Plays the rocket thrusting sound when the player clicks down
        RocketSFX();


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //This is everthing that has to do with the player dying
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(asteroid_Collision,new Vector3());
        Instantiate(explosionEffect,transform.position, transform.rotation);
        restartButton.visible = true;
    }

    //Reloads the current scene(aka the ship game), this method is ment to be used in buttons
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Method for nowing how to handel moving SFX
    void RocketSFX()
    {
        //Checks when the player presses down and releases
        if (Mouse.current.leftButton.wasPressedThisFrame){
            rocketActive = true;
        } else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            rocketActive = false;
        }

        //Plays the rocket thrusting sound when rocketActive is true and stops when its false
        if (rocketActive && rocketMoving.isPlaying == false)
        {
            rocketMoving.Play();
        } else if (!rocketActive)
        {
            rocketMoving.Stop();
        }
    }
}
