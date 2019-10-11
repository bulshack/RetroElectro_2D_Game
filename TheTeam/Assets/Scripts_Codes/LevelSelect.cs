using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    public int Level;
    public Image image;
    private string levelString;
	// Use this for initialization
	 void Start ()
    {
           if(ButtonSettings.releasedLevelStatic >= Level)
        {
            LevelUnlocked();
        }
        else
        {
            LevelLocked();
        }
	}
	
	// Update is called once per frame
	public void Selectlevel (string worldLevel)
    {
        if (GameObject.Find("Button_Competitive").GetComponent<GameModeSelection>().competitive == true)
        {
            SceneManager.LoadScene("Competitive" + worldLevel);
        }
        else
            SceneManager.LoadScene(worldLevel);	
	}


    void LevelLocked()
    {
        GetComponent<Button>().interactable = false;
        image.enabled = true;
    }

    void LevelUnlocked()
    {
        GetComponent<Button>().interactable = true;
        image.enabled = false;
    }
   // void CheckLockedLevels()
   //{
   //    //loop through the levels 
   //    for (int j = 1; j < LockLevel.levels; j++)
   //    {
   //        levelIndex = (j + 1);
   //       if ((PlayerPrefs.GetInt("level" + levelIndex.ToString())) == 1)
   //        {
   //            Debug.Log(levelIndex);
   //            Debug.Log(j);
   //            //finds lockedlevel object and set it to false so player can play next level
   //            GameObject.Find("LockedLevel" + (levelIndex)).SetActive(false);
   //            Debug.Log("Unlocked");
   //        }
   //    }
   //}
}
