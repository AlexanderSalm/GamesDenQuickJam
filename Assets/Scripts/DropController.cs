using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    public string dropType;
    public float spawnVelocity;
    public float spawnDuration;

    public Vector2 velocity;
    private Rigidbody2D rb;
    private AudioSource collect;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        velocity.x = Random.Range(-1.0f, 1.0f) * spawnVelocity;
        velocity.y = Random.Range(-1.0f, 1.0f) * spawnVelocity;

        collect = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, 0, spawnDuration * dt);

        velocity.y = Mathf.Lerp(velocity.y, 0, spawnDuration * dt);


        rb.MovePosition(rb.position + velocity * dt);

        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
    }

    public void collected() {
        AudioSource.PlayClipAtPoint(collect.clip, rb.position);
        Destroy(this.gameObject);
    }
}