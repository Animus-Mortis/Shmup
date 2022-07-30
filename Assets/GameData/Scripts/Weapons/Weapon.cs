using Game.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected GameObject shellPrefabs;
        [SerializeField] protected float timeToAttack;
        [SerializeField] protected float reloadTime;
        [SerializeField] protected int maxCountInHands;
        [SerializeField] protected int maxCountInBag;
        [SerializeField] public WeaponType type;
        [SerializeField] public Sprite icon;

        public int countInHands;
        public int countInBag;
        protected List<GameObject> poolShell = new List<GameObject>();

        private Coroutine ShutingCoroutin;
        private Coroutine RemoteCoroutine;
        private bool canShut = true;

        protected void Start()
        {
            countInHands = maxCountInHands;
            countInBag = maxCountInBag;

           WeaponsManager.instance.ViewCountShell();
        }

        public virtual void Shuting()
        {
            ShutingCoroutin = StartCoroutine(timingBetweenShots());
        }

        public virtual void StopShut()
        {
            canShut = false;
            if (ShutingCoroutin != null)
                StopCoroutine(ShutingCoroutin);
            if (countInBag > 0 && countInHands == 0 && RemoteCoroutine == null)
                RemoteCoroutine = StartCoroutine(RemoteWeapon());

            StartCoroutine(ShutDelay());
        }

        public virtual void FillingPoolShell()
        {
            for (int i = 0; i < maxCountInHands + maxCountInBag + 10; i++)
            {
                GameObject newShell = Instantiate(shellPrefabs);
                newShell.SetActive(false);
                poolShell.Add(newShell);
            }
        }

        private IEnumerator timingBetweenShots()
        {
            while (countInHands > 0)
            {
                while (!canShut)
                {
                    yield return null;
                }

                for (int i = 0; i < poolShell.Count; i++)
                {
                    if (!poolShell[i].activeSelf)
                    {
                        poolShell[i].transform.position = WeaponsManager.instance.shutPoint.position;
                        poolShell[i].transform.rotation = WeaponsManager.instance.shutPoint.GetComponentInParent<Transform>().rotation;
                        poolShell[i].SetActive(true);
                        break;
                    }
                }
                countInHands--;
                WeaponsManager.instance.ViewCountShell();

                if (countInBag > 0 && countInHands == 0 && RemoteCoroutine == null)
                    RemoteCoroutine = StartCoroutine(RemoteWeapon());

                yield return new WaitForSeconds(timeToAttack);
            }
        }

        private IEnumerator RemoteWeapon()
        {
            if (ShutingCoroutin != null)
                StopCoroutine(ShutingCoroutin);

            yield return new WaitForSeconds(reloadTime);
            int countShell;
            if (countInBag >= maxCountInHands)
            {
                countShell = maxCountInHands;
            }
            else
                countShell = countInBag;

            countInBag -= countShell;
            countInHands = countShell;
            WeaponsManager.instance.ViewCountShell();
            RemoteCoroutine = null;
        }

        private IEnumerator ShutDelay()
        {
            yield return new WaitForSeconds(timeToAttack);
            canShut = true;
        }
    }
}