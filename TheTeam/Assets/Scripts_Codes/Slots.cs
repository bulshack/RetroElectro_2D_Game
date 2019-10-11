using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading;

public class Slots : MonoBehaviour, IDropHandler {

    #region Members
    public GameObject obj;
    public static bool duplicate = false;
    public static string parentName = "";
    #endregion

    //#region Helper Functions
    // public GameObject Item
    // {
        // get
         //{
         //    if (transform.childCount > 1)
         //    {
         //        duplicate = true;
         //        return transform.GetChild(0).gameObject;
        //     }
        //     return null;
        // }
    // }
    //#endregion

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {

        if (gameObject.transform.childCount  >= 1)
        {

            if (gameObject.transform.childCount == 2)
            {
                GameObject childremover = gameObject.transform.GetChild(1).gameObject;

               // childremover.transform.parent = null;

                Destroy(childremover);
               
            }

            var new_go = Instantiate(DragNDrop.ItemBeingDragged, gameObject.transform, true);

            new_go.transform.SetParent(gameObject.transform);

            new_go.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;

            new_go.transform.position = gameObject.transform.position;

            obj = new_go;
        }

       
        
    }
    #endregion

}
