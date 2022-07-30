using Game.Pickable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Manager
{
    public class AmmonitionManager : MonoBehaviour
    {
        [SerializeField] private List<DataAmmo> ammos;
        [SerializeField] private Transform SpawnArea;
        [SerializeField] private float yPositionSpawn = 0.6f;

        private void Awake()
        {
            FillingPool();
        }

        private void Start()
        {
            StartCoroutine(SpawnAmmo());
        }

        private void FillingPool()
        {
            for (int j = 0; j < ammos.Count; j++)
            {
                for (int i = 0; i < ammos[j].count; i++)
                {
                    Ammunition newAmmo = Instantiate(ammos[j].ammunition);
                    ammos[j].ammunitions.Add(newAmmo);
                    newAmmo.gameObject.SetActive(false);
                }
            }
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

        private Vector3 RandomSpawnPosition()
        {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(SpawnArea.transform.position.x - SpawnArea.localScale.x / 2, SpawnArea.transform.position.x + SpawnArea.localScale.x / 2);
            pos.z = Random.Range(SpawnArea.transform.position.z - SpawnArea.localScale.z / 2, SpawnArea.transform.position.z + SpawnArea.localScale.z / 2);
            pos.y = yPositionSpawn;
            return pos;
        }
    }

    [System.Serializable]
    public class DataAmmo
    {
        public Ammunition ammunition;
        public List<Ammunition> ammunitions = new List<Ammunition>();
        public int count;
        public float timeToSpawn;

    }
}