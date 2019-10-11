using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Scaler : MonoBehaviour {

    private Vector3 scale_GO;
    private Vector3 scale_Plane;
    private Vector3 scale_Cube;
    private float x;
    private float y;
    private float z;
    private float size;
    private float width;
    private float height;

    void Start () {
        width = Screen.width;
        height = Screen.height;
        size = Camera.main.orthographicSize / 90;// (width / height);
        Debug.Log("Size of Map..." + size);
        x = y = z = size;
        scale_GO    = new Vector3(x, y, 1);
        scale_Plane = new Vector3(x, 1, z);
        scale_Cube  = new Vector3(x, y, 1);// x * 1.9f, y * 1.9f, 1);
                                                //Plane plane = gameObject.FindObjectOfType(Plane);
        gameObject.transform.localScale = scale_GO;
        GameObject plane = gameObject.transform.GetChild(64).gameObject;
        plane.transform.localScale = scale_Plane;
        //for (int i = 0; i < gameObject.transform.childCount-1; i++)
        //{
        //    GameObject cube = gameObject.transform.GetChild(i).gameObject;
        //    cube.transform.localScale = scale_Cube;
        //}


        //Debug.Log("Size of Map..." + gameObject.transform.localScale.x + "," + gameObject.transform.localScale.y + "," + gameObject.transform.localScale.z);
        //Debug.Log("Size of Plane..." + gameObject.transform.GetChild(64).transform.localScale);
        //Debug.Log("Size of Cube..." + gameObject.transform.GetChild(0).transform.localScale);
    }
}
