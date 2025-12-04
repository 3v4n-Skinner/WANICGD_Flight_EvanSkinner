using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;


/* Class: PlatformControler
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/20/24
 * Last Modified: 12/4/24
 * 
 * Purpose: Controls movement of a 2D Platformer Character
 */

public class PlatformControler : MonoBehaviour
{

    [Tooltip("Ammount of knockback taken when dealt damage")]
    [SerializeField] float KnockBack;
    
    [Space]

    [Header("Motion - Horizontal")]
    [SerializeField] float Speed;
    [SerializeField] AudioPacket SFX_Move;
    [SerializeField] ParticleSystem VFX_Move;
    float xDir;

    [Space]

    [Header("Motion - Vertical")]
    [Tooltip("Force applied to character when jumping")]
    [SerializeField] float JumpForce; 
    [SerializeField] AudioPacket SFX_Jump;
    [SerializeField] AudioPacket SFX_Land;
    [SerializeField] ParticleSystem VFX_Jump;
    [SerializeField] ParticleSystem VFX_Land;
    public static Action<Vector3> OnLand;
    float yDir;

    GroundDetector gd;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //Gets component refrences
        rb = GetComponent<Rigidbody2D>();
        gd = GetComponentInChildren<GroundDetector>();

        
        //Loads move sound
        SoundManager.PlayClip(SFX_Move);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Motion
    /// <summary>
    /// Event handeler for jump input, called when player presses jump input
    /// </summary>
    /// <param name="input">Input action event from Input System, does not use type.</param>
    public void GetJump(InputAction.CallbackContext input)
    {
        if (input.performed && gd.Grounded)
        {
            yDir = JumpForce;
            VFX_Jump.Play();
            SFX_Jump.Play();
        }
    }

    
    /// <summary>
    /// Event handeler for horizontal movement. 
    /// </summary>
    /// <param name="input">InputAction event from InputSystem, expects a Vector2</param>
    public void GetMove(InputAction.CallbackContext input)
    {
        if (input.valueType != typeof(Vector2))
        {
            Debug.LogError("Move input is not of type Vector2", this);
            return;
        }

        //Stores horizontal direction for use in FixedUpdate
        xDir = input.ReadValue<Vector2>().x;
    }


    void FixedUpdate()
    {
        //Moves player in pressed direction
        rb.linearVelocity = new Vector2(xDir * Speed, rb.linearVelocity.y + yDir);

        //Toggles VFX and SFX
        if(rb.linearVelocity.x != 0 && gd.Grounded)
        {
            SoundManager.ToggleClip(SFX_Move,false);
            VFX_Move.Play();
        }
        else
        {
            VFX_Move.Stop();
            SoundManager.ToggleClip(SFX_Move, true);

        }

        yDir = 0;
    }
    #endregion


    /// <summary>
    /// Parses collision events for player. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Damage from hazards
        if (collision.gameObject.CompareTag("Hazard"))
        {
            throw new NotImplementedException("Damage not implemented. Damage logic should go here.");
            J_GameManager.GameOver();
        }
        //Landing on a new platform
        else if (collision.gameObject.CompareTag("Collectable"))
        {
            LandOnNew(collision);
        }

        if (collision.contacts[0].normal.y > 0.9)
        {
            VFX_Land.Play();
            SFX_Land.Play();
        }
    }

    /// <summary>
    /// Handles landing on a platform for the first time.
    /// </summary>
    /// <param name="collision">Collision event for landed platform</param>
    private void LandOnNew(Collision2D collision)
    {
        //Updates score
        J_GameManager.Score += 1;
        UI_Score.Instance.AddScore(1);

        //Spawns new platform
        J_PlatformSpawner.SpawnPlatform();

        //Removes Collectable tag from object to prevent secondary collisions
        collision.gameObject.tag = "Untagged";
        OnLand?.Invoke(transform.position);
    }
}

