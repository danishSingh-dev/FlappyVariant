using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Tools;
using System;

namespace Flappy.Management
{
    // Once it receives a command to shoot, 
    // if it is readyToFire,
    // this component will find the next inactive gameObject in the pool, 
    // and will then spawn it at the firePoint location
    // else
    // it will iterate the timer



    #region Component Overview
    /// <summary>
    /// [Activate projectiles upon receiving a Shoot command]
    /// </summary>
    #endregion

    public class ProjectileManager : MonoBehaviour
    {
        #region Public Variables

        [SerializeField] private List<poolStructure> listOfPools = new List<poolStructure>();

        [Header("Shooting Parameters")]
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float timer = 0f;
        [SerializeField] private bool readyToFire = false;

        public bool ReadyToFire { get { return readyToFire; } }

        [Header("Singleton")]
        public static ProjectileManager PM_Instance = null;
        #endregion



        #region Private Variables
        private GameObject loadedProjectile = null;
        private Dictionary<string, Queue<GameObject>> projectileReferences = new Dictionary<string, Queue<GameObject>>();
        #endregion



        #region MonoBehaviour Methods
        private void Awake()
        {
            // Check if singleton has a reference
            if (PM_Instance != null)
            {
                // check if the reference is assigned to another gameObject
                if (PM_Instance != this)
                {
                    // if true, destroy this gameObject
                    Destroy(this);
                }
                // if reference is assigned to this object
                else if (PM_Instance == this)
                {
                    // return out of function
                    return;
                }
            }
            // if the reference isn't assigned
            else if (PM_Instance == null)
            {
                // set the reference to this gameObject
                PM_Instance = this;
            }
        }

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
            IterateFireTimer();
        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods


        void IterateFireTimer()
        {
            if (readyToFire) { return; }

            if(timer < fireRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                ReadyNextProjectile();
            }
        }

        private void ReadyNextProjectile()
        {
            if(listOfPools.Count == 0) { return; }

            GameObject tempProjectile = null;

            foreach(var pool in listOfPools)
            {
                tempProjectile = FindNextInactiveProjectile(pool.poolKey);

                if(tempProjectile == null)
                {
                    continue;
                }
                else if(tempProjectile != null)
                {
                    readyToFire = true;
                    loadedProjectile = tempProjectile;
                }
            }

            timer = 0f;
        }

        private GameObject FindNextInactiveProjectile(string key)
        {
            if (!projectileReferences.ContainsKey(key))
            {
                Debug.LogError("There is no key-value pair that uses this key: " + key);
                return null;
            }

            GameObject tempObj = null;

            for (int i = 0; i < projectileReferences[key].Count; i++)
            {
                tempObj = projectileReferences[key].Dequeue();

                projectileReferences[key].Enqueue(tempObj);

                if (tempObj.activeSelf)
                {
                    tempObj = null;

                    continue;
                }
                else if (!tempObj.activeSelf)
                {
                    return tempObj;
                }
            }
            return null;
        }

        public void ShootLoadedProjectile(Vector3 position)
        {
            if(loadedProjectile == null)
            {
                Debug.Log("No projectile available to shoot");
                return;
            }


            loadedProjectile.SetActive(true);
            loadedProjectile.transform.localPosition = position;

            readyToFire = false;
        }

        #endregion
    }
}