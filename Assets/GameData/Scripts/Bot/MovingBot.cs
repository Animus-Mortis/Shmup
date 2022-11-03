using Game.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Bot
{
    public class MovingBot : MonoBehaviour
    {
        public Animator animator;

        [SerializeField] private Transform[] movingPoints;
        [SerializeField] private float distanceToVisionPlayer;
        [SerializeField] private float speedMove;

        private Transform playerTransform;
        private NavMeshAgent agent;
        private Transform nowMovingPoints;
        private MeleeAttackBot MeleeAttack;
        private SpawnBotsManager spawnBotsManager;
       [SerializeField] private bool seePlayer;

        [Inject]
        public void Construct(Player.PlayerHealth playerHealth)
        {
            playerTransform = playerHealth.transform;
        }
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            MeleeAttack = GetComponent<MeleeAttackBot>();
        }
        private void Start()
        {
            AddMovingPoints(spawnBotsManager.movingPoints);
            StartCoroutine(CheckPosition());
            StartCoroutine(CheckPlayer());
        }

        public void ChangePosition(Vector3 position)
        {
            agent.Warp(position);
        }

        public void AddSpwnerManager(SpawnBotsManager spawnManager)
        {
            if (spawnBotsManager == null)
                spawnBotsManager = spawnManager;
        }

        private void AddMovingPoints(Transform[] points)
        {
            movingPoints = points;
        }

        private Transform ChangeMovingPosition()
        {
            int randomPoint = Random.Range(0, movingPoints.Length);

            while (movingPoints[randomPoint] == nowMovingPoints)
            {
                if (movingPoints.Length == 0) break;

                randomPoint = Random.Range(0, movingPoints.Length);
            }
            return nowMovingPoints = movingPoints[randomPoint];
        }

        private void MoveAgent(Vector3 position)
        {
            agent.destination = position;
        }

        private IEnumerator CheckPosition()
        {
            while (true)
            {
                while (!seePlayer)
                {
                    StartWalkAnimation();

                    if (nowMovingPoints != null)
                    {
                        while (Vector3.Distance(transform.position, nowMovingPoints.position) > 0.5f)
                        {
                            yield return new WaitForSeconds(0.2f);
                        }
                    }

                    MoveAgent(ChangeMovingPosition().position);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return null;
            }
        }

        private void StartWalkAnimation()
        {
            animator.ResetTrigger("Run Forward");
            animator.SetTrigger("Walk Forward");
        }

        private IEnumerator CheckPlayer()
        {
            while (true)
            {
                while (Vector3.Distance(transform.position, playerTransform.position) < distanceToVisionPlayer)
                {
                    animator.ResetTrigger("Walk Forward");

                    if (Vector3.Distance(transform.position, playerTransform.position) < MeleeAttack.distanceToAttack)
                    {
                        agent.speed = 0;
                        animator.ResetTrigger("Run Forward");
                    }
                    else
                    {
                        animator.SetTrigger("Run Forward");
                        agent.speed = speedMove;
                    }

                    MoveAgent(playerTransform.position);
                    seePlayer = true;
                    yield return new WaitForSeconds(0.2f);
                }

                if (nowMovingPoints != null)
                    MoveAgent(nowMovingPoints.position);
                StartWalkAnimation();
                seePlayer = false;
                yield return null;
            }
        }
    }
}