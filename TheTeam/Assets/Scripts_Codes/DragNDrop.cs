using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, 
							IDragHandler, IEndDragHandler{
   
  
	public static GameObject ItemBeingDragged;
	public static Vector3    startPosition;

    #region IBeginDragHandler implementation

    public void OnBeginDrag (PointerEventData eventData)
	{

        ItemBeingDragged = gameObject;
        startPosition = transform.position;
        ///////////////////////////////////////////
        //THIS WILL LOCK THE OBJECT ON THE SLOT
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        /////////////////////////////////////////////
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag (PointerEventData eventData)
	{



        if (this.transform.parent.name[0] == 'S')
        {
            GameObject.Find("Slots").transform.GetComponent<UnityEngine.UI.LayoutGroup>().enabled = false;
            this.transform.SetParent(GameObject.Find("Slots").transform); 
            

        }


        float zPos = -1.0f;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);   

        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);

     


        //Debug.Log("Current position: " + transform.position);
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag (PointerEventData eventData)
	{
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (Slots.duplicate == false && Slots.parentName == "Slots")
        {
            ItemBeingDragged.transform.position = startPosition;
            ItemBeingDragged = null;
           
        }
        else
        {
            if (gameObject.transform.parent.name[0] == 'S')
            {

                Destroy(gameObject);
                ItemBeingDragged = null;
            }
            

            transform.position = startPosition;
            ItemBeingDragged = null;
            Debug.Log("Failed to drop!");
        }
        Slots.duplicate = false;



       
    }



    #endregion

}
