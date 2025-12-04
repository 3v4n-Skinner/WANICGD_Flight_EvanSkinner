using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class: GroundDecector
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/25/24
 * Last Modified: 12/4/2024 
 * 
 * Purpose: Detects when player is on ground
 */

public class GroundDetector : MonoBehaviour
{
    /// <summary>
    /// Count of ground colliders character is currently colliding with
    /// </summary>
    int groundCount;
    public ParticleSystem VFX_Land;
    /// <summary>
    /// Returns true if character is on ground
    /// </summary>
    public bool Grounded => groundCount > 0;


    /// <summary>
    /// Collision event
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        groundCount++;
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {
        groundCount--;
    }
}
