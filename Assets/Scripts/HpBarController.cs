using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    public float speed;
    public float dispHealth;
    private Image full;
    // Start is called before the first frame update
    void Start()
    {
        full = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        dispHealth = Mathf.Lerp(dispHealth, (float)PlayerController.health, Time.deltaTime * speed);
        full.fillAmount = Mathf.Max(Mathf.Min((dispHealth / 100.0f) - 0.05f, 1), 0);
    }
}
