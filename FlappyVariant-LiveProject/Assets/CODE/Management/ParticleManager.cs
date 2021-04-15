using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Tools;

namespace Flappy.Management
{


    #region Component Overview
    /// <summary>
    /// [Keeps track of active and inactive particle systems in the level]
    /// </summary>
    #endregion

    public class ParticleManager : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private List<poolStructure> listOfPools = new List<poolStructure>();

        public static ParticleManager PA_Instance = null;
        #endregion



        #region Private Variables
        private Dictionary<string, Queue<GameObject>> particleReferences = new Dictionary<string, Queue<GameObject>>();
        #endregion



        #region MonoBehaviour Methods
        private void Awake()
        {
            // Check if singleton has a reference
            if (PA_Instance != null)
            {
                // check if the reference is assigned to another gameObject
                if (PA_Instance != this)
                {
                    // if true, destroy this gameObject
                    Destroy(this);
                }
                // if reference is assigned to this object
                else if (PA_Instance == this)
                {
                    // return out of function
                    return;
                }
            }
            // if the reference isn't assigned
            else if (PA_Instance == null)
            {
                // set the reference to this gameObject
                PA_Instance = this;
            }
        }
        private void Start()
        {
            if(listOfPools.Count > 0)
            {
                foreach(var pool in listOfPools)
                {
                    ObjectDuplicator.PopulateDictionaryWithPoolStructure(ref particleReferences, pool);
                }
            }
        }


        #endregion



        #region Custom Methods

        public void PlayRandomParticleAtPosition(Vector3 position)
        {
            if(listOfPools.Count == 0) { return; }

            int rnd = Random.Range(0, listOfPools.Count - 1);
            string keyValue = listOfPools[rnd].poolKey;

            if (!particleReferences.ContainsKey(keyValue)) { return; }

            GameObject tempObj = particleReferences[keyValue].Dequeue();
            particleReferences[keyValue].Enqueue(tempObj);

            tempObj.SetActive(true);
            tempObj.transform.position = position;
            tempObj.TryGetComponent<ParticleSystem>(out ParticleSystem particle);

            if(particle != null)
            {
                particle.Play();
                
            }
        }

        public void ResetAllParticles()
        {
            foreach (var pool in listOfPools)
            {
                for (int i = 0; i < particleReferences[pool.poolKey].Count; i++)
                {
                    GameObject temp = particleReferences[pool.poolKey].Dequeue();
                    particleReferences[pool.poolKey].Enqueue(temp);

                    if (temp.activeSelf)
                    {
                        temp.SetActive(false);
                    }
                    else
                    {
                        continue;
                    }


                }
            }
        }


        #endregion
    }
}