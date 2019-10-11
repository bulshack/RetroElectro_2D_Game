using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool Blue, Orange, Purple, alive, isplaying;
    public int x;
    public GameObject[] cubes = new GameObject[64];
    Vector3 Move_Vector;
    public GameObject map;
  

    // Use this for initialization
    void Start()
    {
        Invoke("Startfunk", .2f);
    }
    private void Update()
    {
        ParticlePlayer();
      

    }
    void Startfunk()
    {

        alive = true;
      
        LoadCubes();
        for (int i = 0; i < 64; ++i)
        {
            if (cubes[i].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Door" & x == i)
            {
                transform.position = new Vector3(cubes[i].GetComponentInChildren<Transform>().position.x,
                                          cubes[i].GetComponentInChildren<Transform>().position.y,
                                          cubes[i].GetComponentInChildren<Transform>().position.z - 4);
                Move_Vector = transform.position;
                x = i;
            }
        }
        transform.rotation = Quaternion.identity; //reset rotation
    }
    private void OnEnable()
    {
        EventManager.Reset += reset;

    }
    void OnDisable()
    {

        EventManager.Reset -= reset;
    }

    void reset()
    {
        //alive = true;
        x = 0;
        Start();

    }
    void LoadCubes()
    {
        if (map == null)

        {
            int index = 0;
            map = GameObject.Find("Map (1)");
            Transform squares = map.transform.GetChild(1);
            for (int i = 0; i < 64; i++)
            {
                switch (i)
                {
                    case 0:
                        index = 56;
                        break;
                    case 8:
                        index = 48;
                        break;
                    case 16:
                        index = 40;
                        break;
                    case 24:
                        index = 32;
                        break;
                    case 32:
                        index = 24;
                        break;
                    case 40:
                        index = 16;
                        break;
                    case 48:
                        index = 8;
                        break;
                    case 56:
                        index = 0;
                        break;
                    default:
                        break;
                }

                cubes[i] = squares.GetChild(index).gameObject;
                index++;



            }

        }

    }
    public void ParticlePlayer()
    {


        if (alive)
        {
            GetComponent<Animator>().SetTrigger("Alive");
        }
        if (!alive)
        {
           
            GetComponent<Animator>().SetTrigger("Empty");
        }
    }
}
