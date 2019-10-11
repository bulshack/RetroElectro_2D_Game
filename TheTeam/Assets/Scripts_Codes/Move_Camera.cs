using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move_Camera : MonoBehaviour {

    public Transform target;
    Vector3 targetPosition, offset;
    float smoothFactor = 16;
    public bool camera_move;
    int i;
    public void Update()
    {

        
        if (camera_move)
        {
            targetPosition = target.position + offset;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, smoothFactor*Time.deltaTime);
            if (Vector3.Distance(Camera.main.transform.position, targetPosition) < 7)
                camera_move = false;
            Debug.Log("running");
        }
        
    }

    public void Clicked()
    {
        i = 0;
        camera_move = true;
    }
}
