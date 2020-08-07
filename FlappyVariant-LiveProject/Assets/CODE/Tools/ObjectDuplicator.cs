using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Tools
{
    // Case Uses in this project:
    // Obstacles
    // Projectiles
    // Particle Systems

    [System.Serializable]
    public class poolStructure
    {
        public GameObject prefab = null;
        public Transform parent = null;
        public int copies = 1;
        public string poolKey = "";
    }


    #region Component Overview
    /// <summary>
    /// [Will duplicate provided objects and store them in provided data structure]
    /// </summary>
    #endregion


    public static class ObjectDuplicator
    {
        #region Public Variables
        //[SerializeField] private
        #endregion

        #region Private Variables
        //private 
        #endregion

        #region Custom Methods
        public static void PopulateDictionary(ref Dictionary<string, Queue<GameObject>> dataStructure, Transform parent, GameObject objToDuplicate, int numberOfCopies, string key)
        {
            Queue<GameObject> tempQueue = new Queue<GameObject>();

            for (int i = 0; i < numberOfCopies; i++)
            {
                GameObject tempObj = GameObject.Instantiate(objToDuplicate);

                tempObj.name = key + " " + (i + 1);
                tempObj.transform.parent = parent;
                tempObj.SetActive(false);

                tempQueue.Enqueue(tempObj);
            }

            dataStructure.Add(key, tempQueue);
        }

        public static void PopulateDictionaryWithPoolStructure(ref Dictionary<string, Queue<GameObject>> dataStructure, poolStructure pool)
        {
            Queue<GameObject> tempQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.copies; i++)
            {
                GameObject tempObj = GameObject.Instantiate(pool.prefab);

                tempObj.name = pool.poolKey + " " + (i + 1);
                tempObj.transform.parent = pool.parent;
                tempObj.SetActive(false);

                tempQueue.Enqueue(tempObj);
            }

            dataStructure.Add(pool.poolKey, tempQueue);
        }

        #endregion
    }
}