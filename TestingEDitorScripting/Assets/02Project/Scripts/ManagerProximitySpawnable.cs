using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerProximitySpawnable : MonoBehaviour {

    #region Inspector
    [Header("Dependencies")]
    public GameObject targetCamera;

    [Header("Objects To Spawn")]
    public proxyObject[] spawnObjects;

    [Header("Parameters")]
    [Range(0f,100f)]
    public float focusChangeRequired;
    [Range(0,200)]
    public int minObjects;
    [Range(0f,500f)]
    public float rangeFromFocusSpawnMin;
    [Range(0f,500f)]
    public float rangeFromFocusSpawnMax;
    [Range(0f,1000f)]
    public float rangeFromFocusRemove;

    [Header("Initial Spawn Parameters")]
    [Range(0,100)]
    public int spawnMinObjects;
    [Range(0f,100f)]
    public float spawnRangeFromFocusMin;
    [Range(0f,100f)]
    public float spawnRangeFromFocusMax;

    [Header("Variants")]
    public bool useRandomScaling;
    [Range(-2.0f,2.0f)]
    public float minScale;
    [Range(-2.0f,2.0f)]
    public float maxScale;
    public bool useRandomRotation;
    [Range(0,2000)]
    public int minOrderInLayer;
    [Range(0,2000)]
    public int maxOrderInLayer;

    #endregion

    private Vector3 lastFocusPosition;


    void Start()
    {
        lastFocusPosition = targetCamera.transform.position;

        //Fill the pools
        foreach (proxyObject item in spawnObjects)
        {
            item.objectPool = new List<GameObject>();

            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.spawnObject);
                obj.SetActive(false);
				obj.transform.parent = gameObject.transform;
                item.objectPool.Add(obj);
            }
        }

        //Perform first spawn
        spawnProximityObjects(spawnMinObjects,spawnRangeFromFocusMin,spawnRangeFromFocusMax);

    }

    void Update()
    {
        if (Vector3.Distance(targetCamera.transform.position, lastFocusPosition) > focusChangeRequired)
        {

            //Find all child game objects
            var objects = new List<GameObject>();
            foreach (Transform child in transform) {
                if (child.gameObject.activeSelf == true) {
                    objects.Add(child.gameObject);
                }
            }

            var objectCount = objects.Count;

            //Check Distance of Existing Objects
            foreach (var obj in objects)
            {
                if (Vector3.Distance(targetCamera.transform.position, obj.transform.position) > rangeFromFocusRemove)
                {
                    obj.SetActive(false);
                    objectCount--;
                }
            }

            //Add new objects
            if (objectCount < minObjects) {
                spawnProximityObjects(minObjects,rangeFromFocusSpawnMin,rangeFromFocusSpawnMax);
            }

            //Set new position
            lastFocusPosition = targetCamera.transform.position;
        }

    }

    void spawnProximityObjects(int minimum, float minRange, float maxRange) {
        //Perform first spawn
        for (int i = 0; i < minimum; i++)
        {
            GameObject newObject = null;

            float totalWeighting = 0;
            foreach (proxyObject item in spawnObjects)
            {
                totalWeighting += item.weighting;
            }
            float randomObjectIndex = UnityEngine.Random.Range(0f, totalWeighting);
            float currentWeighting = 0;
            foreach (proxyObject item in spawnObjects)
            {
                if (randomObjectIndex >= currentWeighting && randomObjectIndex <= (currentWeighting + item.weighting)) {
                    newObject = GetPooledObject(item);
                    newObject.SetActive(true);
                }
                currentWeighting += item.weighting;
            }

            //Scale
            if (useRandomScaling == true)
            {
                float newScale = UnityEngine.Random.Range(minScale, maxScale);
                newObject.transform.localScale = new Vector3(newScale, newScale, 1f);
            }

            //Rotation
            if (useRandomRotation == true)
            {
                float newRotation = UnityEngine.Random.Range(0, 360);
                newObject.transform.Rotate(Vector3.forward * newRotation);
            }

            //Sorting Order
            SpriteRenderer noSpriteRender = newObject.GetComponent<SpriteRenderer>();
            if (noSpriteRender != null) {
                noSpriteRender.sortingOrder = UnityEngine.Random.Range(minOrderInLayer, maxOrderInLayer);
            }
            MeshRenderer noRender = newObject.GetComponent<MeshRenderer>();
            if (noRender != null) {
                noRender.sortingOrder = UnityEngine.Random.Range(minOrderInLayer, maxOrderInLayer);
            }

            //Position
            newObject.transform.position = UtilitiesSpaceMath.relativeRandomPositionInSpace(targetCamera.transform.position, minRange, maxRange);

            //Add to Parent
	        newObject.transform.parent = gameObject.transform;
	        
        }
    }

    GameObject GetPooledObject(proxyObject objGroup)
    {
        for (int i = 0; i < objGroup.objectPool.Count; i++)
        {
            if (!objGroup.objectPool[i].activeInHierarchy)
            {
                return objGroup.objectPool[i];
            }
        }

        if (objGroup.expandable)
        {
            GameObject obj = (GameObject)Instantiate(objGroup.spawnObject);
            obj.SetActive(false);
			obj.transform.parent = gameObject.transform;
            objGroup.objectPool.Add(obj);
            return obj;
        }
        return null;
    }

	
}

[Serializable]
public class proxyObject {
    public string uniqueParentName;
    public GameObject spawnObject;
    [Range(0f,10.0f)]
    public float weighting;
    [Header("Pooling")]
    public int poolSize;
    public bool expandable;
    public List<GameObject> objectPool;
}
