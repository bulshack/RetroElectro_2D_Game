using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Controller : MonoBehaviour
{
    #region Members
    public Texture2D MapImage;
    public GameObject MapPanel;
    float Tolerance = 0.03f;
    int x = 15;
    int y = 95;
    int NPOnumber;
    public Color Space = new Color(0.992f, 0.922f, 0.992f);
    public Color Ground = new Color(0.498f, 0.498f, 0.498f);
    public Color Key1 = new Color(1, 0.949f, 0);
    public Color Key2 = new Color(1, 0.784f, 0);
    public Color Key3 = new Color(1, 0.588f, 0);
    public Color Door1 = new Color(1, 0.498f, 0.153f);
    public Color Door2 = new Color(1, 0.498f, 0.392f);
    public Color Door3 = new Color(1, 0.498f, 0.588f);
    public Color Enemy = new Color(0.709f, 0.901f, 0.113f);
    public Color Obstacle1 = new Color(0.501f, 0, 0);
    public Color Obstacle2 = new Color(0.501f, 0.196f, 0);
    public Color Obstacle3 = new Color(0.501f, 0.196f, 0.392f);
    public Color Lever = new Color(0.501f, 0.313f, 0.2f);
    public Color Portal = new Color(0, 0.635f, 0.910f);
    public Color Begin = new Color(0.129f, 0.690f, 0.298f);
    public Color End = new Color(0.639f, 0.286f, 0.643f);


    #endregion

    #region Unity Methods
    void Awake()
    {
        for (int i = 0; i < 64; ++i) //for each panel
        {
            if (i % 8 == 0) //Update y pos after row is complete, move x back to left
            {
                y -= 10;
                x = 15;
            }
            else //move x one spot to the right
            {
                x += 10;
            }
            //Debug.Log("Color: " + MapImage.GetPixel(x, y) + '\n' + "Int:" + PixelToInt(MapImage.GetPixel(x, y)));
            transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().SetCubeType(PixelToInt(MapImage.GetPixel(x, y)));

        }

    }
    private void Start()
    {
        NPOnumber = Random.Range(0, 2);
        Invoke("AddNPOs", 1);
    }

    #endregion
    void AddNPOs()
    {
        int Doors = 0, Keys = 0, Obstacles = 0, kType, dType, oType;
        oType = kType = dType = NPOnumber;
        for (int i = 0; i < 64; i++)
        {
            if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() != "Space" /*|| transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() != "End"*/)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GroundTile();
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Key1")
                {


                    if (Keys > 0)
                    {
                        kType++;
                    }
                    if (kType > 2)
                    {
                        kType = 0;
                    }

                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = kType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();

                    ++Keys;

                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Key2")
                {
                    if (Keys > 0)
                    {
                        kType++;
                    }
                    if (kType > 2)
                    {
                        kType = 0;
                    }
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = kType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();

                    ++Keys;

                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Key3")
                {
                    if (Keys > 0)
                    {
                        kType++;
                    }
                    if (kType > 2)
                    {
                        kType = 0;
                    }
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = kType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                    ++Keys;

                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Portal")
                {
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Door1")
                {
                    if (Doors > 0)
                    {
                        dType--;
                    }
                    if (dType < 0)
                    {
                        dType = 2;
                    }
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = dType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                    ++Doors;

                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Door2")
                {
                    if (Doors > 0)
                    {
                        dType--;
                    }
                    if (dType < 0)
                    {
                        dType = 2;
                    }
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = dType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                    //necessary
                    ++kType;
                    //
                    ++Doors;

                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Door3")
                {
                    if (Doors > 0)
                    {
                        dType--;
                    }
                    if (dType < 0)
                    {
                        dType = 2;
                    }
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = dType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                    ++Doors;

                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Obstacle1")
                {

                    if (Obstacles > 0)
                    {
                        oType++;
                    }
                    if (oType > 2)
                    {
                        oType = 0;
                    }
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = oType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                    ++Obstacles;
                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Obstacle2")
                {

                    if (Obstacles > 0)
                    {
                        oType++;
                    }
                    if (oType > 2)
                    {
                        oType = 0;
                    }
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = oType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                    ++Obstacles;
                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "Obstacle3")
                {

                    if (Obstacles > 0)
                    {
                        oType++;
                    }
                    if (oType > 2)
                    {
                        oType = 0;
                    }
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().rand = oType;
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                    ++Obstacles;
                }
                if (transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().GetCubeType() == "End")
                {
                    transform.GetChild(i).GetChild(0).GetComponent<Map_Designer>().CreateNPOs();
                }

                
            }
        }
    }


    #region Map Utility


    int PixelToInt(Color _color)
    {
        if (ColorCompare(_color, Space)) return 0;
        if (ColorCompare(_color, Ground)) return 1;
        if (ColorCompare(_color, Key1)) return 2;
        if (ColorCompare(_color, Key2)) return 3;
        if (ColorCompare(_color, Key3)) return 4;
        if (ColorCompare(_color, Door1)) return 5;
        if (ColorCompare(_color, Door2)) return 6;
        if (ColorCompare(_color, Door3)) return 7;
        if (ColorCompare(_color, Obstacle1)) return 8;
        if (ColorCompare(_color, Obstacle2)) return 9;
        if (ColorCompare(_color, Obstacle3)) return 10;
        if (ColorCompare(_color, Enemy)) return 11;
        if (ColorCompare(_color, Portal)) return 12;
        if (ColorCompare(_color, End)) return 13;
        if (ColorCompare(_color, Begin)) return 14;



        return 0;
    }

    bool ColorCompare(Color lh, Color rh)
    {

        if (Tolerance > Mathf.Abs(lh.r - rh.r) && Tolerance > Mathf.Abs(lh.g - rh.g) && Tolerance > Mathf.Abs(lh.b - rh.b)) return true;
        else return false;
    }

    #endregion
}
