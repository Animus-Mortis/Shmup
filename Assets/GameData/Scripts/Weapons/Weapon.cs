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
        [SerializeField] protected Sprite icon;

        protected int countInHands;
        protected int countInBag;
        protected List<GameObject> poolShell = new List<GameObject>();

        private int numberWeapon;
        private Weapon chosenWeapon;
        private Coroutine ShutingCoroutin;

        protected void Start()
        {
            countInHands = maxCountInHands;
            countInBag = maxCountInBag;

            chosenWeapon = WeaponsManager.instance.weapons[numberWeapon];
            WeaponsManager.instance.iconWeapon.sprite = chosenWeapon.icon;
            ViewCountShell();
        }

        private void ChangeWeapon()
        {
            numberWeapon++;

            if (numberWeapon >= WeaponsManager.instance.weapons.Count)
                numberWeapon = 0;

            chosenWeapon = WeaponsManager.instance.weapons[numberWeapon];
            WeaponsManager.instance.iconWeapon.sprite = chosenWeapon.icon;
        }

        public virtual void Shuting()
        {
            ShutingCoroutin = StartCoroutine(timingBetweenShots());
        }

        public virtual void StopShut()
        {
            if (ShutingCoroutin != null)
                StopCoroutine(ShutingCoroutin);
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
                countInHands--;
                ViewCountShell();
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
                if (countInBag > 0 && countInHands == 0)
                    StartCoroutine(RemoteWeapon());

                yield return new WaitForSeconds(timeToAttack);
            }
        }

        private IEnumerator RemoteWeapon()
        {
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
            ViewCountShell();
        }

        private void ViewCountShell()
        {
            WeaponsManager.instance.shellText.text = $"{chosenWeapon.countInHands}/{chosenWeapon.countInBag}";
        }
    }
}