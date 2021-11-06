using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed;

    public Vector2 normalizedMovementVector;

    private Rigidbody2D rb;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 vector = normalizedMovementVector * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + vector);
    }

    public void OnHit() {
        Destroy(this.gameObject);
    }
}
