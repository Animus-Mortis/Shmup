using Game.Pickable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Manager
{
    public class AmmonitionManager : Spawner
    {
        [SerializeField] private List<DataAmmo> ammos;

        private void Awake()
        {
            for (int j = 0; j < ammos.Count; j++)
            {
                ammos[j].ammunitions = base.FillingPool(ammos[j].ammunition.gameObject, ammos[j].count);
            }
        }

        private void Start()
        {
            StartCoroutine(SpawnAmmo());
        }

        private IEnumerator SpawnAmmo()
        {
            while (true)
            {
                int randomAmmo = Random.Range(0, ammos.Count);
                yield return new WaitForSeconds(ammos[randomAmmo].timeToSpawn);
                for (int i = 0; i < ammos[randomAmmo].ammunitions.Count; i++)
                {
                    if (!ammos[randomAmmo].ammunitions[i].gameObject.activeSelf)
                    {
                        ammos[randomAmmo].ammunitions[i].transform.position = RandomSpawnPosition();
                        ammos[randomAmmo].ammunitions[i].gameObject.SetActive(true);
                        break;
                    }
                }
                yield return null;
            }
        }
    }

    [System.Serializable]
    public class DataAmmo
    {
        public Ammunition ammunition;
        public List<GameObject> ammunitions = new List<GameObject>();
        public int count;
        public float timeToSpawn;
    }
}