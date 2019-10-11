using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ListCode : MonoBehaviour
{
    #region Members
    string DisplayText = "";
    string currentText = "";
    float delay = 0.02f;
    bool ExecutedClicked = false;
    public Button Execute;
    public GameObject ToolBox;
    public GameObject Buttons;
    public Transform Player;
    #endregion

    #region Unity Functions //Start, OnEnable

    void OnEnable()
    {
        EventManager.Move += Move;
        EventManager.Jump += Jump;
        EventManager.Rotate_ClockWise += Rotate_ClockWise;
        EventManager.Rotate_Counter_ClockWise += Rotate_CounterClockWise;
        EventManager.Reset += ResetPosition;
        EventManager.Begin += Begin;
    }
    void OnDisable()
    {
        EventManager.Move -= Move;
        EventManager.Jump -= Jump;
        EventManager.Rotate_ClockWise -= Rotate_ClockWise;
        EventManager.Rotate_Counter_ClockWise -= Rotate_CounterClockWise;
        EventManager.Reset -= ResetPosition;
        EventManager.Begin -= Begin;
    }
    void Start()
    {
        Button temp = Execute.GetComponent<Button>();
        temp.onClick.AddListener(ChangeBoolean);
        transform.localScale = new Vector3(0, 0, 0);
    }

    #endregion

    #region CodeBoard Utility
    void ClearBoard()
    {
        transform.GetChild(0).GetComponent<Text>().text = "";
    }
    IEnumerator Showtext()
    {
        for (int i = 0; i < DisplayText.Length + 1; i++)
        {
            currentText = DisplayText.Substring(0, i);
            transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
    void ChangeBoolean()
    {
        //Debug.Log("I am clicking"); 
        ExecutedClicked = !ExecutedClicked;
        // Start();
    }
    void Move()
    {
        StopAllCoroutines();

        ClearBoard();      
        DisplayText = "> Move Command Executing..... Variable: RobotPosition = RobotPosition + 1";

        StartCoroutine(Showtext());
    }
    void Jump()
    {
        StopAllCoroutines();
       // ClearBoard();
        DisplayText = "Jump Command Executing.... Variable: RobotPosition = RobotPosition + 2";
        StartCoroutine(Showtext());
    }
    void Rotate_ClockWise()
    {
        StopAllCoroutines();
        //ClearBoard();
        DisplayText = "\n\n> Clock-Wise Rotation Executing....Variable RobotDirection = -90";
        StartCoroutine(Showtext());
    }
    void Rotate_CounterClockWise()
    {
        StopAllCoroutines();
        //ClearBoard();
        DisplayText = "\n\n> Clock-Wise Rotation Executing...Variable RobotDirection = +90";
        StartCoroutine(Showtext());
    }
    void ResetPosition()
    {
        ClearBoard();
        ToolBox.transform.localScale = new Vector3(1, 1, 1);
        Buttons.transform.localScale = new Vector3(1, 1, 1);
        transform.localScale = new Vector3(0, 0, 0);
        StopAllCoroutines();
    }
    void Begin()
    {
        StopAllCoroutines();
        //ClearBoard();
        ToolBox.transform.localScale = new Vector3(0, 0, 0);
        Buttons.transform.localScale = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(Showtext());
    }
    #endregion
}
