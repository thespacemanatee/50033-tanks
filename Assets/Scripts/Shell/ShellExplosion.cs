﻿using Tank;
using UnityEngine;

namespace Shell
{
    public class ShellExplosion : MonoBehaviour
    {
        public LayerMask m_TankMask;
        public ParticleSystem m_ExplosionParticles;
        public AudioSource m_ExplosionAudio;
        public float m_MaxDamage = 100f;
        public float m_ExplosionForce = 1000f;
        public float m_MaxLifeTime = 2f;
        public float m_ExplosionRadius = 5f;


        private void Start()
        {
            Destroy(gameObject, m_MaxLifeTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            // Find all the tanks in an area around the shell and damage them.
            var colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

            for (var i = 0; i < colliders.Length; ++i)
            {
                var targetRigidbody = colliders[i].GetComponent<Rigidbody>();
                if (targetRigidbody == null) continue;

                targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

                var targetHealth = targetRigidbody.GetComponent<TankHealth>();
                if (targetHealth == null) continue;

                var damage = CalculateDamage(targetRigidbody.position);
                targetHealth.TakeDamage(damage);
            }

            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
            Destroy(gameObject);
        }

        private float CalculateDamage(Vector3 targetPosition)
        {
            // Calculate the amount of damage a target should take based on it's position.
            var explosionToTarget = targetPosition - transform.position;
            var explosionDistance = explosionToTarget.magnitude;
            var relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

            var damage = relativeDistance * m_MaxDamage;
            damage = Mathf.Max(0f, damage);

            return damage;
        }
    }
}