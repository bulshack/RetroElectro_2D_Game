using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockLevel : MonoBehaviour {

    // Use this for initialization
    public static int levels = 10;
    private int levelIndex;
    void Start ()
    {
        PlayerPrefs.DeleteAll(); //erase data on sa
        LockLevels();
	}
	
	//function to lock the levels
	public void LockLevels()
    {
        //loop through all levels
        for (int j = 0; j < levels; j++)
        {
            levelIndex = (j + 1);
            //create a PlayerPrefs of that particular level and set it to 0
            if (!PlayerPrefs.HasKey("level" + levelIndex.ToString()))
                PlayerPrefs.SetInt("level" + levelIndex.ToString(), 0);
        }

    }
}
