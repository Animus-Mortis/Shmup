using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHP;
        [SerializeField] private float nowHP;
        [SerializeField] private UnityEvent DieEvent;
        [SerializeField] private UnityEvent<float,float> ChangeHPOnUI;

        private void Start()
        {
            nowHP = maxHP;
        }

        public void TakeDamage(float damage)
        {
            nowHP -= damage;
            ChangeHPOnUI.Invoke(nowHP, maxHP);
            if (nowHP <= 0)
                Die();
        }

        public void Die()
        {
            DieEvent.Invoke();
        }
    }
}