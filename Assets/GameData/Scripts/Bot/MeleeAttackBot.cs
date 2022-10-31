using Game.Manager;
using Game.Player;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Bot
{
    public class MeleeAttackBot : MonoBehaviour
    {
        [SerializeField] private float gamage;
        [SerializeField] private float speedAttack;

        public float distanceToAttack;

        [Inject] private PlayerHealth player;

        private void Start()
        {
            StartCoroutine(CheckCanAttack());
        }

        private void SetDamage()
        {
            player.TakeDamage(gamage);
        }

        private IEnumerator CheckCanAttack()
        {
            while (true)
            {
                while(Vector3.Distance(transform.position, player.transform.position) < distanceToAttack)
                {
                    SetDamage();
                    yield return new WaitForSeconds(speedAttack);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}