using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public float thrustForce;
    public Rigidbody2D player_rb2d;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            //Calculate mouse direction
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 Direction = (mouse_pos - transform.position).normalized;
            transform.up = Direction;
            player_rb2d.AddForce(Direction * thrustForce);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
