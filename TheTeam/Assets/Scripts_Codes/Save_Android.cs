using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Android : MonoBehaviour {


    // call this where ever u want to save the coin variable
    //public int coinAmount;
    //PlayerPrefs.SetInt("CoinNumber", coinAmount);

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey("Map_Array"))  // check if we already save It before
        {

            //coinAmount = PlayerPrefs.GetInt("CoinNumber");

        }

        else
        {

            //coinAmount = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

//DeleteAll      Removes all keys and values from the preferences.Use with caution.
//DeleteKey      Removes key and its corresponding value from the preferences.
//GetFloat       Returns the value corresponding to key in the preference file if it exists.
//GetInt         Returns the value corresponding to key in the preference file if it exists.
//GetString      Returns the value corresponding to key in the preference file if it exists.
//HasKey         Returns true if key exists in the preferences.
//Save           Writes all modified preferences to disk.
//SetFloat       Sets the value of the preference identified by key.
//SetInt         Sets the value of the preference identified by key.
//SetString      Sets the value of the preference identified by key.