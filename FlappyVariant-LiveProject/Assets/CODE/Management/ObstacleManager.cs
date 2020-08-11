using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Tools;

namespace Flappy.Management
{



    #region Component Overview
    /// <summary>
    /// [Keeps track of active and inactive obstacles in the level]
    /// </summary>
    #endregion

    public class ObstacleManager : MonoBehaviour
    {
        #region Public Variables

        [SerializeField] private List<poolStructure> listOfPools = new List<poolStructure>();

        [Header("Spawn Parameters")]
        [SerializeField] private Transform spawnPosition = null;
        [SerializeField] private float spawnRate = 2f;
        [SerializeField] private float timer = 0f;

        [Header("Position Limits")]
        [SerializeField] private float minYPosition = -1f;
        [SerializeField] private float maxYPosition = 1f;

        [Header("Scale Limits")]
        [SerializeField] private float minYScale = 0f;
        [SerializeField] private float maxYScale = 1f;
        #endregion



        #region Private Variables
        private Dictionary<string, Queue<GameObject>> obstacleReferences = new Dictionary<string, Queue<GameObject>>();
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            if(listOfPools.Count > 0)
            {
                foreach(var pool in listOfPools)
                {
                    ObjectDuplicator.PopulateDictionaryWithPoolStructure(ref obstacleReferences, pool);
                }
            }
        }


        private void Update()
        {
            IterateSpawnTimer();
        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods

        void IterateSpawnTimer()
        {
            // check if timer value is less than the spawnRate
            if(timer < spawnRate)
            {
                // if true, add deltaTime 
                timer += Time.deltaTime;
            }
            else
            {
                // else, begin the activation process
                BeginActivationProcess();
            }
        }

        private void BeginActivationProcess()
        {
            // check if listOfPools has at least one entry
            if(listOfPools.Count > 0)
            {
                // create a null gameObject reference
                GameObject tempObj = null;
                
                
                
                // TODO refactor to randomize obstacle selection
                // iterate through listOfPools
                foreach(var pool in listOfPools)
                {
                    // set the tempObj reference by running the ReturnNextInActiveObjectInPool function
                    tempObj = ReturnNextInactiveObjectInPool(pool.poolKey);

                    // check if tempObj is null
                    if(tempObj == null)
                    {
                        // if true, iterate to the next pool of obstacles
                        continue;
                    }
                    // check if tempObj is not null
                    else if(tempObj != null)
                    {
                        ActivateObstacle(tempObj);

                        // break out of loop
                        break;
                    }
                }
            }
            

            // reset timer to 0
            timer = 0f;
        }

        private void ActivateObstacle(GameObject obstacle)
        {
            obstacle.SetActive(true);

            ResetYPosition(obstacle);
            ResetYScale(obstacle);

            RandomizeYPosition(obstacle);
            RandomizeYScale(obstacle);
        }

        GameObject ReturnNextInactiveObjectInPool(string key)
        {

            // check to see obstacleReferences contains the provided key
            if (!obstacleReferences.ContainsKey(key))
            {
                Debug.LogError("There is no key-value pair that uses this key: " + key);
                return null;
            }

            // create a null gameObject reference
            GameObject tempObj = null;
            
            // iterate through the queue assigned to the provided key
            for (int i = 0; i < obstacleReferences[key].Count; i++)
            {
                // set the tempObj reference to the next object in queue
                tempObj = obstacleReferences[key].Dequeue();

                // add tempObj back into queue
                obstacleReferences[key].Enqueue(tempObj);
                
                // check if tempObj is currently active
                if (tempObj.activeSelf)
                {
                    // if true, set tempObj back to null
                    tempObj = null;

                    // iterate loop
                    continue;
                }
                // check if tempObj is currently inactive
                else if (!tempObj.activeSelf)
                {
                    // if true, return the tempObj
                    return tempObj;
                }
            }

            // if loop iteration completes and all obstacles were active, return a null object
            return null;
        }


        void RandomizeYPosition(GameObject obstacle)
        {
            // between 9 and -9
            float rnd = Random.Range(minYPosition, maxYPosition);

            Vector3 currentPosition = obstacle.transform.localPosition;

            Vector3 newPosition = new Vector3(currentPosition.x, rnd, currentPosition.z);

            obstacle.transform.localPosition = newPosition;
        }

        void RandomizeYScale(GameObject obstacle)
        {
            // between 1 and 2.5
            float rnd = Random.Range(minYScale, maxYScale);

            Vector3 currentScale = obstacle.transform.localScale;
            Vector3 newScale = Vector3.up;
            newScale *= rnd;

            newScale += currentScale;

            obstacle.transform.localScale = newScale;
        }

        void ResetYPosition(GameObject obstacle)
        {
            obstacle.transform.localPosition = spawnPosition.localPosition;
        }

        void ResetYScale(GameObject obstacle)
        {
            Vector3 currentScale = obstacle.transform.localScale;
            Vector3 newScale = new Vector3(currentScale.x, 1f, currentScale.z);

            obstacle.transform.localScale = newScale;
        }

        
        #endregion
    }
}