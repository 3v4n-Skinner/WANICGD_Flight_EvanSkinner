using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* Class: J_EnemyControler
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/26/24
 * Last Modified: 
 * 
 * Purpose: Handles motion of a basic enemy that moves back and forth on a platform
 */

public class J_EnemyControler : MonoBehaviour
{
    [Tooltip("Amount of unity units character moves each second")]
    [SerializeField] float Speed;
    
    //Direction enemy is currently moving
    float dir = 1;

    //Hit event for look vector
    RaycastHit2D look;


    //Called every physics update
    private void FixedUpdate()
    {
        //Casts a ray forward to turn around on edge of platform.
        look = Physics2D.Raycast(transform.position, (dir * transform.right) - transform.up, 1);

        //Turns around if look hits nothing (air)
        if (!look)
        {
            dir *= -1;
        }
        //Error logging if enemy layers are setup incorectly
        else if (look.collider == this.GetComponent<Collider2D>()) {
            Debug.LogWarning($"Raycast for enemy {gameObject.name} is colliding with self", this);
        }

        //Translates enemy in direction of movement.
        transform.position +=  Vector3.right* Speed *dir* Time.deltaTime;
    }

    //Draws look ray
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, (dir * transform.right) - transform.up);
    }
}
