using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialTextObj;
    private TextMeshProUGUI tutorialText;

    private string [] tutorialTextArray = new string[7];

    // Start is called before the first frame update
    void Start()
    {
        tutorialText = tutorialTextObj.GetComponent<TextMeshProUGUI>();
        tutorialText.text = "";
        tutorialTextArray[0] = "Left Stick To Move\nRight Stick To Aim";
        tutorialTextArray[1] = "Up Arrow Button To Jump";
        tutorialTextArray[2] = "Don't Fall!";
        tutorialTextArray[3] = "Lightning Bolt Button\nTo Dash Over Small Gaps";
        tutorialTextArray[4] = "Cross Buttons To Shoot\nLeft/Right/Both Weapons";
        tutorialTextArray[5] = "Gun Button to Open\\Close Inventory\nYou Can Swap Weapons From There";
        tutorialTextArray[6] = "Defeat all enemies to clear the level!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_ToggleTutorialText(){
        tutorialTextObj.SetActive(!tutorialText.IsActive());
    }

    public void SetTutorialText(int num){
        tutorialTextObj.SetActive(true);
        tutorialText.text = tutorialTextArray[num];
    }

}
