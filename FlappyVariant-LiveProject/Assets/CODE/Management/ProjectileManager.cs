using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Tools;

namespace Flappy.Management
{
    #region Component Overview
    /// <summary>
    /// [Keeps track of active and inactive projectiles in the level]
    /// </summary>
    #endregion

    public class ProjectileManager : MonoBehaviour
    {
        #region Public Variables

        [SerializeField] private List<poolStructure> listOfPools = new List<poolStructure>();
        #endregion



        #region Private Variables

        private Dictionary<string, Queue<GameObject>> projectileReferences = new Dictionary<string, Queue<GameObject>>();
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            // check if there is at least one entry in the listOfPools
            if(listOfPools.Count > 0)
            {
                // iterate through the list
                foreach(var pool in listOfPools)
                {
                    // call duplication function from the ObjectDuplicator and provide reference to dictionary data structure
                    ObjectDuplicator.PopulateDictionaryWithPoolStructure(ref projectileReferences, pool);
                }
            }
        }


        private void Update()
        {

        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods

        #endregion
    }
}