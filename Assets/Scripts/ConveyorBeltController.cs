using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{
    public List<GameObject> easyDishes;
    public List<GameObject> hardDishes;

    public Vector2 dishesWaitRange;

    public int seperationDistance;
    public static int spacing;
    public int speed;
    public int dishesCap;
    public int hardDishesThreshold;

    public int dishesMade;
    public static int dishesMadeCount;
    public float speedThreshold;

    public static int dishCount;

    public List<GameObject> currentDishes;
    public List<GameObject> allDishes;

    public GameObject canvas;

    private float timeSinceLastDish;
    private bool hardAdded = false;
    private float timeToNextDish;

    private Transform tf;
    private AudioSource dishMade;
    private AudioSource newItem;
    public static AudioSource error;
    // Start is called before the first frame update
    void Start()
    {
        spacing = seperationDistance;
        dishMade = GetComponents<AudioSource>()[0];
        newItem = GetComponents<AudioSource>()[1];
        error = GetComponents<AudioSource>()[2];
        allDishes = new List<GameObject>();
        currentDishes = new List<GameObject>();
        tf = GetComponent<Transform>();
        for(int i = 0; i < easyDishes.Count; i++) {
            allDishes.Add(easyDishes[i]);
        }
        timeToNextDish = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        dishCount = currentDishes.Count;
        dishesMadeCount = dishesMade;
        if (dishesMade == hardDishesThreshold && !hardAdded) {
            hardAdded = true;
            for(int i = 0; i < hardDishes.Count; i++) {
                allDishes.Add(hardDishes[i]);
            }
        }

        for(int i = 0; i < currentDishes.Count; i++) {
            currentDishes[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.Lerp(-i * seperationDistance, currentDishes[i].GetComponent<RectTransform>().anchoredPosition.x, 1 - (speed * Time.deltaTime)), currentDishes[i].GetComponent<RectTransform>().anchoredPosition.y, currentDishes[i].transform.position.z); ;
        }

        timeSinceLastDish += Time.deltaTime;
        if (timeSinceLastDish > timeToNextDish) {
            addDish();
            timeToNextDish = Mathf.Lerp(Random.Range(dishesWaitRange.x, dishesWaitRange.y), 5, (float)dishesMade/speedThreshold);
            timeSinceLastDish = 0;
        }
        if (currentDishes.Count == Mathf.Min(dishesCap, dishesMade + 2)) timeSinceLastDish = 0;

    }

    void addDish() {
        int index = Random.Range(0, allDishes.Count);
        GameObject dish = Instantiate(allDishes[index]);
        dish.transform.SetParent(tf);
        dish.GetComponent<RectTransform>().anchoredPosition = new Vector2(1000, 0);
        //dish.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        dish.GetComponentInChildren<DishController>().canvas = canvas;
        currentDishes.Insert(0, dish);
        newItem.Play();

        for(int i = 0; i < currentDishes.Count; i++) {
            currentDishes[i].GetComponentInChildren<DishController>().index = i;
        }
    }

    public void makeDish(int index) {
        GameObject dishContainer = currentDishes[index];
        GameObject dish = dishContainer.transform.GetChild(4).gameObject;
        InventoryController.slimeCount -= dish.GetComponent<DishController>().slimeCost;
        InventoryController.orcCount -= dish.GetComponent<DishController>().orcCost;
        InventoryController.wizardCount -= dish.GetComponent<DishController>().wizardCost;
        InventoryController.ghostCount -= dish.GetComponent<DishController>().ghostCost;
        Debug.Log(index);
        currentDishes.RemoveAt(index);
        dishMade.Play();
        dish.GetComponent<DishController>().makeHook();
        dishesMade++;
        PlayerController.heal(20);
    }

    public float getDanger() {
        float maxDanger = 0;
        for(int i = 0; i < currentDishes.Count; i++) {
            float thisDanger = currentDishes[i].GetComponentInChildren<DishController>().danger;
            if (thisDanger > maxDanger) maxDanger = thisDanger;
        }
        return maxDanger;
    }
}
