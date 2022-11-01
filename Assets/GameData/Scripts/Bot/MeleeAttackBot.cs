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

        private Animator animator;
        private int randomTypeAttack;
        private bool attacked;
        [Inject] private PlayerHealth player;

        private void Start()
        {
            animator = GetComponent<MovingBot>().animator;
            StartCoroutine(CheckCanAttack());
        }

        public void SetDamage()
        {
            player.TakeDamage(gamage);
        }

        private IEnumerator CheckCanAttack()
        {
            while (true)
            {
                while(Vector3.Distance(transform.position, player.transform.position) < distanceToAttack)
                {
                    attacked = true;
                    randomTypeAttack = Random.Range(1, 3);
                    animator.SetBool($"Attack 0{randomTypeAttack}", true);
                    yield return new WaitForSeconds(speedAttack);
                }
                if (attacked)
                {
                    animator.SetBool($"Attack 0{randomTypeAttack}", false);
                    attacked = false;
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}