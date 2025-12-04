using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class: J_PlatformSpawner
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/26/24
 * Last Modified: 
 * 
 * Purpose: Abstract parent class for characters. Controls health.
 */

public abstract class Character : MonoBehaviour
{

    [Header("Health")]

    [SerializeField] protected int Health;
    [SerializeField] protected int MaxHealth;

    public int GetHealth => Health;
    public int GetMaxHealth => MaxHealth;

    protected float ITime;
    float ITick;

    public void GiveDamage(int damage, bool ignoreIvn =false)
    {
        if (ITick <= 0 || ignoreIvn)
        {
            ITick = ITime;
            if (Health - damage <= 0)
            {
                Death();
                return;
            }
            else
            {
                Health-= damage;
            }
            OnHealtChange();
        }
    }

    /// <summary>
    /// Abstract death function for all characters.
    /// Called when damage is taken at or bellow 0.
    /// </summary>
    public abstract void Death();
    public virtual void OnHealtChange()
    {

    }

    protected virtual void Update()
    {
        ITick -= Time.deltaTime; 
    }


}
