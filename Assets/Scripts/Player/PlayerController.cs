using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public static int health = 100;
    public static int maxHealth = 100;

    public float speed;
    public string enemyTag;

    public GameObject knife;
    public GameObject bow;
    public GameObject arrow;

    public float knifeDistance;
    public float bowDistance;

    public float timeSinceLastAttack = 100;
    public float timeSinceLastHit = 100;
    public float iFramesDuration;

    public float knifeCooldown;
    public float bowCooldown;

    public float swingDistance;
    public float swingDuration;
    private float currSwingDistance;
    private bool swinging = false;

    private bool iFramesFlasher = true;

    public bool knifeActive = true;
    public bool bowActive = false;

    public bool inKitchen = false;

    public GameObject hpText;

    public GameObject dungeonThreshold;
    public GameObject dungeonGenerator;
    
    private Transform tf;
    private Rigidbody2D rb;
    private Camera camera;
    // Start is called before the first frame update
    void Start() {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        camera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
    }

    void Update() {

        //unfixedMovementHandle();

        float dt = Time.deltaTime;
        timeSinceLastAttack += dt;
        timeSinceLastHit += dt;

        hpText.GetComponent<Text>().text = "HP: " + health.ToString();

        rotateWeapons();

        if (!inKitchen) handleAttacks();

        GetComponent<SpriteRenderer>().enabled = iFramesFlasher || timeSinceLastHit > iFramesDuration;
        iFramesFlasher = !iFramesFlasher;

        tf.eulerAngles = new Vector3(0, 0, 0);

        if (tf.position.x < dungeonThreshold.GetComponent<Transform>().position.x) {
            if (!inKitchen) {
                enterKitchen();
            }
        }
        else if (tf.position.x > dungeonThreshold.GetComponent<Transform>().position.x) {
            if (inKitchen) {
                enterDungeon();
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate() {
        movementHandle();

    }

    void takeDamage(int damage) {
        Debug.Log("Taking Damage: " + damage.ToString());
        health -= damage;

        timeSinceLastHit = 0;

        
        if (health <= 0) {
            died();
        }
    }

    public static void heal(int damage) {
        health = Mathf.Max(health + damage, 100);
        
    }

    void died() {
        Debug.Log("Died :(");
    }

    void rotateWeapons() {
        knife.GetComponent<SpriteRenderer>().enabled = knifeActive && !inKitchen;
        bow.GetComponent<SpriteRenderer>().enabled = bowActive && !inKitchen;

        Vector3 mousePoint = camera.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(tf.position.y - mousePoint.y, tf.position.x - mousePoint.x) * Mathf.Rad2Deg;
        //float angle = Vector2.SignedAngle(new Vector2(tf.position.x, tf.position.y), new Vector2(mousePoint.x,mousePoint.y)) + 180;

        knife.GetComponent<Transform>().eulerAngles = new Vector3(tf.eulerAngles.x, tf.eulerAngles.y, angle + 90);
        bow.GetComponent<Transform>().eulerAngles = new Vector3(tf.eulerAngles.x, tf.eulerAngles.y, angle + 180);

        Vector3 knifeOffset = new Vector3(Mathf.Cos((angle + 180) * Mathf.Deg2Rad) * (knifeDistance + currSwingDistance), Mathf.Sin((angle + 180) * Mathf.Deg2Rad) * (knifeDistance + currSwingDistance), knife.GetComponent<Transform>().position.z);
        Vector3 bowOffset = new Vector3(Mathf.Cos((angle + 180) * Mathf.Deg2Rad) * bowDistance, Mathf.Sin((angle + 180) * Mathf.Deg2Rad) * bowDistance, bow.GetComponent<Transform>().position.z);

        bow.GetComponent<Transform>().localPosition = bowOffset;
        knife.GetComponent<Transform>().localPosition = knifeOffset;
        //bow.GetComponent<Rigidbody2D>().MovePosition(bowOffset + new Vector2(tf.position.x, tf.position.y));
        //knife.GetComponent<Rigidbody2D>().MovePosition(knifeOffset + new Vector2(tf.position.x, tf.position.y));
    }

    void handleAttacks() {
        //handle knife swinging
        if (swinging) {
            if (timeSinceLastAttack < swingDuration) {
                currSwingDistance = Mathf.Lerp(0, swingDistance, timeSinceLastAttack / swingDuration);

                knife.GetComponent<BoxCollider2D>().enabled = true;
            }
            else if (timeSinceLastAttack < 2 * swingDuration){
                currSwingDistance = Mathf.Lerp(swingDistance, 0, (timeSinceLastAttack - swingDuration) / swingDuration);

                knife.GetComponent<BoxCollider2D>().enabled = false;
            }
            else{
                swinging = false;
                currSwingDistance = 0;

                knife.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (knifeActive) {
                if (timeSinceLastAttack > knifeCooldown) {
                    timeSinceLastAttack = 0;
                    swinging = true;
                    knife.GetComponents<AudioSource>()[0].Play();

                }

            }
            else if (timeSinceLastAttack > bowCooldown) {
                bowActive = false;
                knifeActive = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            if (bowActive) {
                if (timeSinceLastAttack > bowCooldown) {
                    GameObject spawnedArrow = GameObject.Instantiate(arrow);
                    spawnedArrow.GetComponent<Transform>().position = bow.GetComponent<Transform>().position + bow.GetComponent<Transform>().localPosition;
 
                    Vector3 newAngles = new Vector3(
                        bow.GetComponent<Transform>().eulerAngles.x,
                        bow.GetComponent<Transform>().eulerAngles.y,
                        bow.GetComponent<Transform>().eulerAngles.z - 90);

                    spawnedArrow.GetComponent<Transform>().eulerAngles = newAngles;

                    Vector2 movementVector = new Vector2(Mathf.Cos((newAngles.z + 90) * Mathf.Deg2Rad), Mathf.Sin((newAngles.z + 90) * Mathf.Deg2Rad));
                    spawnedArrow.GetComponent<ArrowController>().normalizedMovementVector = movementVector;

                    timeSinceLastAttack = 0;

                    bow.GetComponents<AudioSource>()[0].Play();
                }
            }
            else if (timeSinceLastAttack > knifeCooldown) {
                bowActive = true;
                knifeActive = false;
            }
        }
    }

    void movementHandle() {
        float dt = Time.fixedDeltaTime;

        Vector2 movementVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) {
            movementVector += new Vector2(0, speed * dt);
        }
        if (Input.GetKey(KeyCode.A)) {
            movementVector += new Vector2(-speed * dt, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.S)) {
            movementVector += new Vector2(0, -speed * dt);
        }
        if (Input.GetKey(KeyCode.D)) {
            movementVector += new Vector2(speed * dt, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        rb.MovePosition(rb.position + movementVector);
        //tf.position = tf.position + movementVector;
        //rb.velocity = movementVector;
    }

    void unfixedMovementHandle() {
        float dt = Time.deltaTime;

        Vector2 movementVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) {
            movementVector += new Vector2(0, speed);
        }
        if (Input.GetKey(KeyCode.A)) {
            movementVector += new Vector2(-speed, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            movementVector += new Vector2(0, -speed);
        }
        if (Input.GetKey(KeyCode.D)) {
            movementVector += new Vector2(speed, 0);
        }

        //rb.MovePosition(rb.position + movementVector);
        //tf.position = tf.position + movementVector;
        rb.velocity = movementVector;
    }
    public void enterKitchen() {
        inKitchen = true;
        Debug.Log("kitchen entered");
        dungeonGenerator.GetComponent<DungeonGenerator>().DestroyDungeon();
    }

    public void enterDungeon() {
        inKitchen = false;
        Debug.Log("dungeon entered");
        dungeonGenerator.GetComponent<DungeonGenerator>().GenerateDungeon();
    }

    public static void plateExpiredHook() {
        Debug.Log("Expired!");
    }

    void OnCollisionStay2D(Collision2D col) {
        float dt = Time.deltaTime;
        if (col.gameObject.tag == enemyTag) {
            if (timeSinceLastHit > iFramesDuration) {
                takeDamage(col.gameObject.GetComponent<EnemyTemplate>().contactDamage);
            }
        }

        if (col.gameObject.tag == "Drop") {
            string droptype = col.gameObject.GetComponent<DropController>().dropType;
            InventoryController.addDrop(droptype);
            col.gameObject.GetComponent<DropController>().collected();
        }

        if (col.gameObject.tag == "Wall") {

        }
    }

    private void OnTriggerEnter2D(Collider2D col) {

    }
}
