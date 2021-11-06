using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    public int contactDamage;
    public int specialDamage;
    public int health;

    private float timeSinceLastHit = 100;
    public float iFramesDuration;
    public Vector2 knockback;
    public float knockbackDuration;

    public int knifeDamage;
    public int bowDamage;
    public float knifeKnockback;
    public float bowKnockback;

    public GameObject drop;

    //private bool iFramesFlasher = false;

    private Transform tf;
    private Rigidbody2D rb;
    private AudioSource hit;
    private AudioSource die;

    // Start is called before the first frame update
    public void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        hit = GetComponents<AudioSource>()[0];
        die = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    public void Update()
    {
        float dt = Time.deltaTime;
        timeSinceLastHit += dt;

        knockback.x = Mathf.Lerp(knockback.x, 0, knockbackDuration * dt);

        knockback.y = Mathf.Lerp(knockback.y, 0, knockbackDuration * dt);

        Vector2 newPos = new Vector3(
            rb.position.x + knockback.x * dt,
            rb.position.y + knockback.y * dt);

        rb.MovePosition(newPos);

        tf.eulerAngles = new Vector3(0, 0, 0);

        //GetComponent<SpriteRenderer>().enabled = iFramesFlasher || timeSinceLastHit > iFramesDuration;
        //iFramesFlasher = !iFramesFlasher;
    }

    public void OnCollisionStay2D(Collision2D col) {
        if (timeSinceLastHit > iFramesDuration) {
            Vector2 vel = col.relativeVelocity.normalized;
            Debug.Log(vel);
            if (col.gameObject.tag == "Knife") {
                Debug.Log("Knife Hit");
                knockback = new Vector2(
                    vel.x * knifeKnockback,
                    vel.y * knifeKnockback);

                timeSinceLastHit = 0;
                takeDamage(knifeDamage);
            }
            else if (col.gameObject.tag == "Arrow") {
                Debug.Log("Arrow Hit");
                knockback = new Vector2(
                    vel.x * bowKnockback,
                    vel.y * bowKnockback);

                timeSinceLastHit = 0;
                takeDamage(bowDamage);

                col.gameObject.GetComponent<ArrowController>().OnHit();
            }
        }
    }

    void takeDamage(int damage) {
        hit.Play();
        health -= damage;
        if (health <= 0) {
            died();
        }
    }

    void died() {
        AudioSource.PlayClipAtPoint(die.clip, rb.position);
        GameObject spawnedDrop = Instantiate(drop);
        spawnedDrop.GetComponent<Transform>().position = new Vector3(tf.position.x, tf.position.y, spawnedDrop.GetComponent<Transform>().position.z);

        Destroy(this.gameObject);
    }


}
