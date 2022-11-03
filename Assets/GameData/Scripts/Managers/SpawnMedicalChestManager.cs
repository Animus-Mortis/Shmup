using Game.Pickable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager
{
    public class SpawnMedicalChestManager : Spawner
    {
        [SerializeField] private medicineChest medicineChest;
        [SerializeField] private int count;
        [SerializeField] private float timeToSpawn;

        private List<GameObject> chests = new List<GameObject>();

        private void Awake()
        {
            base.Awake();
            chests = base.FillingPool(medicineChest.gameObject, count);
        }

        private void Start()
        {
            StartCoroutine(SpawnChest());
        }

        private IEnumerator SpawnChest()
        {
            while (true)
            {
                int randomAmmo = Random.Range(0, chests.Count);
                yield return new WaitForSeconds(timeToSpawn);
                for (int i = 0; i < chests.Count; i++)
                {
                    if (!chests[i].activeSelf)
                    {
                        chests[i].transform.position = RandomSpawnPosition();
                        chests[i].SetActive(true);
                        break;
                    }
                }
                yield return null;
            }
        }
    }
}