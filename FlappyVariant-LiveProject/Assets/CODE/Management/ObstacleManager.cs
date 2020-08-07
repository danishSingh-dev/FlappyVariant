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
        //[SerializeField] private GameObject obstaclePrefab = null;
        //[SerializeField] private Transform poolParent = null;
        //[SerializeField] private int copies = 1;
        //[SerializeField] private string poolKey = " ";

        [SerializeField] private List<poolStructure> listOfPools = new List<poolStructure>();

        [SerializeField] private Transform spawnPosition = null;
        [SerializeField] private float spawnRate = 2f;
        [SerializeField] private float timer = 0f;
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
            timer += Time.deltaTime;

            //ActivateNextObstacle(poolKey);
        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods


        void ActivateNextObstacle(string key)
        {
            if(timer < spawnRate) { return; }
            if (!obstacleReferences.ContainsKey(key)) 
            {
                Debug.LogError("There is no key-value pair that uses this key: " + key);
                return; 
            }

            
            int count = 1; 
            
            
            for (int i = 0; i < obstacleReferences[key].Count; i++)
            {
                if(count == obstacleReferences[key].Count)
                {
                    Debug.Log("All obstacles are currently active");
                    break;
                }

                GameObject tempObj = obstacleReferences[key].Dequeue();

                if (tempObj.activeSelf)
                {
                    obstacleReferences[key].Enqueue(tempObj);
                    count++;
                    continue;
                }
                else
                {
                    tempObj.SetActive(true);
                    tempObj.transform.localPosition = spawnPosition.localPosition;
                    obstacleReferences[key].Enqueue(tempObj);
                    timer = 0f;
                    
                    break;
                }

            }


        }
        #endregion
    }
}