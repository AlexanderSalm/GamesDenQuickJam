using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishController : MonoBehaviour
{
    public int slimeCost;
    public int orcCost;
    public int wizardCost;
    public int ghostCost;

    public float timeLimit;

    public GameObject slimeIndicator;
    public GameObject orcIndicator;
    public GameObject wizardIndicator;
    public GameObject ghostIndicator;

    private Vector2 slimeDistance = new Vector2(25, 60);
    private Vector2 orcDistance = new Vector2(-30, 60);
    private Vector2 wizardDistance = new Vector2(25, 110);
    private Vector2 ghostDistance = new Vector2(-30, 110);

    public float sensitivity;

    public int index;

    public GameObject canvas;

    private RectTransform tf;
    private Canvas can;
    private float elapsedTime = 0.0f;
    private Image timerIndicator;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<RectTransform>();
        can = canvas.GetComponent<Canvas>();
        timerIndicator = tf.GetChild(0).GetComponent<Image>();

        slimeIndicator.GetComponentInChildren<Text>().text = slimeCost.ToString();
        orcIndicator.GetComponentInChildren<Text>().text = orcCost.ToString();
        wizardIndicator.GetComponentInChildren<Text>().text = wizardCost.ToString();
        ghostIndicator.GetComponentInChildren<Text>().text = ghostCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > timeLimit) PlayerController.plateExpiredHook();
        timerIndicator.fillAmount = elapsedTime / timeLimit;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(can.transform as RectTransform, Input.mousePosition, can.worldCamera, out pos);
        float closeness = Mathf.Min(Mathf.Max(1 - Vector3.Distance(tf.parent.transform.parent.GetComponent<RectTransform>().anchoredPosition + tf.parent.GetComponent<RectTransform>().anchoredPosition, pos) /sensitivity + 0.45f, 0), 1);

        slimeIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector3(-Mathf.Lerp(0, slimeDistance.x, closeness), -Mathf.Lerp(0, slimeDistance.y, closeness));
        setAlpha(slimeIndicator.GetComponent<Image>(), closeness);
        slimeIndicator.GetComponentInChildren<Text>().color = Color.white;
        if (InventoryController.slimeCount < slimeCost) slimeIndicator.GetComponentInChildren<Text>().color = Color.red;
        setTextAlpha(slimeIndicator, closeness);

        orcIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector3(-Mathf.Lerp(0, orcDistance.x, closeness), -Mathf.Lerp(0, orcDistance.y, closeness));
        setAlpha(orcIndicator.GetComponent<Image>(), closeness);
        orcIndicator.GetComponentInChildren<Text>().color = Color.white;
        if (InventoryController.orcCount < orcCost) orcIndicator.GetComponentInChildren<Text>().color = Color.red;
        setTextAlpha(orcIndicator, closeness);

        wizardIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector3(-Mathf.Lerp(0, wizardDistance.x, closeness), -Mathf.Lerp(0, wizardDistance.y, closeness));
        setAlpha(wizardIndicator.GetComponent<Image>(), closeness);
        wizardIndicator.GetComponentInChildren<Text>().color = Color.white;
        if (InventoryController.wizardCount < wizardCost) wizardIndicator.GetComponentInChildren<Text>().color = Color.red;
        setTextAlpha(wizardIndicator, closeness);

        ghostIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector3(-Mathf.Lerp(0, ghostDistance.x, closeness), -Mathf.Lerp(0, ghostDistance.y, closeness));
        setAlpha(ghostIndicator.GetComponent<Image>(), closeness);
        ghostIndicator.GetComponentInChildren<Text>().color = Color.white;
        if (InventoryController.ghostCount < ghostCost) ghostIndicator.GetComponentInChildren<Text>().color = Color.red;
        setTextAlpha(ghostIndicator, closeness);

        if (Input.GetKeyDown(KeyCode.Mouse0) && CraftMealController.distance > CraftMealController.minDistance) {
            if (RectTransformUtility.RectangleContainsScreenPoint(tf, Input.mousePosition)){
                if (InventoryController.slimeCount >= slimeCost && 
                    InventoryController.orcCount >= orcCost && 
                    InventoryController.wizardCount >= wizardCost &&
                    InventoryController.ghostCount >= ghostCost) {
                    GameObject.FindGameObjectsWithTag("Conveyor")[0].GetComponent<ConveyorBeltController>().makeDish(index);
                }
                else {

                }
            }
        }

    }

    void setAlpha(Image i, float closeness) {
        Color c = i.color;
        c.a = closeness;
        i.color = c;
    }

    void setTextAlpha(GameObject t, float closeness) {
        Color c = t.GetComponentInChildren<Text>().color;
        c.a = closeness;
        t.GetComponentInChildren<Text>().color = c;
    }

    public void makeHook() {
        Debug.Log("make hook");
        foreach (Transform child in tf.parent) {
            Destroy(child.gameObject);
        }
    }
}
