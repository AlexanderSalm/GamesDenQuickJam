using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VignetteController : MonoBehaviour
{
    public GameObject conveyor;
    public float speed;
    // Start is called before the first frame update
    private Image img;
    private float alpha;
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float danger = conveyor.GetComponent<ConveyorBeltController>().getDanger();
        alpha = Mathf.Lerp(alpha, danger, speed * Time.deltaTime);
        setAlpha(img);
    }

    void setAlpha(Image i) {
        Color c = i.color;
        c.a = alpha;
        i.color = c;
    }
}
