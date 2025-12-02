using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class Player_Controller : MonoBehaviour
{
    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMultiplyer;
    public float thrustForce = 1f;
    public Rigidbody2D player_rb2d;
    public UIDocument uiDoc;
    private Button restartButton;
    private Label scoreLabel;
    public GameObject explosionEffect;
    public AudioClip asteroid_Collision;
    public AudioSource rocketMoving;
    void Start()
    {
        scoreLabel = uiDoc.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDoc.rootVisualElement.Q<Button>("RestartButton");
        restartButton.clicked += ReloadScene;
        restartButton.visible = false;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplyer);

        scoreLabel.text = ("Score: " + score);

        if (Mouse.current.leftButton.isPressed)
        {
            //Calculate mouse direction
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 Direction = (mouse_pos - transform.position).normalized;
            transform.up = Direction;
            player_rb2d.AddForce(Direction * thrustForce * Time.deltaTime,ForceMode2D.Impulse);
            
        }
        if(Input.GetMouseButtonDown(0))
        {
            rocketMoving.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            rocketMoving.Stop();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(asteroid_Collision,new Vector3());
        Instantiate(explosionEffect,transform.position, transform.rotation);
        restartButton.visible = true;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
