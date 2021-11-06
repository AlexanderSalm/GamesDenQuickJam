using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public static float TILE_SIZE = 0.5f;
    public List<GameObject> floors;
    public List<GameObject> walls;
    public List<GameObject> enemies;

    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        int dungeonWidth = Random.Range(1, 7);
        int dungeonHeight = Random.Range(1, 7);
        tf = GetComponent<Transform>();
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
                if (roomX != 0) {
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

                GenerateRoom(12, 12, tf.position + new Vector3((roomX) * (roomWidth + hallLength) * TILE_SIZE, (roomY) * (roomHeight + hallLength) * TILE_SIZE), doors, 3);

                if (roomX != dungeonWidth - 1) GenerateHoriHallway(hallLength, hallWidth, tf.position + new Vector3(
                    ((roomX) * (roomWidth + hallLength) * TILE_SIZE) + (roomWidth * TILE_SIZE), 
                    (roomY) * (roomHeight + hallLength) * TILE_SIZE + ((roomHeight-3)/2 * TILE_SIZE)));

                if (roomY != dungeonHeight - 1) GenerateVertHallway(hallWidth, hallLength, tf.position + new Vector3(
                    ((roomX) * (roomWidth + hallLength) * TILE_SIZE) + ((roomWidth-3)/2 * TILE_SIZE),
                    (roomY) * (roomHeight + hallLength) * TILE_SIZE + (roomHeight * TILE_SIZE)));
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

                }
                else {
                    int rand = Random.Range(0, walls.Count);
                    tile = Instantiate(walls[rand]);
                }
                tile.GetComponent<Transform>().position = position + new Vector3(x * TILE_SIZE, y * TILE_SIZE, tile.GetComponent<Transform>().position.z);

                
            }
        }

        GameObject enemy = enemies[Random.Range(0, enemies.Count)];

        for(int i = 0; i < numEnemies; i++) {
            float randX = Random.Range(1.0f, width - 1);
            float randY = Random.Range(1.0f, height - 1);
            GameObject spawnedEnemy = Instantiate(enemy);
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

                }
                else {
                    int rand = Random.Range(0, walls.Count);
                    tile = Instantiate(walls[rand]);
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

                }
                else {
                    int rand = Random.Range(0, walls.Count);
                    tile = Instantiate(walls[rand]);
                }
                tile.GetComponent<Transform>().position = position + new Vector3(x * TILE_SIZE, y * TILE_SIZE, tile.GetComponent<Transform>().position.z);
            }
        }
    }
}
