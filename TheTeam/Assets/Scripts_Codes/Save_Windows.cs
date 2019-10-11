using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Windows : MonoBehaviour
{

    /// <summary>
    /// List of things to save:
    /// -slot values - incld.
    /// -player position
    /// -current level - completed in LevelSelect.cs
    /// -music/sfx volume settings - completed in "audioSettings.cs"
    /// -game mode - completed in "GameModeSelection.cs"
    /// -score
    /// -time
    /// -locked levels - completed in "LockLevel.cs"
    /// </summary>


    #region Members
    public List<int> slots = new List<int>();
    private GameObject whiteboardPanel;
    #endregion

    #region Supporting Functions
    public void SlotValues()
    {
        whiteboardPanel = GameObject.FindGameObjectWithTag("Slots");
        Debug.Log("Parent: " + whiteboardPanel);
        Transform child;
        for (int i = 0; i < whiteboardPanel.transform.childCount; ++i)
        {
            child = whiteboardPanel.transform.GetChild(i);
            if (child.childCount > 1)
            {
                Debug.Log("Target to save: " + child.GetChild(1).tag);
                switch (child.GetChild(1).tag)
                {
                    case "Move_Tool":
                        {
                            slots.Add(1);
                            PlayerPrefs.SetInt("Slots" + i, slots[i]);
                            break;
                        }
                    case "Jump_Tool":
                        {
                            slots.Add(2);
                            PlayerPrefs.SetInt("Slots" + i, slots[i]);
                            break;
                        }
                    case "Plus90_Tool":
                        {
                            slots.Add(3);
                            PlayerPrefs.SetInt("Slots" + i, slots[i]);
                            break;
                        }
                    case "Minus90_Tool":
                        {
                            slots.Add(4);
                            PlayerPrefs.SetInt("Slots" + i, slots[i]);
                            break;
                        }
                    case "For_Tool":
                        {
                            slots.Add(5);
                            PlayerPrefs.SetInt("Slots" + i, slots[i]);
                            break;
                        }
                    case "If_Tool":
                        {
                            slots.Add(6);
                            PlayerPrefs.SetInt("Slots" + i, slots[i]);
                            break;
                        }
                    case "While_Tool":
                        {
                            slots.Add(7);
                            PlayerPrefs.SetInt("Slots" + i, slots[i]);
                            break;
                        }
                    case "&&_Tool":
                        {
                            slots.Add(8);
                            PlayerPrefs.SetInt("Slots" + i, slots[i]);
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        PlayerPrefs.SetInt("Slots.Count", slots.Count);
        Debug.Log("Slots.Count = " + PlayerPrefs.GetInt("Slots.Count"));
        for (int i = 0; i < slots.Count; i++)
        {
            Debug.Log("Slots" + i + ": " + slots[i]);
        }
    }

    #endregion
}
