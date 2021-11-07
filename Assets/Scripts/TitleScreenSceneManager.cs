using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClicked() {
        PlayerController.health = PlayerController.maxHealth;
        InventoryController.slimeCount = 0;
        InventoryController.orcCount = 0;
        InventoryController.wizardCount = 0;
        InventoryController.ghostCount = 0;
        ConveyorBeltController.dishCount = 0;
        SceneManager.LoadScene("Dungeon");
    }

    public void ExitClicked() {
        Application.Quit();
    }

    public void RetryClicked() {
        PlayClicked();
    }

    public void TitleScreenClicked() {
        SceneManager.LoadScene("Title Screen");
    }
}
