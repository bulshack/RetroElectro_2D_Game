using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Designer : MonoBehaviour
{
    #region Members
    public enum CubeType { Space, Ground, Key1, Key2, Key3, Door1, Door2, Door3, Obstacle1, Obstacle2, Obstacle3, Enemy, Portal, Begin, End };
    public CubeType CurrentType;
    //Materials//
    public Color Cyan;
    public Color Magenta;
    public Color Green;
    public Color Blue;
    public Color Key2 = new Color(1, 0.784f, 0);
    public Color Key3 = new Color(1, 0.588f, 0);
    public Color Door2 = new Color(1, 0.498f, 0.392f);
    public Color Door3 = new Color(1, 0.498f, 0.588f);
    public Color Enemy = new Color(.709f, .901f, .113f);
    public Color Obstacle1 = new Color(.501f, 0, 0);
    public Color Obstacle2 = new Color(.501f, .196f, 0);
    public Color Obstacle3 = new Color(.501f, .196f, .392f);
    public Color Lever = new Color(0.501f, 0.313f, 0.2f);
    public GameObject GroundColor, lever, bKey, bDoor, bObstacle, pKey, pDoor,
                      pObstacle, oKey, oDoor, oObstacle, tele,
                      CurrentDoor, Currentkey, CurrentObstacle,
                      Teleporter, CurrentLever, groundTile,endGame;
    public int rand;
    #endregion

    #region Cube Functionality
    void Randomize_Map()
    {
        CurrentType = (CubeType)Random.Range(0, 1);
    }

    public void ResetColor()
    {
        UpdateType();
    }

    void UpdateType()
    {
        switch (CurrentType)
        {
            case CubeType.Space:
                {
                    GetComponent<Renderer>().material.color = Color.black;
                    break;
                }
            case CubeType.Ground:
                {
                    GetComponent<Renderer>().material.color = Cyan;
                    break;
                }
            case CubeType.End:
                {
                    GetComponent<Renderer>().material.color = Green;
                    break;
                }
            case CubeType.Portal:
                {
                    GetComponent<Renderer>().material.color = Blue;
                    break;
                }
            case CubeType.Door1:
                {
                    GetComponent<Renderer>().material.color = Color.red;
                    break;
                }
            case CubeType.Key1:
                {
                    GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                }
            case CubeType.Begin:
                {
                    GetComponent<Renderer>().material.color = Color.white;
                    break;
                }
            case CubeType.Key2:
                {
                    GetComponent<Renderer>().material.color = Key2;
                    break;
                }
            case CubeType.Door2:
                {
                    GetComponent<Renderer>().material.color = Door2;
                    break;
                }
            case CubeType.Key3:
                {
                    GetComponent<Renderer>().material.color = Key3;
                    break;
                }
            case CubeType.Door3:
                {
                    GetComponent<Renderer>().material.color = Door3;
                    break;
                }
            case CubeType.Enemy:
                {
                    GetComponent<Renderer>().material.color = Enemy;
                    break;
                }
            case CubeType.Obstacle1:
                {
                    GetComponent<Renderer>().material.color = Obstacle1;
                    break;
                }
            case CubeType.Obstacle2:
                {
                    GetComponent<Renderer>().material.color = Obstacle2;
                    break;
                }
            case CubeType.Obstacle3:
                {
                    GetComponent<Renderer>().material.color = Obstacle3;
                    break;
                }
        }

    }
    public void SetCubeType(int _type)
    {
        //Debug.Log("Type set!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        CurrentType = (CubeType)_type;
        UpdateType();
    }
    #endregion
    public void CreateNPOs()
    {

        #region Keys
        if (GetComponent<Renderer>().material.color == Color.yellow)
        {

            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            Currentkey = Instantiate(GetKey(), newTransform, Quaternion.identity);

        }
        if (GetComponent<Renderer>().material.color == Key2)
        {

            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            Currentkey = Instantiate(GetKey(), newTransform, Quaternion.identity);

        }
        if (GetComponent<Renderer>().material.color == Key3)
        {

            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            Currentkey = Instantiate(GetKey(), newTransform, Quaternion.identity);

        }
        #endregion
        #region Doors
        if (GetComponent<Renderer>().material.color == Color.red)
        {
            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            CurrentDoor = Instantiate(GetDoor(), newTransform, Quaternion.identity);
        }
        if (GetComponent<Renderer>().material.color == Door2)
        {
            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            CurrentDoor = Instantiate(GetDoor(), newTransform, Quaternion.identity);
        }
        if (GetComponent<Renderer>().material.color == Door3)
        {
            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            CurrentDoor = Instantiate(GetDoor(), newTransform, Quaternion.identity);
        }
        #endregion
        #region Obstacle
        if (GetComponent<Renderer>().material.color == Obstacle1)
        {
            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
            CurrentObstacle = Instantiate(GetObstacle(), newTransform, Quaternion.identity);
        }
        if (GetComponent<Renderer>().material.color == Obstacle2)
        {
            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
            CurrentObstacle = Instantiate(GetObstacle(), newTransform, Quaternion.identity);
        }
        if (GetComponent<Renderer>().material.color == Obstacle3)
        {
            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
            CurrentObstacle = Instantiate(GetObstacle(), newTransform, Quaternion.identity);
        }
        #endregion
        #region Teleporter
        if (GetComponent<Renderer>().material.color == Blue)
        {
            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            Teleporter = Instantiate(tele, newTransform, Quaternion.identity);
        }
        #endregion
        #region EndGame
        if (GetComponent<Renderer>().material.color == Green)
        {
            Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 9);
            Instantiate(endGame, newTransform, Quaternion.identity);
        }
#endregion

    }
    public void GroundTile() {
        Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        groundTile = Instantiate(GroundColor, newTransform, Quaternion.identity);
    }
    #region Unity Methods //Start


    void OnEnable()
    {


    }
    #endregion

    #region Accessors
    public string GetCubeType()
    {
        return CurrentType.ToString();
    }
    #endregion
    GameObject GetKey()
    {

        if (GetComponent<Renderer>().material.color == Key3)
        {
            bKey.GetComponent<KeysScript>().Blue = true;
            return bKey;
        }
        else if (GetComponent<Renderer>().material.color == Key2)
        {
            oKey.GetComponent<KeysScript>().Orange = true;
            return oKey;
        }
        else if (GetComponent<Renderer>().material.color == Color.yellow)
        {
            pKey.GetComponent<KeysScript>().Purple = true;
            return pKey;
        }
        else
        {
            return null;
        }

    }
    GameObject GetDoor()
    {
        if (GetComponent<Renderer>().material.color == Door3)
        {
            bDoor.GetComponent<DoorScript>().Blue = true;
            return bDoor;
        }
        else if (GetComponent<Renderer>().material.color == Door2)
        {
            oDoor.GetComponent<DoorScript>().Orange = true;
            return oDoor;
        }
        else if (GetComponent<Renderer>().material.color == Color.red)
        {
            pDoor.GetComponent<DoorScript>().Purple = true;
            return pDoor;
        }
        else
        {
            return null;
        }

    }
    GameObject GetObstacle()
    {

        if (GetComponent<Renderer>().material.color == Obstacle1)
        {
            bObstacle.GetComponent<ObstacleScript>().Blue = true;
            return bObstacle;
        }
        else if (GetComponent<Renderer>().material.color == Obstacle2)
        {
            oObstacle.GetComponent<ObstacleScript>().Orange = true;
            return oObstacle;
        }
        else if (GetComponent<Renderer>().material.color == Obstacle3)
        {
            pObstacle.GetComponent<ObstacleScript>().Purple = true;
            return pObstacle;
        }
        else
        {
            return null;
        }

    }




}
