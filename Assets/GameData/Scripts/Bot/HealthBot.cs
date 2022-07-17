using Game.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Bot
{
    public class HealthBot : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHP;
        [SerializeField] private float nowHP;
        [SerializeField] private UnityEvent DieEvent;
        [SerializeField] private UnityEvent<float, float> ChangeHPOnUI;

        private SpawnBotsManager spawnBotsManager;

        private void Start()
        {
            nowHP = maxHP;
        }

        public void AddSpwnerManager(SpawnBotsManager spawnManager)
        {
            if (spawnBotsManager == null)
                spawnBotsManager = spawnManager;
        }

        public void Die()
        {
            spawnBotsManager.RemoveBotFromScene(gameObject);
            DieEvent.Invoke();
        }

        public void TakeDamage(float damage)
        {
            nowHP -= damage;
            ChangeHPOnUI.Invoke(nowHP, maxHP);
            if (nowHP <= 0)
                Die();
        }
    }
}