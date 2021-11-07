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
    void Start()
    {
        
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
