using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    #region Delegates
    public delegate void ToolCommand();
    #endregion

    #region Public Static Events
    public static event ToolCommand Move;
    public static event ToolCommand Jump;
    public static event ToolCommand Rotate_ClockWise;
    public static event ToolCommand Rotate_Counter_ClockWise;
    public static event ToolCommand Reset;
    public static event ToolCommand Generate_Map;

    public static event ToolCommand Begin;
    #endregion

    #region C_Functions
    public static void C_Move()
    {
        Move();
    }

    public static void C_Jump()
    {
        Jump();
    }

    public static void C_RotateClockWise()
    {
        Rotate_ClockWise();
    }

    public static void C_RotateCounterClockWise()
    {
        Rotate_Counter_ClockWise();
    }

    public static void C_Reset()
    {
        Reset();
    }

    public static void Randomize_Map()
    {
        Generate_Map();
    }

    public static void C_Begin()
    {
        Begin();
    }
    #endregion

}
