using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        public float health;

        public bool IsAlive = true;
        
        public float maxHealth {
            get {
                return 100 * getHealthMulty();
            }
        }
        [Header("Settings")]
        public float passiveHealtAmount = 5f;
        public float passiveHealInterval = 0.3f;

        public Vector3 spawnPoint = new Vector3(0, 0, 0);
        public bool CanRespawn = false;
        public float RespawnTime = 2f;

        public bool isPlayer = false;

        public SinkManager SinkManager;

        float getHealthMulty()
        {
            if (isPlayer)
            {
                return StatsManager.main.Health;
            }
            return 1f;
        }
        
        private void Start()
        {
            health = maxHealth;

            if (SinkManager != null)
            {
                return;
            }

            if (isPlayer)
            {
                SinkManager = SinkManager.main;
            }
            else
            {
                SinkManager = gameObject.GetComponent<SinkManager>();
            }
         
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Clamp(health - damage, 0, maxHealth);

            if (health <= 0 && IsAlive == true)
            {
                IsAlive = false;

                if (CanRespawn == false)
                {
                    Destroy(gameObject);
                }
                else
                {
                    if (isPlayer)
                    {
                        CameraManager.main.Enabled = false;
                    }
                    StartCoroutine(SinkManager.SinkEffect());
                    StartCoroutine(RespawnTimer());
                }
            }
        }

        IEnumerator RespawnTimer()
        {
            yield return new WaitForSeconds(RespawnTime + SinkManager.SinkTime);
            Respawn();
        }

        public void Heal(float amount)
        {
            health = Mathf.Clamp(health + amount, 0, maxHealth);
        }

        public void Respawn()
        {
            if (CanRespawn == false)
            {
                return;
            }

            IsAlive = true;
       
            health = maxHealth;

            if (isPlayer)
            {
                MovementManager.main.canMove = true;
                CameraManager.main.Enabled = true;
            }
          
            transform.position = spawnPoint;
            SinkManager.RespawnEffect();
        }

        float timePassed = 0f;

        private void Update()
        {
            if (IsAlive && health < maxHealth)
            {
                if (timePassed > passiveHealInterval * getHealthMulty())
                {
                    Heal(passiveHealtAmount * getHealthMulty());
                    timePassed = 0;
                }
                else
                {
                    timePassed += Time.deltaTime;
                }
            }
        }
    }
}