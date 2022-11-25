using Game.Bot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Factory;

namespace Game.Manager
{
    [RequireComponent(typeof(FactoryObject))]
    public class SpawnBotsManager : MonoBehaviour
    {
        public Transform[] movingPoints;

        [SerializeField] private GameObject[] botPrefabs;
        [SerializeField] private List<GameObject> poolBots;
        [SerializeField] private List<GameObject> botsInScane;
        [SerializeField] private Transform SpawnArea;
        [SerializeField] private int MaxCountBotsInWave;
        [SerializeField] private float yPositionSpawn = 0.6f;
        [SerializeField] private float timeToSpawnBot;

        private MeshRenderer mesh;
        private FactoryObject factory;

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
            mesh.enabled = false;
            factory = GetComponent<FactoryObject>();

            FillingPool();
        }

        private void Start()
        {
            StartCoroutine(CheckBotInWave());
        }

        public void RemoveBotFromScene(GameObject bot)
        {
            if (botsInScane == null) return;

            for (int i = 0; i < botsInScane.Count; i++)
            {
                if (bot == botsInScane[i])
                    botsInScane.Remove(botsInScane[i]);
            }
        }

        private void FillingPool()
        {
            for (int i = 0; i < botPrefabs.Length; i++)
            {
                for (int j = 0; j < MaxCountBotsInWave; j++)
                {
                    GameObject newBot = factory.InstantiateObjectWithInzect(botPrefabs[i], Vector3.zero, Quaternion.identity, null);
                    newBot.SetActive(false);
                    newBot.GetComponent<HealthBot>().AddSpwnerManager(this);
                    newBot.GetComponent<MovingBot>().AddSpwnerManager(this);
                    poolBots.Add(newBot);
                }
            }
        }

        private IEnumerator StartWave()
        {
            int randomBot = Random.Range(MaxCountBotsInWave / 2, MaxCountBotsInWave);
            for (int i = 0; i < randomBot; i++)
            {
                poolBots[i].GetComponent<MovingBot>().ChangePosition(GetSpawnPosition());
                botsInScane.Add(poolBots[i]);
                poolBots[i].SetActive(true);
                yield return new WaitForSeconds(timeToSpawnBot);
            }
        }

        private Vector3 GetSpawnPosition()
        {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(SpawnArea.transform.position.x - SpawnArea.localScale.x / 2, SpawnArea.transform.position.x + SpawnArea.localScale.x / 2);
            pos.z = Random.Range(SpawnArea.transform.position.z - SpawnArea.localScale.z / 2, SpawnArea.transform.position.z + SpawnArea.localScale.z / 2);
            pos.y = yPositionSpawn;
            return pos;
        }

        private IEnumerator CheckBotInWave()
        {
            while (true)
            {
                if (botsInScane.Count == 0)
                {
                    StartCoroutine(StartWave());
                }
                yield return new WaitForSeconds(1);
            }
        }
    }
}