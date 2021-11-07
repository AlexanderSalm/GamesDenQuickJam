using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpinController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + (speed * dt));
        transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
