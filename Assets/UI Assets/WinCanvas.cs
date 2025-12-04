using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinCanvas : MainMenu
{
    [SerializeField] string OldRecord_Prefix;
    [SerializeField] TextMeshProUGUI OldRecord_Display;

    [Space]
    [SerializeField] string NewRecord_Prefix;
    [SerializeField] TextMeshProUGUI NewRecord_Display;


    public void SetText(int oldRecord,int newRecord)
    {
        OldRecord_Display.text = OldRecord_Prefix+ oldRecord;
        NewRecord_Display.text= NewRecord_Prefix+ newRecord;

    }
    // Start is called before the first frame update

    public void Menu()
    {
        MakeCODA("Are you sure you want to return to the main menu?", () => LoadScene("MainMenu"));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
