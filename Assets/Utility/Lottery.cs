using System.Collections.Generic;
using UnityEngine;
/* Class: Lottery
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/27/2024
 * Last Modified: 11/27/2024
 * 
 * Purpose: Generic Struct for a lottery random system
 */

[System.Serializable]
public class Lottery<T>
{
    public List<Entry<T>> Entries;
    int totalLots;

    /// <summary>
    /// Draws a random entry from the lottery.
    /// </summary>
    /// <returns></returns>
    public T Draw()
    {
        //Calculates total lots if not already set
        if(totalLots == 0)
        {
            GenPool();
        }

        //Finds item at random value based on weights
        int value = Random.Range(0, totalLots+1);
        foreach (Entry<T> e in Entries)
        {
            value -= e.Odds;
            if (value <= 0)
            {
                return e.Item;
            }
        }

        throw new System.Exception("Drew value out of bounds of lottery");
    }

    /// <summary>
    /// Calculates the total odds within the lottery entries
    /// </summary>
    void GenPool()
    {
        foreach (Entry<T> e in Entries)
        {
            totalLots += e.Odds;
        }
    }

}

[System.Serializable]
public struct Entry<T>
{
    [Tooltip("Chance item will be drawn. Higher is more likely")]
    public int Odds;
    public T Item;
}
