using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager
{
    public class FireSplashManager : MonoBehaviour
    {
        public static FireSplashManager instance;

        [SerializeField] private ParticleSystem splashPrefab; 
        [SerializeField] private int count;
        [SerializeField] private List<ParticleSystem> Splashes = new List<ParticleSystem>();

        private void Awake()
        {
            instance = this;
            FillingPool();
        }

        private void FillingPool()
        {
            for(int i =0; i < count; i++)
            {
                ParticleSystem newSplash = Instantiate(splashPrefab);
                Splashes.Add(newSplash);
            }
        }

        public void SplashActive(Vector3 position)
        {
            for (int i = 0; i < Splashes.Count; i++)
            {
                if (Splashes[i].isStopped)
                {
                    Splashes[i].transform.position = position;
                    Splashes[i].Play();
                    break;
                }
            }
        }
    }
}