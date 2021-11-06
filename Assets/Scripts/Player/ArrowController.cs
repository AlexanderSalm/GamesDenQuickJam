using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed;

    public Vector2 normalizedMovementVector;

    private Rigidbody2D rb;
    private Transform tf;
    private AudioSource hit;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        hit = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 vector = normalizedMovementVector * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + vector);
    }

    public void OnHit() {
        AudioSource.PlayClipAtPoint(hit.clip, rb.position);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            OnHit();
        }
    }
}
