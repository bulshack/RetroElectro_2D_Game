using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    #region Members
    public Sprite[] sprites = new Sprite[8];
    public Image Icon;
    Animator anim;
    public float speed = .5f;
    public int x;
    public int y;
    public int keys;
    private Vector3 Move_Vector;
    public ParticleSystem[] particles = new ParticleSystem[2];
    public GameObject[] cubes = new GameObject[64];
    public int direction = 1;
    public static int releasedLevelStatic = 1;
    public int releasedLevel;
    public GameObject map, logo, bKey, oKey, pKey;
    public GameObject completeLevelUI, losingLevelUI, timerUI, gameTimerUI;
    Game_Timer gameTimer;
    ExecuteScript_rev2 stopFunction;
    public bool Win, Lose;
    public AudioSource keySound;
    public AudioSource doorSound;
    public AudioSource teleportSound;
    //private float lerpTime = 3.0f;
    //private int lerpIndex = 0;
    //private float lerp;
    //private Renderer rend;
    //public List<Material> materialArray;
    //private Material target;
    //private Material nextTarget;
    //[SerializeField]
    //private Material baseMaterial;


    //Clock wise 
    //      1(up)   
    //  4(l)   2(r)
    //      3(dn)
    #endregion

    #region Movement Commands
    public void Move()
    {

        ParticlesRotate();
        for (int i = 0; i < particles.Length; i++)
        {
            if (i == 0)
                particles[i].Play();
            else
                particles[i].Stop();
        }
        //Move the player in the direction it's facing by map_width or map_height
        int temp = x;
        switch (direction)
        {

            case 1: //UP
                {
                    if (x + 8 > 63)
                    {
                        print("Out of bounds up");
                        break;
                    }

                    x += 8;
                    print(cubes[x].GetComponentInChildren<Transform>().position);
                    break;
                }
            case 2: //RIGHT
                {
                    if (x % 8 == 7)
                    {
                        print("Out of bounds right");
                        break;
                    }
                    x += 1;
                    print(cubes[x].GetComponentInChildren<Transform>().position);
                    break;
                }
            case 3: //DOWN
                {
                    if (x - 8 < 0)
                    {
                        print("Out of bounds down");
                        break;
                    }
                    x -= 8;
                    print(cubes[x].GetComponentInChildren<Transform>().position);
                    break;
                }
            case 4: //LEFT
                {
                    if (x % 8 == 0)
                    {
                        print("Out of bounds left");
                        break;
                    }
                    x -= 1;
                    print(cubes[x].GetComponentInChildren<Transform>().position);
                    break;
                }
            default:
                {
                    break;
                }

        }

        Debug.Log(cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString());
        if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Ground") //if the cube is regular ground it moves
        {
            // Debug.Log(cubes[x].GetComponentInChildren<Transform>().position);
            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
            return;
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Begin")
        {

            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
            return;
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Space")
        {

            anim.SetTrigger("Dead");
            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);

            Invoke("PlayerDead", .75f);
            x = temp;
            return;
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "End") //if the cube is the victory cube we end the current level
        {
            Move_Vector = new Vector3(
                cubes[x].GetComponentInChildren<Transform>().position.x,
                cubes[x].GetComponentInChildren<Transform>().position.y,
                cubes[x].GetComponentInChildren<Transform>().position.z - 4);
            Win = true;
            UnlockLevels();
            gameTimer.StopTimer();
            completeLevelUI.SetActive(true);
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Portal")
        {
            Move_Vector = new Vector3(
               cubes[x].transform.position.x,
               cubes[x].transform.position.y,
               cubes[x].transform.position.z - 4);


            Invoke("FindTele", .75f);


            teleportSound.Play();
            Invoke("FindTele",1);
            
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Key1")
        {
            #region Key1
            Move_Vector = new Vector3(cubes[x].transform.position.x,
                                      cubes[x].transform.position.y,
                                      cubes[x].transform.position.z - 4);
            keySound.Play();

            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Blue)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", true);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[1]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[6]);
                bKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                //   cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Orange)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", true);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[3]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[7]);
                oKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                //  cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Purple)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", true);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[2]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[5]);
                pKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                // cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");
            }

            #endregion
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Key2")
        {
            #region Key2
            Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
            keySound.Play();
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Blue)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", true);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[1]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[6]);
                bKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                // cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");

            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Orange)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", true);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[3]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[7]);
                oKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                //  cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Purple)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", true);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[2]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[5]);
                pKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                // cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");
            }
            return;
            #endregion
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Key3")
        {
            #region Key3
            Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
            keySound.Play();
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Blue)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", true);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[1]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[6]);
                bKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                // cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");

            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Orange)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", true);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[3]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[7]);
                oKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                // cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Purple)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", true);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[2]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[5]);
                pKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                //cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().GetComponent<Animator>().SetTrigger("Empty");
            }
            return;
            #endregion
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Door1")
        {
            if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Blue && bKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                                          cubes[x].transform.position.y,
                                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;
            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Orange && oKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;

            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Purple && pKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;
            }
            else if (!cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive)
            {
                Move_Vector = new Vector3(
            cubes[x].transform.position.x,
            cubes[x].transform.position.y,
            cubes[x].transform.position.z - 4);
                return;
            }
            else
            {
                Invoke("PlayerDead", 1);

                return;
            }

        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Door2")
        {
            if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Blue && bKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;
            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Orange && oKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;

            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Purple && pKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;
            }
            else if (!cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive)
            {
                Move_Vector = new Vector3(
            cubes[x].transform.position.x,
            cubes[x].transform.position.y,
            cubes[x].transform.position.z - 4);
                return;
            }
            else
            {

                Invoke("PlayerDead",1);

                return;
            }

        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Door3")
        {
            if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Blue && bKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;
            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Orange && oKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;

            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Purple && pKey)
            {
                Move_Vector = new Vector3(cubes[x].transform.position.x,
                          cubes[x].transform.position.y,
                          cubes[x].transform.position.z - 4);
                doorSound.Play();
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().GetComponent<Animator>().SetTrigger("Empty");
                return;
            }
            else if (!cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive)
            {
                Move_Vector = new Vector3(
            cubes[x].transform.position.x,
            cubes[x].transform.position.y,
            cubes[x].transform.position.z - 4);
                return;
            }
            else
            {
                Invoke("PlayerDead", 1);

                return;
            }

        }
        else
        {
            //PlayerDead();
            //x = temp;

        }

    }
    void FindTele()
    {
        for (int k = 0; k < 64; ++k)
        {
            //Find the other portal
            if (cubes[k].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Portal" && k != x)
            {
                transform.position = cubes[k].GetComponentInChildren<Transform>().position;
                Move_Vector = new Vector3(
                    cubes[k].transform.position.x,
                    cubes[k].transform.position.y,
                    cubes[k].transform.position.z - 4);
                x = k;
                break;
            }

        }
    }
    void PlayerDead()
    {
        print("Player Dead");
        Lose = true;


    }
    public void Jump()
    {
        ParticlesRotate();
        for (int i = 0; i < particles.Length; i++)
        {
            if (i == 1)
                particles[i].Play();
            else
                particles[i].Stop();
        }
        anim.SetTrigger("Jump");
        int temp = x;

        //Move player 2 squares in the position, change the scale so it appears as it is jumpingswitch (direction)
        switch (direction)
        {
            case 1: //UP
                {
                    if (x + 16 > 63)
                    {
                        print("Out of bounds up");
                        break;
                    }
                    x += 16;
                    print(cubes[x].GetComponentInChildren<Transform>().position);
                    break;
                }
            case 2: //RIGHT
                {
                    if (x % 8 > 5)
                    {
                        print("Out of bounds right");
                        break;
                    }
                    x += 2;
                    print(cubes[x].GetComponentInChildren<Transform>().position);
                    break;
                }
            case 3: //DOWN
                {
                    if (x - 16 < 0)
                    {
                        print("Out of bounds down");
                        break;
                    }
                    x -= 16;
                    print(cubes[x].GetComponentInChildren<Transform>().position);
                    break;
                }
            case 4: //LEFT
                {
                    if (x % 8 < 2)
                    {

                        print("Out of bounds left ");
                        break;
                    }
                    x -= 2;
                    print(cubes[x].GetComponentInChildren<Transform>().position);
                    break;
                }
            default:
                {
                    break;
                }

        }


        if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Ground") //if the cube is regular ground it moves
        {
            // Debug.Log(cubes[x].GetComponentInChildren<Transform>().position);
            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
            return;
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Begin")
        {

            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
            return;
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Space")
        {

            anim.SetTrigger("Dead");
            for (int i = 0; i < particles.Length; i++)
            {

                if (particles[i].isPlaying)
                    particles[i].Stop();

            }

            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);

            Invoke("PlayerDead", .75f);
            x = temp;
            return;
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "End") //if the cube is the victory cube we end the current level
        {
            Move_Vector = new Vector3(
                cubes[x].GetComponentInChildren<Transform>().position.x,
                cubes[x].GetComponentInChildren<Transform>().position.y,
                cubes[x].GetComponentInChildren<Transform>().position.z - 4);
            Win = true;
            UnlockLevels();
            gameTimer.StopTimer();
            completeLevelUI.SetActive(true);
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Portal")
        {
            Move_Vector = new Vector3(
              cubes[x].transform.position.x,
              cubes[x].transform.position.y,
              cubes[x].transform.position.z - 4);

            Invoke("FindTele", .75f);

        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Key1")
        {
            #region Key1

            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Blue)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", true);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[1]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[6]);
                bKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[3].Play();
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Orange)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", true);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[3]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[7]);
                oKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[5].Play();
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Purple)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", true);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[2]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[5]);
                pKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[7].Play();
            }

            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
            return;
            #endregion
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Key2")
        {
            #region Key2

            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Blue)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", true);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[1]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[6]);
                bKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[3].Play();
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Orange)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", true);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[3]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[7]);
                oKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[5].Play();
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Purple)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", true);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[2]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[5]);
                pKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[7].Play();
            }

            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
            return;
            #endregion
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Key3")
        {
            #region Key3

            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Blue)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", true);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[1]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[6]);
                bKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[3].Play();
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Orange)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", false);
                logo.GetComponent<Animator>().SetBool("Orange", true);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[3]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[7]);
                oKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[5].Play();
            }
            if (cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().Purple)
            {
                keys += 1;
                logo.GetComponent<Animator>().SetBool("Blue", false);
                logo.GetComponent<Animator>().SetBool("Green", false);
                logo.GetComponent<Animator>().SetBool("Purple", true);
                logo.GetComponent<Animator>().SetBool("Orange", false);
                particles[0].textureSheetAnimation.SetSprite(0, sprites[2]);
                particles[1].textureSheetAnimation.SetSprite(0, sprites[5]);
                pKey = cubes[x].GetComponentInChildren<Map_Designer>().Currentkey;
                cubes[x].GetComponentInChildren<Map_Designer>().Currentkey.GetComponent<KeysScript>().alive = false;
                particles[7].Play();
            }

            Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
            return;
            #endregion
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Door1")
        {
            #region Door1
            if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Blue && bKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                particles[2].Play();
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;
            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Orange && oKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                particles[4].Play();
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;

            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Purple && pKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                particles[6].Play();
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;
            }
            else if (!cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive)
            {
                Move_Vector = new Vector3(
               cubes[x].transform.position.x,
               cubes[x].transform.position.y,
               cubes[x].transform.position.z - 4);
                return;
            }
            else
            {
                Invoke("PlayerDead", 1);

                return;
            }
            #endregion
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Door2")
        {
            #region Door2
            if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Blue && bKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;
            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Orange && oKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;

            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Purple && pKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;
            }
            else if (!cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive)
            {
                Move_Vector = new Vector3(
               cubes[x].transform.position.x,
               cubes[x].transform.position.y,
               cubes[x].transform.position.z - 4);
                return;
            }
            else
            {
                Invoke("PlayerDead", 1);

                return;
            }
            #endregion
        }
        else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Door3")
        {
            #region Door3
            if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Blue && bKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;
            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Orange && oKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;

            }
            else if (cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().Purple && pKey)
            {
                keys -= 1;
                cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive = false;
                Move_Vector = new Vector3(
                cubes[x].transform.position.x,
                cubes[x].transform.position.y,
                cubes[x].transform.position.z - 4);
                return;
            }
            else if (!cubes[x].GetComponentInChildren<Map_Designer>().CurrentDoor.GetComponent<DoorScript>().alive)
            {
                Move_Vector = new Vector3(
               cubes[x].transform.position.x,
               cubes[x].transform.position.y,
               cubes[x].transform.position.z - 4);
                return;
            }
            else
            {
                Invoke("PlayerDead", 1);
                return;
            }
            #endregion
        }

    }

    public void Rotate_ClockWise()
    {
        ParticlesRotate();
        //Rotates on Z axis by -90 degrees
        if (direction == 4) direction = 1;
        else direction += 1;
        transform.Rotate(new Vector3(0, 0, 1), -90f);
    }

    public void Rotate_CounterClockWise()
    {
        ParticlesRotate();
        if (direction == 1) direction = 4;
        else direction -= 1;
        transform.Rotate(new Vector3(0, 0, 1), 90f);
    }

    #endregion //Move, Jump, Rotate 90, Rotate -90

    protected void UnlockLevels()
    {
        if (releasedLevelStatic <= releasedLevel)
        {
            releasedLevelStatic = releasedLevel;
            PlayerPrefs.SetInt("Level", releasedLevelStatic);
        }
        Win = true;

    }


    #region Map Functions
    public void EndGame()
    {
        if (Win)
        {
            for (int i = 0; i < 64; ++i)
            {
                cubes[i].GetComponentInChildren<Renderer>().material.color = Color.green;
            }
            gameTimer.StopTimer();
            completeLevelUI.SetActive(true);
        }
        if (    Lose)
        {
            for (int i = 0; i < 64; ++i)
            {
                cubes[i].GetComponentInChildren<Renderer>().material.color = Color.black;
            }
            Debug.Log("dead");
            losingLevelUI.SetActive(true);
            EventManager.C_Reset();

        }
    }
    void ResetPosition()
    {
        x = 0;
        direction = 1;
        anim.SetTrigger("Alive");
        SetBegin();
        ResetLogo();
        ResetKeys();
    }

    //IEnumerator LoadLevelMenu()
    //{
    //    yield return new WaitForSeconds(2);
    //    SceneManager.LoadScene("MapSelector");
    //}

    #endregion

    #region Developer Helper Functions
   void InputCheck() //Debugging
   {
       //if (Input.GetKeyDown("space"))
       //{
       //    Move();
       //    print("Space Key was pressed");
       //}
       //if (Input.GetKeyDown(KeyCode.LeftArrow))
       //{
       //    Rotate_CounterClockWise();
       //    print("Left Arrow Key was pressed");
       //}
       //if (Input.GetKeyDown(KeyCode.RightArrow))
       //{
       //    Rotate_ClockWise();
       //    print("Right Arrow Key was pressed");
       //}
  
   }

    //void NextMaterial()
    //{
    //    Debug.Log("NextMaterial() called.");
    //    if (lerpIndex >= (materialArray.Count - 1))
    //        lerpIndex = 0;
    //    else
    //        lerpIndex++;
    //    target = materialArray[lerpIndex];
    //}
        #endregion

        #region Unity Functions //Start, Update, OnEnable, OnDisable
        void Start()
    {
        gameTimer = GameObject.Find("Game_Timer").GetComponent<Game_Timer>();
        Icon = GameObject.Find("Key Icon").GetComponent<Image>();
        completeLevelUI = GameObject.Find("VictoryScreen");
        losingLevelUI = GameObject.Find("LosingScreen");
        timerUI = GameObject.Find("Game_Timer");
        DisableCompetitiveTimer();
        Win = Lose = false;
        //rend = GetComponent<Renderer>();
        //target = materialArray[lerpIndex];
        //nextTarget = materialArray[lerpIndex + 1];
        completeLevelUI.SetActive(false);
        losingLevelUI.SetActive(false);
        Invoke("Startfunk", .5f);

    }


    void Update()
    {
        if (Icon)
        {
            if (bKey)
            {
                oKey = null;
                pKey = null;
                Icon.GetComponent<Animator>().SetTrigger("Blue");
            }
            if (oKey)
            {
                bKey = null;
                pKey = null;
                Icon.GetComponent<Animator>().SetTrigger("Green");
            }
            if (pKey)
            {
                oKey = null;
                bKey = null;
                Icon.GetComponent<Animator>().SetTrigger("Purple");
            }
            if (!bKey && !oKey && !pKey)
            {
                ResetKeys();
                Icon.GetComponent<Animator>().SetTrigger("Empty");
            }

        }

        EndGame();
        //lerp = Mathf.PingPong(Time.time, lerpTime) / lerpTime;
        //rend.material.Lerp(baseMaterial, target, lerp);
        //if (lerp <= 0.01)
        //    NextMaterial();
        InputCheck();
        transform.position = Vector3.MoveTowards(transform.position, Move_Vector, speed * Time.deltaTime);
    }
    void OnEnable()
    {
        EventManager.Move += Move;
        EventManager.Jump += Jump;
        EventManager.Rotate_ClockWise += Rotate_ClockWise;
        EventManager.Rotate_Counter_ClockWise += Rotate_CounterClockWise;
        EventManager.Reset += ResetPosition;
        anim = GetComponentInChildren<Animator>();
    }

    void OnDisable()
    {
        EventManager.Move -= Move;
        EventManager.Jump -= Jump;
        EventManager.Rotate_ClockWise -= Rotate_ClockWise;
        EventManager.Rotate_Counter_ClockWise -= Rotate_CounterClockWise;
        EventManager.Reset -= ResetPosition;
    }

    #endregion

    #region SargFunks
    void DisableCompetitiveTimer()
    {
        if (SceneManager.GetActiveScene().name == "CompetitiveLevel_1" || SceneManager.GetActiveScene().name == "CompetitiveLevel_2" || SceneManager.GetActiveScene().name == "CompetitiveLevel_3"
            || SceneManager.GetActiveScene().name == "CompetitiveLevel_4" || SceneManager.GetActiveScene().name == "CompetitiveLevel_5" || SceneManager.GetActiveScene().name == "CompetitiveLevel_6" || SceneManager.GetActiveScene().name == "CompetitiveLevel_7" || SceneManager.GetActiveScene().name == "CompetitiveLevel_8" || SceneManager.GetActiveScene().name == "CompetitiveLevel_9" || SceneManager.GetActiveScene().name == "CompetitiveLevel_10")
        {
            Debug.Log(SceneManager.GetActiveScene().name == "CompetitiveLevel_1");
            Debug.Log("kill"); timerUI.SetActive(true);
        }
        else
            timerUI.SetActive(false);
    }
    void Startfunk()
    {

        ResetKeys();
        LoadCubes();
        SetBegin();
        transform.rotation = Quaternion.identity; //reset rotation
        ResetLogo();



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
    void ParticlesRotate()
    {
        var vel = particles[0].velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.World;
        AnimationCurve curve = new AnimationCurve();
        switch (direction)
        {
            case 1:
                //curve.AddKey(0.0f, 1.0f);
                curve.AddKey(1.0f, 0.0f);
                vel.x = new ParticleSystem.MinMaxCurve(0.0f, curve);
                vel.y = new ParticleSystem.MinMaxCurve(-10.0f, curve);
                vel.z = new ParticleSystem.MinMaxCurve(0.0f, curve);
                break;
            case 2:
                // curve.AddKey(0.0f, 1.0f);
                curve.AddKey(1.0f, 0.0f);
                vel.x = new ParticleSystem.MinMaxCurve(-10.0f, curve);
                vel.y = new ParticleSystem.MinMaxCurve(0.0f, curve);
                vel.z = new ParticleSystem.MinMaxCurve(0.0f, curve);
                break;
            case 3:
                //  curve.AddKey(0.0f, 1.0f);
                curve.AddKey(1.0f, 0.0f);
                vel.x = new ParticleSystem.MinMaxCurve(10.0f, curve);
                vel.y = new ParticleSystem.MinMaxCurve(0.0f, curve);
                vel.z = new ParticleSystem.MinMaxCurve(0.0f, curve);
                break;
            case 4:
                //   curve.AddKey(0.0f, 1.0f);
                curve.AddKey(1.0f, 0.0f);
                vel.x = new ParticleSystem.MinMaxCurve(10.0f, curve);
                vel.y = new ParticleSystem.MinMaxCurve(0.0f, curve);
                vel.z = new ParticleSystem.MinMaxCurve(0.0f, curve);
                break;
            default:
                break;
        }



    }
    void SetBegin()
    {
        for (int i = 0; i < 64; ++i)
        {
            if (cubes[i].GetComponentInChildren<Map_Designer>().CurrentType.ToString() == "Begin")
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
    void ResetKeys()
    {
        pKey = null;
        oKey = null;
        bKey = null;
    }
    void ResetLogo()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
        };
        logo.GetComponent<Animator>().SetBool("Blue", false);
        logo.GetComponent<Animator>().SetBool("Green", true);
        logo.GetComponent<Animator>().SetBool("Purple", false);
        logo.GetComponent<Animator>().SetBool("Orange", false);
        particles[0].textureSheetAnimation.SetSprite(0, sprites[0]);
        particles[1].textureSheetAnimation.SetSprite(0, sprites[4]);

    }
    #endregion
}
