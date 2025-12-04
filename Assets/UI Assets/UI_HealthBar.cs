using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] RectTransform HealthArea;
    [SerializeField] GameObject HealthPrefab;


    [SerializeField] Color OnColor;
    [SerializeField] Color OffColor;

    int health;
    int maxHealth = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SetMaxHealth(int value)
    {
        //Adds more health
        if(value > maxHealth)
        {
            for(int i = 0; i<value-maxHealth; i++)
            {
                SpawnHealth();
            }
        }
        else if(value<maxHealth)
        {
           for (int i = maxHealth-1;i> value; i--)
            {
                Destroy(HealthArea.GetChild(i));
            }
        }
        maxHealth = value;

    }
    void SpawnHealth()
    {
        Instantiate(HealthPrefab, HealthArea);//.transform.SetParent(HealthArea);
        
    }
    public void SetHealth(int value) 
    {
        if(value > maxHealth)
        {
            throw new WarningException("Setting health to value over maximum");
        }
        
        //Heal Code
        if (health < value)
        {
            for (int i = health; i < value; i++)
            {
                HealthArea.GetChild(i).GetComponent<Image>().color = OnColor;
            }
        }
        else
        {
            for (int i = health-1; i >= value; i--)
            {
                HealthArea.GetChild(i).GetComponent<Image>().color = OffColor;

            }
        }
        health = value;
    }
}
