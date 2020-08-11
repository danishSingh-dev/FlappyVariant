using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Management;

namespace Flappy.Behaviours
{


    #region Component Overview
    /// <summary>
    /// [Handles all projectile related mechanics]
    /// </summary>
    #endregion

    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileBehaviour : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private float moveSpeed = 5f;
        #endregion



        #region Private Variables
        private Rigidbody _rb = null;
        private ParticleManager particleManager = null;
        private ScoreSystem scoreSystem = null;

        private SFXManager sfxManager = null;
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            TryGetComponent(out _rb);

            _rb.velocity = Vector3.right * moveSpeed;

            particleManager = ParticleManager.PA_Instance;
            scoreSystem = ScoreSystem.SS_Instance;
            sfxManager = SFXManager.SFX_Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == 12)
            {
                gameObject.SetActive(false);
            }
            else if(other.gameObject.layer == 9)
            {
                particleManager.PlayRandomParticleAtPosition(other.ClosestPointOnBounds(transform.position));
                other.gameObject.transform.parent.gameObject.SetActive(false);
                gameObject.SetActive(false);

                sfxManager.PlayImpactSound();
                scoreSystem.AddToScore(10);
            }
        }

        #endregion



        #region Custom Methods

        #endregion
    }
}