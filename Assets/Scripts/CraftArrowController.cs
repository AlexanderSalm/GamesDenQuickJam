using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftArrowController : MonoBehaviour
{
    private RectTransform tf;
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<RectTransform>();
        startPos = tf.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        tf.anchoredPosition = new Vector2((ConveyorBeltController.dishCount-1) * -ConveyorBeltController.spacing, 0) + startPos;
        setAlpha(GetComponent<Image>(), CraftMealController.distance);
    }
    void setAlpha(Image i, float closeness) {
        Color c = i.color;
        c.a = closeness;
        i.color = c;
    }
}
