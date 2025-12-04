using UnityEngine;
using UnityEngine.UIElements;

public class BallScript : MonoBehaviour
{
    //Players rigidbody
    private Rigidbody2D ball_rb2d;

    [Header("Velocity related below")]
    public float range_of_power = 25f;
    public float scale_Power = 20f;
    private float powerX = 0f;
    private float powerY = 0f;

    [Header("SFX related below")]
    public AudioClip hit_Sound;

    [Header("UI related below")]
    public UIDocument ballUI;
    private Label bounces;
    private static int score = 0;
    

    void Start()
    {
        //Gets the balls rigidbody in an ineffective way and sets the balls velocity to a random speed
        ball_rb2d = transform.GetComponent<Rigidbody2D>();
        powerX = Random.Range(1,range_of_power);
        powerY = Random.Range(1, range_of_power);
        ball_rb2d.AddForceX(powerX * scale_Power);
        ball_rb2d.AddForceY(powerY * scale_Power);

        //Gets the label from the UI Doc
        bounces = ballUI.rootVisualElement.Q<Label>();
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the score UI
        bounces.text = "Ball Bounces: " + score;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Plays a sound effect when balls collide with things, and Only adds score when the balls collide
        AudioSource.PlayClipAtPoint(hit_Sound,new Vector3());
        if (collision.transform.name.Contains("Ball"))
        {
            score += 1;
        }
        
    }
}
