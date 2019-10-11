using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSelection : MonoBehaviour {

    // Use this for initialization
    public bool educational, master, competitive;
    LockLevel select;
    void Start()
    {
        select = GetComponent<LockLevel>();
    }
    public void EducationalMode()
    {
        gameObject.GetComponent<Image>().color = Color.red;
        GameObject.Find("Button_Competitive").gameObject.GetComponent<Image>().color = Color.white;
        GameObject.Find("Button_Competitive").GetComponent<GameModeSelection>().competitive = false;
        educational = true;
        PlayerPrefs.SetString("GameMode", "Educational");
    }

    public void CompetitiveMode()
    {
        gameObject.GetComponent<Image>().color = Color.red;
        GameObject.Find("Button_Educational").gameObject.GetComponent<Image>().color = Color.white;
        GameObject.Find("Button_Educational").gameObject.GetComponent<GameModeSelection>().educational = false;
        competitive = true;
        PlayerPrefs.SetString("GameMode", "Competitive");
        //select.LockLevels();
    }
}
