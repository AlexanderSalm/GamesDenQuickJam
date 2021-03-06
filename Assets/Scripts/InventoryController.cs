using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static int slimeCount;
    public static int orcCount;
    public static int wizardCount;
    public static int ghostCount;

    public GameObject slimeText;
    public GameObject orcText;
    public GameObject wizardText;
    public GameObject ghostText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        slimeText.GetComponent<Text>().text = slimeCount.ToString();
        orcText.GetComponent<Text>().text = orcCount.ToString();
        wizardText.GetComponent<Text>().text = wizardCount.ToString();
        ghostText.GetComponent<Text>().text = ghostCount.ToString();
    }

    public static void addDrop(string droptype) {
        if (droptype == "slime") {
            slimeCount++;
        }
        else if (droptype == "orc") {
            orcCount++;
        }
        else if (droptype == "wizard") {
            wizardCount++;
        }
        else if (droptype == "ghost") {
            ghostCount++;
        }
    }

    public static void subtractHighestIngrediant() {
        if (slimeCount >= orcCount && slimeCount >= wizardCount && slimeCount >= ghostCount) {
            slimeCount--;
        }
        else if (orcCount >= slimeCount && orcCount >= wizardCount && orcCount >= ghostCount) {
            orcCount--;
        }
        else if (wizardCount >= slimeCount && wizardCount >= orcCount && wizardCount >= ghostCount) {
            wizardCount--;
        }
        else {
            ghostCount--;
        }
    }
}
