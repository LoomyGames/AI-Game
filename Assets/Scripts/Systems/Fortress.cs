using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress : MonoBehaviour
{
    [SerializeField]
    private int maxRooms = 20;

    [SerializeField]
    private int maxWallsHeight = 10;

    [SerializeField]
    private int maxRandomness = 5;

    [SerializeField]
    private int maxWeapons = 10;

    private Vector3 matricesPosition;

    private Object[] possibleRooms;

    public GameObject roomSizeObject;

    public Transform parentTransform;

    private Vector3 basicRoomSize;
    // Start is called before the first frame update
    void Awake()
    {
        matricesPosition = roomSizeObject.transform.position;
        possibleRooms = Resources.LoadAll("Rooms");
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
        GameObject spawnedRoom = (GameObject)possibleRooms[Random.Range(0, possibleRooms.Length)];
        return spawnedRoom;
    }

    void SpawnRoom(Vector3 previousRoom, int i, int j)
    {
        GameObject room;
        if (i == 0 || j == 0 || i == maxRooms || j == maxRooms - 1)
        {
            room = Instantiate((GameObject)possibleRooms[0], parentTransform);
            room.transform.position = previousRoom;
            CreateWalls(previousRoom);
            //ADD EXTERIOR WALLS BEHAVIOR TOO

        }
        else
        {
            room = Instantiate(GetRandomRoom(), parentTransform);
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

    void CreateWalls(Vector3 wallPosStart)
    {
        Vector3 currentWallPosition;
        currentWallPosition = wallPosStart + new Vector3(0, basicRoomSize.y, 0);
        GameObject wall;
        for(int i = 0; i <= maxWallsHeight - Random.Range(0, maxRandomness); i++)
        {
            wall = Instantiate((GameObject)possibleRooms[0], parentTransform);
            wall.transform.position = currentWallPosition;
            currentWallPosition.y += basicRoomSize.y;
        }
    }
}
