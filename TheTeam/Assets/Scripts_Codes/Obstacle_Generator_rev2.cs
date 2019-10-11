using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Generator_rev2 : MonoBehaviour
{
    #region Members
    public GameObject obstacle;
    public int level;
    public GameObject Map_Panel;
    private RectTransform MapRect;
    public float GridDimensions = 8;
    public float map_Width;
    public float map_Height;
    Random PosX;
    Random PosY;
    #endregion

    #region Unity Methods
    void Start()
    {
        MapRect = Map_Panel.transform.GetComponent<RectTransform>(); //Gets the rectangle (map background)
        map_Width = MapRect.sizeDelta.x * MapRect.localScale.x; //Gets the width of the entire map
        map_Height = MapRect.sizeDelta.y * MapRect.localScale.y;
        map_Width /= GridDimensions; //Splits up the length of the map by the amount of cells
        map_Height /= GridDimensions;

        //Random.InitState((int)Time.);
        //Instantiate(obstacle, new Vector3(transform.position.x + Random.Range(1, 8) * map_Width, Random.Range(1, 8) * map_Height), transform.rotation, transform);
        //Instantiate(obstacle, new Vector2(transform.position.x + Random.Range(1, 8), transform.position.y + Random.Range(1, 8)), transform.rotation);

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            InstantiateObstacle();
            print("Obstacle instantiated");
        }
    }
    #endregion

    #region Helper Methods
    void InstantiateObstacle()
    {
        Instantiate(obstacle, new Vector2(transform.position.x + Random.Range(1, 8) * map_Width, transform.position.y + Random.Range(1, 8) * map_Height), transform.rotation, transform);
    }
    #endregion
}
