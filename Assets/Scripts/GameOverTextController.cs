using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTextController : MonoBehaviour
{
    // Start is called before the first frame update
    private Color color;
    public float time;
    private float elapsedTime;
    public bool dm;
    public bool hs;
    void Start()
    {
        if (!PlayerPrefs.HasKey("HS")) PlayerPrefs.SetInt("HS", -1);

        if (ConveyorBeltController.dishesMadeCount > PlayerPrefs.GetInt("HS")) {
            PlayerPrefs.SetInt("HS", ConveyorBeltController.dishesMadeCount);
        }

        if (dm) GetComponent<Text>().text = PlayerController.deathMessage;
        else if (hs) GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HS").ToString();
        else GetComponent<Text>().text = "Score: " + ConveyorBeltController.dishesMadeCount.ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float val = Mathf.Lerp(color.r, 0, Mathf.Min(Mathf.Max(elapsedTime / time, 0), 1));
        color = new Color(Color.red.r, val, val);
        GetComponent<Text>().color = color;
    }
}
