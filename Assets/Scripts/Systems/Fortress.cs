using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress : MonoBehaviour
{
    [SerializeField]
    private int maxRooms = 20; //the amount of rows and collumns the floor of the fortress has (named rooms but they are just tiles)

    [SerializeField]
    private int maxWallHeight = 10; //maximum height of the walls and the towers
    [SerializeField]
    private int maxTowerHeight = 20;

    [SerializeField]
    private int maxRandomness = 5; //the maximum amount of decrease in height of the walls and towers

    [SerializeField]
    private int maxWeapons = 10; //maximum amount of actual weapon turrets
    [SerializeField]
    private float towerPercentage = 0.2f; // the chance of spawning towers, barrack tiles, weapons and wall decorations
    [SerializeField]
    private float barracksPercentage = 0.2f;
    [SerializeField]
    private float weaponsPercentage = 0.5f;
    [SerializeField]
    private float decorationsPercentage = 0.2f;

    public int fortressHealth = 0; //the health buff of the fortress

    public int fortressPlanes = 10; //the initial amount of airplanes that the fortress spawns

    private Vector3 matricesPosition; //the current position where the floor tile is spawned (which has i and j as coords)

    private Object[] floorsWalls; //arrays which contain the resources used to generate the content
    private Object[] towerTiles;
    private Object[] weaponsTiles;
    private Object[] barracksTiles;
    private Object[] wallDecorations;
    private Object[] planePrefabs;

    public GameObject roomSizeObject; //the reference size of any tile's maximum bounds
    private GameDirector gameDirector; //the game director

    public Transform parentTransform; //the transform under which the fortress will be created (separated from the game manager)

    private Vector3 basicRoomSize; //the size values of roomSizeObject (the height, width and depth)

    private int currentWeapons = 0; //the current number of weapon tiles

    public bool isComplete = false; //is the fortress finished? (are the planes dead)
    public bool isPlane = false; //is there a plane instantiated? 

    public GameObject currentPlane; //the currently instantiated plane
    // Start is called before the first frame update
    void Awake()
    {
        matricesPosition = roomSizeObject.transform.position; //start the object placement at the initial position
        gameDirector = GameObject.FindWithTag("GameManager").GetComponent<GameDirector>(); //fill the arrays by loading resources
        floorsWalls = Resources.LoadAll("Floors And Walls");
        towerTiles = Resources.LoadAll("Tower Tiles");
        weaponsTiles = Resources.LoadAll("Weapon Tiles");
        barracksTiles = Resources.LoadAll("Barracks Tiles");
        wallDecorations = Resources.LoadAll("Wall Decorations");
        planePrefabs = Resources.LoadAll("Planes");
        basicRoomSize = roomSizeObject.GetComponent<Collider>().bounds.size; //get the size of the base object
        AdjustFortressSettings(); //set the values for the fortress based on the game director
        CreateBase(); //create the fortress
    }

    // Update is called once per frame
    void Update()
    {
        if(fortressPlanes == 0) //if there are no planes left, the fortress is finished
        {
            isComplete = true;
        }

        if (!isPlane && fortressPlanes > 0) //if there is no plane and there are still planes left to spawn
        {
            currentPlane = Instantiate((GameObject)planePrefabs[Random.Range(0, planePrefabs.Length)]); //create a random plane
            currentPlane.transform.position = transform.position + new Vector3(Random.Range(0, 100f), 
                Random.Range(maxTowerHeight * 5, maxTowerHeight * 5 + 40f), Random.Range(0, 100f)); // set it above the fortress
            isPlane = true;
        }
    }

    public GameObject GetRandomRoom() //get a random room (now named tile) for the floor and walls of the fortress
    {
        GameObject spawnedRoom = (GameObject)floorsWalls[Random.Range(0, floorsWalls.Length)];
        return spawnedRoom;
    }

    void SpawnRoom(Vector3 previousRoom, int i, int j) //method for creating the walls, towers and barracks inside the fortress
    {
        GameObject room;
        if (i == 0 || j == 0 || i == maxRooms || j == maxRooms - 1) //if the matrices iterates and creates tiles on the edges of the map
        {                                                           
            room = Instantiate((GameObject)floorsWalls[2], parentTransform);//create a specific floor tile (same as the wall material)
            room.transform.position = previousRoom; //place the room on the spawn location
            CreateWalls(previousRoom, maxWallHeight, i, j); //create the wall for that specific tile
        }
        else //otherwise spawn the normal fillers inside the fortress
        {
            room = Instantiate(GetRandomRoom(), parentTransform); //instantiate a random floor tile (different colors) 
            if(Random.Range(0f,1f) <= towerPercentage && room.name == "NormalFloor(Clone)") //if the tile is green take the chance to
            {                                                                               //spawn a tower 
                CreateFiller(previousRoom, towerTiles, maxTowerHeight, "Towers"); //create the tower
            }
            else if (Random.Range(0f, 1f) <= barracksPercentage && (room.name == "BarracksBase(Clone)" || room.name == "BarracksBase2(Clone)"))
            { //otherwise if the tile is a barracks specific tile take the chance to spawn a barracks tile
                fortressHealth++; //increase the health buff for each barracks tile 
                CreateFiller(previousRoom, barracksTiles, 4, "Barracks"); //create the barracks
            }

            room.transform.position = previousRoom; //set the created tile position to the adequate position in the floor
        }
    }

    void CreateBase() //create the base (floor) of the fortress
    {
        for (int i = 0; i <= maxRooms; i++) //run through the collumns of the base
        {
            for (int j = 0; j < maxRooms; j++) //run through the rows of the base
            {
                SpawnRoom(matricesPosition, i, j); //spawn a tile on each position in the matrices
                matricesPosition.z += basicRoomSize.z; //increase the z position (which is equivalent to increasing j in the matrices)
            }
            matricesPosition.z = roomSizeObject.transform.position.z;//reset the z position (j = 0)
            matricesPosition.x += basicRoomSize.x; // move to the next collumn (to the right) which means the i value increases
        }
    }

    void CreateFiller(Vector3 fillPosStart, Object[] fillObjects, int maxHeight, string fillType) 
    { //method for creating the towers, barrracks and weapons 
        int randomVariation = Random.Range(0, maxRandomness); //the random height adjustment 
        Vector3 currentFillPosition; //the current position where the fill needs to go 
        currentFillPosition = fillPosStart + new Vector3(0, basicRoomSize.y, 0); //increase the position to be above the floor
        GameObject filler; //the object that will fill the spot
        int i;
        for (i = 0; i <= maxHeight - randomVariation; i++) //run through and instantiate the object based on a random height
        {
            filler = Instantiate((GameObject)fillObjects[Random.Range(0, fillObjects.Length)], parentTransform);
            filler.transform.position = currentFillPosition; //set the object's position to the current fill spot
            currentFillPosition.y += basicRoomSize.y; //set the current fill spot upwards for the next object

        }
        if (fillType == "Towers" && i == maxHeight - randomVariation + 1 && currentWeapons <= maxWeapons &&
            Random.Range(0f,1f) < weaponsPercentage) //if the fill is set to towers, then a weapon can also be placed on top, based on 
        {                                            //chance and the amount of weapons already present
            filler = Instantiate((GameObject)weaponsTiles[Random.Range(0, weaponsTiles.Length)], parentTransform);
            filler.transform.position = currentFillPosition; //just instantiate a weapon on top of the last filler
            currentWeapons++; //increase the number of weapons currently active
        }
    }

    void CreateWalls(Vector3 wallsPosStart, int maxHeight, int i, int j) //method used for creating the walls specifically
    {
        int randomVariation = Random.Range(0, maxRandomness); //again, get a random value that will be deducted from the height of each wall
        Vector3 currentWallPosition; //the current position of the selected tile (floor)
        currentWallPosition = wallsPosStart + new Vector3(0, basicRoomSize.y, 0); //increase that position to above the floor 
        GameObject wall; //the wall tile to be created
        for (int k = 0; k <= maxHeight - randomVariation; k++) //loop that runs through and creates a wall with a height based on the
        {                                                      //max height minus the random variation
            wall = Instantiate((GameObject)floorsWalls[2], parentTransform);
            wall.transform.position = currentWallPosition; //create the wall and set its location to the selected empty spot
            if(Random.Range(0f,1f) <= decorationsPercentage) //take the chance of adding a random decoration to the wall too 
            {
                CreateDecorations(currentWallPosition, i, j);
            }
            currentWallPosition.y += basicRoomSize.y; //increase the position to allow for the creation of the next wall tile
        }
    }

    void CreateDecorations(Vector3 decoratedObjPos, int i, int j) //method for creating the wall's outside decoration
    {
        GameObject decoration; //the decoration to be created
        Vector3 currentDecoPosition = decoratedObjPos; //the location of the wall it is created on
        if(j == 0) //logic for deciding what side of the wall the decoration spawns so it's always on the outside of the fortress
        {
            currentDecoPosition += new Vector3(0, 0, -basicRoomSize.z); //the bottom wall line
        }
        else if(j == maxRooms - 1)
        {
            currentDecoPosition += new Vector3(0, 0, basicRoomSize.z); // the top wall line
        } 
        else if(i == 0)
        {
            currentDecoPosition += new Vector3(-basicRoomSize.x, 0, 0); //the left wall line
        }
        else if(i == maxRooms)
        {
            currentDecoPosition += new Vector3(basicRoomSize.x, 0, 0); //the right wall line
        }
        decoration = Instantiate((GameObject)wallDecorations[Random.Range(0, wallDecorations.Length)], parentTransform);
        decoration.transform.position = currentDecoPosition; //create the decoration and set its location
    }

    void AdjustFortressSettings() //get all of the settings of the fortress based on the game director 
    {
        gameDirector.GetPlayerInfo(); //update the game director's information 
        maxRooms = (int)(gameDirector.playerHealth / 2.5f); //set the fortress parameters based on the player's current stats
        weaponsPercentage = (float)gameDirector.playerKills / 20;
        maxWeapons = gameDirector.playerKills;
        towerPercentage = (float)maxRooms / 300;
        barracksPercentage = (float)gameDirector.playerAmmo / 3000;
        decorationsPercentage = (float)gameDirector.playerHealth / 300;
        fortressHealth += gameDirector.playerHealth;
        fortressPlanes = gameDirector.playerHealth / 10;
    }
}
