using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Manager
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] protected Transform SpawnArea;
        [SerializeField] protected float yPositionSpawn = 0.6f;

        [Inject] private DiContainer diContainer;

        public virtual List<GameObject> FillingPool(GameObject prefab, int count)
        {
            List<GameObject> pool = new List<GameObject>();

            for (int i = 0; i < count; i++)
            {
                GameObject newObj = diContainer.InstantiatePrefab(prefab);
                newObj.gameObject.SetActive(false);
                pool.Add(newObj);
            }
            return pool;
        }

        public virtual Vector3 RandomSpawnPosition()
        {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(SpawnArea.position.x - SpawnArea.localScale.x / 2, SpawnArea.position.x + SpawnArea.localScale.x / 2);
            pos.z = Random.Range(SpawnArea.position.z - SpawnArea.localScale.z / 2, SpawnArea.position.z + SpawnArea.localScale.z / 2);
            pos.y = yPositionSpawn;
            return pos;
        }
    }
}