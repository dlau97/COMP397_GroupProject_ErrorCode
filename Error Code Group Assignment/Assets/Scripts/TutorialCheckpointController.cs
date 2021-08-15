using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialCheckpointController : MonoBehaviour
{

    private TutorialController tutorialController;
    private GameObject tutorialTextObj;
    private TextMeshProUGUI tutorialText;

    public int tutorialCheckpointID = 0;

    // Start is called before the first frame update
    void Start()
    {
        tutorialController = GameObject.Find("Game Manager").GetComponent<TutorialController>();
        tutorialTextObj = GameObject.Find("Txt_Tutorial");
        tutorialText = tutorialTextObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" || other.transform.tag == "Mech"){
            tutorialTextObj.SetActive(true);
            tutorialController.SendMessage("SetTutorialText", tutorialCheckpointID);
        }

    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player" || other.transform.tag == "Mech"){
            tutorialTextObj.SetActive(false);
        }
    }
}
