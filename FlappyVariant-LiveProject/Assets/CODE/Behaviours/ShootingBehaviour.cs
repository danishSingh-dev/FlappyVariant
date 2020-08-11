using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Management;

namespace Flappy.Behaviours
{



    #region Component Overview
    /// <summary>
    /// [Allows the Player to shoot projectiles]
    /// </summary>
    #endregion

    public class ShootingBehaviour : MonoBehaviour
    {
        #region Public Variables

        [SerializeField] private Transform firePoint = null;
        
        #endregion



        #region Private Variables
        private ProjectileManager projectileManager = null;
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            projectileManager = ProjectileManager.PM_Instance;
        }

        #endregion



        #region Custom Methods

        public void TriggerShot()
        {
            if (projectileManager.ReadyToFire)
            {
                projectileManager.ShootLoadedProjectile(firePoint.position);
            }
        }

        #endregion
    }
}