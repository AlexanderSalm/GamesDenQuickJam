using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMealController : MonoBehaviour
{
    public static float minDistance;
    public float minDistanceSetter;
    public float sensitivity;

    public static float distance;

    private GameObject player;
    private Transform tf;
    private GameObject eGraphic;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        eGraphic = tf.GetChild(0).gameObject;
        minDistance = minDistanceSetter;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Min(Mathf.Max(1 - Vector3.Distance(tf.position, player.transform.position) / sensitivity + 0.45f, 0), 1);

        if (distance > minDistance) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("craft");
            }
        }


        setAlpha(eGraphic.GetComponent<SpriteRenderer>(), distance);
    }
    void setAlpha(SpriteRenderer i, float closeness) {
        Color c = i.color;
        c.a = closeness;
        i.color = c;
    }
}
