using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress : MonoBehaviour
{
    [SerializeField]
    private int maxRooms = 20;

    [SerializeField]
    private int maxWallHeight = 10;
    [SerializeField]
    private int maxTowerHeight = 20;

    [SerializeField]
    private int maxRandomness = 5;

    [SerializeField]
    private int maxWeapons = 10;
    [SerializeField]
    private float towerPercentage = 0.2f;

    private Vector3 matricesPosition;

    private Object[] floorsWalls;
    private Object[] towerTiles;

    public GameObject roomSizeObject;

    public Transform parentTransform;

    private Vector3 basicRoomSize;
    // Start is called before the first frame update
    void Awake()
    {
        matricesPosition = roomSizeObject.transform.position;
        floorsWalls = Resources.LoadAll("Floors And Walls");
        towerTiles = Resources.LoadAll("Tower Tiles");
        basicRoomSize = roomSizeObject.GetComponent<BoxCollider>().bounds.size;
        CreateBase();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            matricesPosition = roomSizeObject.transform.position;
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
            CreateBase();
        }
    }

    public GameObject GetRandomRoom()
    {
        GameObject spawnedRoom = (GameObject)floorsWalls[Random.Range(0, floorsWalls.Length)];
        return spawnedRoom;
    }

    void SpawnRoom(Vector3 previousRoom, int i, int j)
    {
        GameObject room;
        if (i == 0 || j == 0 || i == maxRooms || j == maxRooms - 1)
        {
            room = Instantiate((GameObject)floorsWalls[0], parentTransform);
            room.transform.position = previousRoom;
            CreateFiller(previousRoom, floorsWalls, maxWallHeight);
        }
        else
        {
            room = Instantiate(GetRandomRoom(), parentTransform);
            if(Random.Range(0f,1f) <= towerPercentage && room.name == "NormalFloor(Clone)")
            {
                CreateFiller(previousRoom, towerTiles, maxTowerHeight);
            }
            room.transform.position = previousRoom;
        }
    }

    void CreateBase()
    {
        for (int i = 0; i <= maxRooms; i++)
        {
            for (int j = 0; j < maxRooms; j++)
            {
                SpawnRoom(matricesPosition, i, j);
                matricesPosition.z += basicRoomSize.z;
            }
            matricesPosition.z = roomSizeObject.transform.position.z;
            matricesPosition.x += basicRoomSize.x;
        }
    }

    void CreateFiller(Vector3 fillPosStart, Object[] fillObjects, int maxHeight)
    {
        int randomVariation = Random.Range(0, maxRandomness);
        Vector3 currentFillPosition;
        currentFillPosition = fillPosStart + new Vector3(0, basicRoomSize.y, 0);
        GameObject filler;
        for(int i = 0; i <= maxHeight - randomVariation; i++)
        {
            filler = Instantiate((GameObject)fillObjects[0], parentTransform);
            filler.transform.position = currentFillPosition;
            currentFillPosition.y += basicRoomSize.y;
        }
    }
}
