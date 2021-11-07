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

    public GameObject slimeIndicator;
    public GameObject orcIndicator;
    public GameObject wizardIndicator;
    public GameObject ghostIndicator;

    private Vector2 slimeDistance = new Vector2(25, 50);
    private Vector2 orcDistance = new Vector2(-25, 50);
    private Vector2 wizardDistance = new Vector2(25, 100);
    private Vector2 ghostDistance = new Vector2(-25, 100);

    public float sensitivity;

    public GameObject canvas;

    private RectTransform tf;
    private Canvas can;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<RectTransform>();
        can = canvas.GetComponent<Canvas>();

        slimeIndicator.GetComponentInChildren<Text>().text = slimeCost.ToString();
        orcIndicator.GetComponentInChildren<Text>().text = orcCost.ToString();
        wizardIndicator.GetComponentInChildren<Text>().text = wizardCost.ToString();
        ghostIndicator.GetComponentInChildren<Text>().text = ghostCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(can.transform as RectTransform, Input.mousePosition, can.worldCamera, out pos);
        float closeness = Mathf.Min(Mathf.Max(1 - Vector3.Distance(tf.parent.GetComponent<RectTransform>().anchoredPosition, pos) /sensitivity + 0.45f, 0), 1);

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
}
