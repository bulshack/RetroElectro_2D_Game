using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpPanelScript : MonoBehaviour {

  

  //
    string DisplayText = "";
    string currentText = "";
    float delay = 0.02f;
    // 

    public GameObject currentLeveltxt;
    public GameObject HelpBack;
    public GameObject HelpPanelB;
    public GameObject HelpPanel;
    public bool ActorOff;
   
    // Use this for initialization
    void Start () {


        //HelpPanel = GameObject.Find("HelpPanel");
        HelpPanelB = GameObject.Find("Help_Button");
        HelpBack = GameObject.Find("Back_HelpButton");
        currentLeveltxt = GameObject.Find("CurrentLevel");
        if (HelpBack == null || HelpPanelB == null || HelpPanel == null || currentLeveltxt == null)
        {
            Debug.Log("One of the Buttons of the Help Panel is null or the panel itself");
            Debug.Log("Help Panel Disable, check line 30 Help Script");

            this.enabled = false; 

            return;
        }

        ActorOff = false; 

        HelpPanel.gameObject.SetActive(false);  

        Button btn = HelpPanelB.GetComponent<Button>(); 

        btn.onClick.AddListener(TaskOnClick);

    }





    public void retuning()
    {
        ActorOff = false;
        HelpPanel.gameObject.SetActive(false);
        Debug.Log(ActorOff);
    }


    // Update is called once per frame
  

   public void TaskOnClick()
    {
        ActorOff = !ActorOff;
        Debug.Log(ActorOff);


        if (ActorOff == true)
        {
            HelpPanel.gameObject.SetActive(true);
   
            Scene _activeS = SceneManager.GetActiveScene();
            //currentLeveltxt.transform.GetComponent<Text>().text = _activeS.name;
            Debug.Log(currentLeveltxt);
            //ClearBoard();
            DisplayText = "This help Panel was designed to help you complete levels. This is a Demo of the help Panel. This panel is still in development. Thank you.";
            StartCoroutine(Showtext());

        }

        else if (ActorOff == false)
        {
            HelpPanel.gameObject.SetActive(false);

        }

    }



    IEnumerator Showtext()
    {
        for (int i = 0; i < DisplayText.Length + 1; i++)
        {
            //currentText = DisplayText.Substring(0, i);
            //gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }



    //void ClearBoard()
    //{
    //    this.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "";
    //}

}
