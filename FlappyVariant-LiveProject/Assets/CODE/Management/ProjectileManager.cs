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
        //[SerializeField] private GameObject projectilePrefab = null;
        //[SerializeField] private Transform poolParent = null;
        //[SerializeField] private int copies = 1;
        //[SerializeField] private string poolKey = " ";

        [SerializeField] private List<poolStructure> listOfPools = new List<poolStructure>();
        #endregion



        #region Private Variables
        private Dictionary<string, Queue<GameObject>> projectileReferences = new Dictionary<string, Queue<GameObject>>();
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            //if(projectilePrefab && poolParent)
            //{
            //    ObjectDuplicator.PopulateDictionary(ref projectileReferences, poolParent, projectilePrefab, copies, poolKey);
            //}


            if(listOfPools.Count > 0)
            {
                foreach(var pool in listOfPools)
                {
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