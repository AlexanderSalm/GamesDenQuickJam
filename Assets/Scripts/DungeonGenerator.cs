using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public static float TILE_SIZE = 0.5f;
    public List<GameObject> floors;
    public List<GameObject> walls;
    public List<GameObject> enemies;

    public List<GameObject> woodFloors;

    private Transform tf;
    // Start is called before the first frame update
    private void Start() {
        tf = GetComponent<Transform>();
        //GenerateDungeon();
        //GenerateKitchen();
    }
    public void GenerateDungeon()
    {
        int dungeonWidth = Random.Range(1, 5);
        int dungeonHeight = Random.Range(1, 5);
        
        int roomWidth = 12;
        int roomHeight = 12;
        int hallWidth = 4;
        int hallLength = 16;
        for (int roomX = 0; roomX < dungeonWidth; roomX++) {
            for (int roomY = 0; roomY < dungeonHeight; roomY++) {
                Debug.Log("loop");
                List<Vector2> doors = new List<Vector2>();
                //Top wall
                if (roomY != 0) {
                    doors.Add(new Vector2(5, 0));
                    doors.Add(new Vector2(6, 0));
                    doors.Add(new Vector2(7, 0));
                }

                //Bottom wall
                if (roomY != dungeonHeight - 1) {
                    doors.Add(new Vector2(5, 12));
                    doors.Add(new Vector2(6, 12));
                    doors.Add(new Vector2(7, 12));
                }

                //Left wall
                if (roomX != 0 || (roomX == 0 && roomY == 0)) {
                    doors.Add(new Vector2(0, 6));
                    doors.Add(new Vector2(0, 7));
                    doors.Add(new Vector2(0, 5));
                }

                //Right wall
                if (roomX != dungeonWidth - 1) {
                    doors.Add(new Vector2(12, 6));
                    doors.Add(new Vector2(12, 7));
                    doors.Add(new Vector2(12, 5));
                }

                int enemies = Random.Range(1, 6);

                GenerateRoom(12, 12, tf.position + new Vector3((roomX) * (roomWidth + hallLength) * TILE_SIZE, (roomY) * (roomHeight + hallLength) * TILE_SIZE), doors, enemies);

                if (roomX != dungeonWidth - 1) GenerateHoriHallway(hallLength, hallWidth, tf.position + new Vector3(
                    ((roomX) * (roomWidth + hallLength) * TILE_SIZE) + (roomWidth * TILE_SIZE), 
                    (roomY) * (roomHeight + hallLength) * TILE_SIZE + ((roomHeight-3)/2 * TILE_SIZE)));

                if (roomY != dungeonHeight - 1) GenerateVertHallway(hallWidth, hallLength, tf.position + new Vector3(
                    ((roomX) * (roomWidth + hallLength) * TILE_SIZE) + ((roomWidth-3)/2 * TILE_SIZE),
                    (roomY) * (roomHeight + hallLength) * TILE_SIZE + (roomHeight * TILE_SIZE)));
            }
        }
    }

    void GenerateKitchen() {
        for(int x = 0; x < 20; x++) {
            for(int y = 0; y < 20; y++) {
                int rand = Random.Range(0, woodFloors.Count);
                GameObject tile = Instantiate(woodFloors[rand]);
                tile.GetComponent<Transform>().position = new Vector3(-36.5f, 0) + new Vector3(x * TILE_SIZE, y * TILE_SIZE, tile.GetComponent<Transform>().position.z);
        }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateRoom(int width, int height, Vector3 position, List<Vector2> doors, int numEnemies) {
        for (int x = 0; x < width + 1; x++) {
            for (int y = 0; y < height + 1; y++) {
                GameObject tile;
                if (((x < width && x > 0 && y < height && y > 0) || doors.Contains(new Vector2(x, y)))) {
                    int rand = Random.Range(0, floors.Count);
                    tile = Instantiate(floors[rand]);
                    tile.GetComponent<Transform>().parent = tf;

                }
                else {
                    int rand = Random.Range(0, walls.Count);
                    tile = Instantiate(walls[rand]);
                    tile.GetComponent<Transform>().parent = tf;
                }
                tile.GetComponent<Transform>().position = position + new Vector3(x * TILE_SIZE, y * TILE_SIZE, tile.GetComponent<Transform>().position.z);

                
            }
        }

        GameObject enemy = enemies[Random.Range(0, enemies.Count)];

        for(int i = 0; i < numEnemies; i++) {
            float randX = Random.Range(1.0f, width - 1);
            float randY = Random.Range(1.0f, height - 1);
            GameObject spawnedEnemy = Instantiate(enemy);
            spawnedEnemy.GetComponent<Transform>().parent = tf;
            spawnedEnemy.GetComponent<Transform>().position = position + new Vector3(randX * TILE_SIZE, randY * TILE_SIZE, 0);
        }
    }

    void GenerateVertHallway(int width, int height, Vector3 position) {
        for(int x = 0; x < width + 1; x++) {
            for(int y = 0; y < height; y++) {
                GameObject tile;
                if (x < width && x > 0) {
                    int rand = Random.Range(0, floors.Count);
                    tile = Instantiate(floors[rand]);
                    tile.GetComponent<Transform>().parent = tf;

                }
                else {
                    int rand = Random.Range(0, walls.Count);
                    tile = Instantiate(walls[rand]);
                    tile.GetComponent<Transform>().parent = tf;
                }
                tile.GetComponent<Transform>().position = position + new Vector3(x * TILE_SIZE, y * TILE_SIZE, tile.GetComponent<Transform>().position.z);
            }
        }
    }

    void GenerateHoriHallway(int width, int height, Vector3 position) {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height + 1; y++) {
                GameObject tile;
                if (y < height && y > 0) {
                    int rand = Random.Range(0, floors.Count);
                    tile = Instantiate(floors[rand]);
                    tile.GetComponent<Transform>().parent = tf;

                }
                else {
                    int rand = Random.Range(0, walls.Count);
                    tile = Instantiate(walls[rand]);
                    tile.GetComponent<Transform>().parent = tf;
                }
                tile.GetComponent<Transform>().position = position + new Vector3(x * TILE_SIZE, y * TILE_SIZE, tile.GetComponent<Transform>().position.z);
            }
        }
    }

    public void DestroyDungeon() {
        foreach(Transform child in tf) {
            Destroy(child.gameObject);
        }
    }
}
