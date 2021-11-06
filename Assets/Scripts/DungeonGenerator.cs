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
        tf = GetComponent<Transform>();
        List<Vector2> doors = new List<Vector2>();
        doors.Add(new Vector2(7, 8));
        doors.Add(new Vector2(8, 8));
        doors.Add(new Vector2(9, 8));
        doors.Add(new Vector2(10, 8));

        doors.Add(new Vector2(12, 4));
        doors.Add(new Vector2(12, 3));
        GenerateRoom(12, 8, tf.position, doors, 3);
        GenerateVertHallway(5, 12, tf.position + new Vector3(6 * TILE_SIZE, 8 * TILE_SIZE));
        GenerateHoriHallway(12, 3, tf.position + new Vector3(12 * TILE_SIZE, 2 * TILE_SIZE));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRoom(int width, int height, Vector3 position, List<Vector2> doors, int numEnemies) {
        for (int x = 0; x < width + 1; x++) {
            for (int y = 0; y < height + 1; y++) {
                if (!doors.Contains(new Vector2(x, y))) {
                    GameObject tile;
                    if (x < width && x > 0 && y < height && y > 0) {
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
