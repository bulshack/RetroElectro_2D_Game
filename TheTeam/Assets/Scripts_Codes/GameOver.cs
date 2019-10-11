using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public void GameOverMan()
    {
        Debug.Log("Exit hit.");
        Application.Quit();
    }
}
