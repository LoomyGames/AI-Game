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
    [SerializeField]
    private int maxBarracks = 10;
    [SerializeField]
    private float barracksPercentage = 0.2f;
    [SerializeField]
    private float weaponsPercentage = 0.5f;
    [SerializeField]
    private float decorationsPercentage = 0.2f;

    [SerializeField]
    private int fortressStrength = 10;
    [SerializeField]
    private int fortressHealth = 10;
    [SerializeField]
    private int fortressPlanes = 10;

    private Vector3 matricesPosition;

    private Object[] floorsWalls;
    private Object[] towerTiles;
    private Object[] weaponsTiles;
    private Object[] barracksTiles;
    private Object[] wallDecorations;

    public GameObject roomSizeObject;

    public Transform parentTransform;

    private Vector3 basicRoomSize;

    private int currentWeapons = 0;
    private int currentBarracks = 0;
    // Start is called before the first frame update
    void Awake()
    {
        matricesPosition = roomSizeObject.transform.position;
        floorsWalls = Resources.LoadAll("Floors And Walls");
        towerTiles = Resources.LoadAll("Tower Tiles");
        weaponsTiles = Resources.LoadAll("Weapon Tiles");
        barracksTiles = Resources.LoadAll("Barracks Tiles");
        wallDecorations = Resources.LoadAll("Wall Decorations");
        basicRoomSize = roomSizeObject.GetComponent<Collider>().bounds.size;
        CreateBase();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            matricesPosition = roomSizeObject.transform.position;
            currentWeapons = 0;
            currentBarracks = 0;
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
            CreateBase();
        }*/
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
            room = Instantiate((GameObject)floorsWalls[2], parentTransform);
            room.transform.position = previousRoom;
            CreateWalls(previousRoom, maxWallHeight, i, j);
        }
        else
        {
            room = Instantiate(GetRandomRoom(), parentTransform);
            if(Random.Range(0f,1f) <= towerPercentage && room.name == "NormalFloor(Clone)")
            {
                CreateFiller(previousRoom, towerTiles, maxTowerHeight, "Towers");
            }
            else if (Random.Range(0f, 1f) <= barracksPercentage && (room.name == "BarracksBase(Clone)" || room.name == "BarracksBase2(Clone)"))
            {
                CreateFiller(previousRoom, barracksTiles, 4, "Barracks");
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

    void CreateFiller(Vector3 fillPosStart, Object[] fillObjects, int maxHeight, string fillType)
    {
        int randomVariation = Random.Range(0, maxRandomness);
        Vector3 currentFillPosition;
        currentFillPosition = fillPosStart + new Vector3(0, basicRoomSize.y, 0);
        GameObject filler;
        int i;
        for (i = 0; i <= maxHeight - randomVariation; i++)
        {
            filler = Instantiate((GameObject)fillObjects[Random.Range(0, fillObjects.Length)], parentTransform);
            filler.transform.position = currentFillPosition;
            currentFillPosition.y += basicRoomSize.y;

        }
        if (fillType == "Towers" && i == maxHeight - randomVariation + 1 && currentWeapons <= maxWeapons &&
            Random.Range(0f,1f) < weaponsPercentage)
        {
            filler = Instantiate((GameObject)weaponsTiles[Random.Range(0, weaponsTiles.Length)], parentTransform);
            filler.transform.position = currentFillPosition;
            currentWeapons++;
        }
    }

    void CreateWalls(Vector3 wallsPosStart, int maxHeight, int i, int j)
    {
        int randomVariation = Random.Range(0, maxRandomness);
        Vector3 currentWallPosition;
        currentWallPosition = wallsPosStart + new Vector3(0, basicRoomSize.y, 0);
        GameObject wall;
        for (int k = 0; k <= maxHeight - randomVariation; k++)
        {
            wall = Instantiate((GameObject)floorsWalls[2], parentTransform);
            wall.transform.position = currentWallPosition;
            if(Random.Range(0f,1f) <= decorationsPercentage)
            {
                CreateDecorations(currentWallPosition, i, j);
            }
            currentWallPosition.y += basicRoomSize.y;
        }
    }

    void CreateDecorations(Vector3 decoratedObjPos, int i, int j)
    {
        GameObject decoration;
        Vector3 currentDecoPosition = decoratedObjPos;
        if(j == 0)
        {
            currentDecoPosition += new Vector3(0, 0, -basicRoomSize.z);
        }
        else if(j == maxRooms - 1)
        {
            currentDecoPosition += new Vector3(0, 0, basicRoomSize.z);
        } 
        else if(i == 0)
        {
            currentDecoPosition += new Vector3(-basicRoomSize.x, 0, 0);
        }
        else if(i == maxRooms)
        {
            currentDecoPosition += new Vector3(basicRoomSize.x, 0, 0);
        }
        decoration = Instantiate((GameObject)wallDecorations[Random.Range(0, wallDecorations.Length)], parentTransform);
        decoration.transform.position = currentDecoPosition;
    }
}
