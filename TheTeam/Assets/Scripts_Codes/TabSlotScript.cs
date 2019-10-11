using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabSlotScript : MonoBehaviour, IPointerClickHandler
{


    public GameObject slots;

    private GameObject[] _Slots = new GameObject[28];





    private void Start()
    {
        for (int i = 0; i < _Slots.Length; i++)
        {
            _Slots[i] = slots.transform.GetChild(i).gameObject;
        }
    }



    public void OnPointerClick(PointerEventData pointerEventData)
    {

        if (pointerEventData.clickCount == 2)
        {

            if (gameObject.transform.parent.name[0] != 'S')
            {
                for (int i = 0; i < _Slots.Length; i++)
                {
                    if (_Slots[i].transform.childCount == 1)
                    {

                        var Clone = Instantiate(gameObject, _Slots[i].transform, true);

                        Clone.transform.SetParent(_Slots[i].transform);

                        Clone.transform.position = _Slots[i].transform.position;

                        // Destroy(Clone.GetComponent("SortByClicking"));
                        break;

                    }
                }

            }
        }

        else if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (gameObject.transform.parent.parent.name == _Slots[0].transform.parent.name)
            {
                Destroy(gameObject);
            }

        }

    }


}


