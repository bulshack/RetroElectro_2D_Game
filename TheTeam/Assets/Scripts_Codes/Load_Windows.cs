using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Windows : MonoBehaviour
{

    #region Members
    private GameObject slotArray;
    #endregion

    #region Supporting Functions
    public void LoadSlotValues()
    {
        int saved = PlayerPrefs.GetInt("LevelSaved");
        SceneManager.LoadScene("Level_" + saved);
        slotArray = GameObject.FindGameObjectWithTag("Slots");
        Transform child;
        for (int i = 0; i < PlayerPrefs.GetInt("Slots.Count"); i++)
        {
            int tmp = PlayerPrefs.GetInt("Slots" + i, 0);
            child = slotArray.transform.GetChild(i);
            switch (tmp)
            {
                case 0:
                    break;
                case 1:
                    {
                        //Move
                        GameObject tmpObject = Instantiate(Resources.Load("Move")) as GameObject;
                        tmpObject.transform.SetParent(child);
                        break;
                    }
                case 2:
                    {
                        //Jump
                        GameObject tmpObject = Instantiate(Resources.Load("Jump")) as GameObject;
                        tmpObject.transform.SetParent(child);
                        break;
                    }
                case 3:
                    {
                        //Plus90
                        GameObject tmpObject = Instantiate(Resources.Load("Plus90")) as GameObject;
                        tmpObject.transform.SetParent(child);
                        break;
                    }
                case 4:
                    {
                        //Minus90
                        GameObject tmpObject = Instantiate(Resources.Load("Minus90")) as GameObject;
                        tmpObject.transform.SetParent(child);
                        break;
                    }
                case 5:
                    {
                        //For
                        GameObject tmpObject = Instantiate(Resources.Load("For")) as GameObject;
                        tmpObject.transform.SetParent(child);
                        break;
                    }
                case 6:
                    {
                        //If
                        GameObject tmpObject = Instantiate(Resources.Load("If")) as GameObject;
                        tmpObject.transform.SetParent(child);
                        break;
                    }
                case 7:
                    {
                        //While
                        GameObject tmpObject = Instantiate(Resources.Load("While")) as GameObject;
                        tmpObject.transform.SetParent(child);
                        break;
                    }
                case 8:
                    {
                        //&&
                        GameObject tmpObject = Instantiate(Resources.Load("&&")) as GameObject;
                        tmpObject.transform.SetParent(child);
                        break;
                    }
                default:
                    break;
            }
        }
    }
    #endregion
}
