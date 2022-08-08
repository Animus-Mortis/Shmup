using Game.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Bot
{
    public class MovingBot : MonoBehaviour
    {
        [SerializeField] private Transform[] movingPoints;
        [SerializeField] private float distanceToVisionPlayer;
        [SerializeField] private float speedMove;

        private Transform playerTransform;
        private NavMeshAgent agent;
        private Transform nowMovingPoints;
        private MeleeAttackBot MeleeAttack;
        private SpawnBotsManager spawnBotsManager;
        private bool seePlayer;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            MeleeAttack = GetComponent<MeleeAttackBot>();
        }
        private void Start()
        {
            playerTransform = ListLinkObject.instance.Player;
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

        private IEnumerator CheckPlayer()
        {
            while (true)
            {
                while (Vector3.Distance(transform.position, playerTransform.position) < distanceToVisionPlayer)
                {
                    if (Vector3.Distance(transform.position, playerTransform.position) < MeleeAttack.distanceToAttack)
                        agent.speed = 0;
                    else
                        agent.speed = speedMove;

                    MoveAgent(playerTransform.position);
                    seePlayer = true;
                    yield return new WaitForSeconds(0.2f);
                }

                if (nowMovingPoints != null)
                    MoveAgent(nowMovingPoints.position);

                seePlayer = false;
                yield return null;
            }
        }
    }
}