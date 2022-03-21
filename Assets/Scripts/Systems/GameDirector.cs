using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    Object[] bigBuildings;
    Object[] resourceBuildings;
    Object[] landingPlaces;
    Object[] objectivePlaces;
    Object[] centerPlaces;

    public Transform currentIsland;
    //Transform[] islandComponents;
    // Start is called before the first frame update
    void Start()
    {
        bigBuildings = Resources.LoadAll("Big Buildings", typeof (GameObject));
        resourceBuildings = Resources.LoadAll("Resource Buildings", typeof(GameObject));
        landingPlaces = Resources.LoadAll("Landing Places", typeof(GameObject));
        objectivePlaces = Resources.LoadAll("Objective Places", typeof(GameObject));
        centerPlaces = Resources.LoadAll("Center Places", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnIslandSpawn()
    {
        foreach (Transform child in currentIsland)
        {
            AddRandomBuilding(child);
            //Debug.Log(child.name);
            Debug.Log(currentIsland.name);
        }

    }

    void AddRandomBuilding(Transform isComponent)
    {
        GameObject instantiatedItem;
        if(isComponent.name == "BigBuilding")
        {
            int j = Random.Range(0, bigBuildings.Length);
            instantiatedItem = (GameObject)bigBuildings[j];
            GameObject thing = Instantiate(instantiatedItem, isComponent);
            Transform SpawnPosition = thing.transform.Find("SpawnPos");
            thing.transform.position = CalculatePosition(thing.transform, SpawnPosition);
            
        } 
        else if (isComponent.name == "ResourceBuilding")
        {
            int j = Random.Range(0, resourceBuildings.Length);
            instantiatedItem = (GameObject)resourceBuildings[j];
            GameObject thing = Instantiate(instantiatedItem, isComponent);
            Transform SpawnPosition = thing.transform.Find("SpawnPos");
            thing.transform.position = CalculatePosition(thing.transform, SpawnPosition);
        }
        else if (isComponent.name == "LandingPlace")
        {
            int j = Random.Range(0, landingPlaces.Length);
            instantiatedItem = (GameObject)landingPlaces[j];
            GameObject thing = Instantiate(instantiatedItem, isComponent);
            //Transform SpawnPosition = thing.transform.Find("SpawnPos");
            //thing.transform.position = CalculatePosition(thing.transform, SpawnPosition);

        }
        else if (isComponent.name == "ObjectivePlace")
        {
            int j = Random.Range(0, objectivePlaces.Length);
            instantiatedItem = (GameObject)objectivePlaces[j];
            GameObject thing = Instantiate(instantiatedItem, isComponent);
            Transform SpawnPosition = thing.transform.Find("SpawnPos");
            thing.transform.position = CalculatePosition(thing.transform, SpawnPosition);
        }
        else if (isComponent.name == "CenterPlace")
        {
            int j = Random.Range(0, centerPlaces.Length);
            instantiatedItem = (GameObject)centerPlaces[j];
            GameObject thing = Instantiate(instantiatedItem, isComponent);
            Transform SpawnPosition = thing.transform.Find("SpawnPos");
            thing.transform.position = CalculatePosition(thing.transform, SpawnPosition);
        }
    }

    Vector3 CalculatePosition(Transform obj, Transform obj2)
    {
        Vector3 position = new Vector3(obj.position.x, obj.position.y - obj2.position.y, obj.position.z);
        Debug.Log("This is the calculated Y spawn position of " + obj.name + ": " + position.y);
        return position;
    }
}
