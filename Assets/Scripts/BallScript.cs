using UnityEngine;
using UnityEngine.UIElements;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D ball_rb2d;
    public float range_of_power = 25f;
    public float scale_Power = 20f;
    private float powerX = 0f;
    private float powerY = 0f;
    public AudioClip hit_Sound;
    public UIDocument ballUI;
    private Label bounces;
    private static int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball_rb2d = transform.GetComponent<Rigidbody2D>();
        powerX = Random.Range(1,range_of_power);
        powerY = Random.Range(1, range_of_power);
        ball_rb2d.AddForceX(powerX * scale_Power);
        ball_rb2d.AddForceY(powerY * scale_Power);
        bounces = ballUI.rootVisualElement.Q<Label>();
    }

    // Update is called once per frame
    void Update()
    {
        bounces.text = "Ball Bounces: " + score;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(hit_Sound,new Vector3());
        if (collision.transform.name.Contains("Ball"))
        {
            score += 1;
        }
        
    }
}
