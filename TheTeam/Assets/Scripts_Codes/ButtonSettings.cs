using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSettings : MonoBehaviour {

    public static int releasedLevelStatic = 1;
    public int releasedLevel;
    public string nextLevel;
	// Use this for initialization
	void Awake ()
    {
		if(PlayerPrefs.HasKey("Level"))
        {
            releasedLevelStatic = PlayerPrefs.GetInt("Level", releasedLevelStatic);
        }
	}
	
	// Update is called once per frame
	public void ButtonNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
        if(releasedLevelStatic <= releasedLevel)
        {
            releasedLevelStatic = releasedLevel;
            PlayerPrefs.SetInt("Level", releasedLevelStatic);
        }	
	}

   // public void NewGame()
   // {
   //     PlayerPrefs.DeleteAll();
   //     GameObject.Find("LockedLevel2").SetActive(true);
   // }
}
