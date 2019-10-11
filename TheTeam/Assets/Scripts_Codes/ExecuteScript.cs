using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteScript : MonoBehaviour
{
    #region Members
    public GameObject[] ArraySlots = new GameObject[10];
    #endregion

    #region Event Methods
    public void OnClick()
    {
        uint seconds = 1;
        for (int i = 0; i < ArraySlots.Length; ++i)
        {
            if (ArraySlots[i].transform.childCount > 1)
            {
                StartCoroutine(ExecuteTool(ArraySlots[i].transform.GetChild(1).tag, seconds, ArraySlots[i]));
                seconds += 2;
            }
        }
        StartCoroutine(I_Reset(++seconds));
    }
    #endregion

    #region Functionality Methods
    IEnumerator ExecuteTool(string _type, uint _time, GameObject _tool)
    {
        yield return new WaitForSeconds(_time);

        //TODO: Make the tool game object passed in highlight when it calls the event
        switch (_type)
        {
            case "Jump_Tool":
                EventManager.C_Jump();
                break;

            case "Move_Tool":
                EventManager.C_Move();
                break;

            case "Plus90_Tool":
                EventManager.C_RotateCounterClockWise();
                break;

            case "Minus90_Tool":
                EventManager.C_RotateClockWise();
                break;
        }
    }

    IEnumerator I_Reset(uint _time)
    {
        yield return new WaitForSeconds(_time);
        EventManager.C_Reset();
    }
    public void Reset()
    {
        EventManager.C_Reset();
        int index = 0;
        for (int i = 0; i < ArraySlots.Length; i++)
        {
            for (int j = 0; j < ArraySlots[i].transform.childCount; j++)
            {
                index = j;
                if (index == 1)
                {
                    Transform temp = ArraySlots[i].transform.GetChild(1);
                    if (temp == null)
                    {
                        return;
                    }
                    Destroy(temp.gameObject);
                }
            }
        }
    }
    #endregion
}






