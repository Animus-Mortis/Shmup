using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Manager
{
    public class WeaponsManager : MonoBehaviour
    {
        public static WeaponsManager instance;

        [SerializeField] private Transform shutPoint;
        public Transform ShutPoint { get { return shutPoint; } }

        [SerializeField] private List<Weapon.Weapon> weapons;
        public List<Weapon.Weapon> Weapons { get { return weapons; } }

        [SerializeField] private Image iconWeapon;
        [SerializeField] private Text shellText;

        private int numberWeapon;
        private Weapon.Weapon chosenWeapon;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this);

            chosenWeapon = weapons[numberWeapon];
            iconWeapon.sprite = chosenWeapon.Icon;
        }

        public void ChangeWeapon()
        {
            numberWeapon++;

            if (numberWeapon >= weapons.Count)
                numberWeapon = 0;

            chosenWeapon = weapons[numberWeapon];
            iconWeapon.sprite = chosenWeapon.Icon;
            ViewCountShell();
        }

        public void Shuting()
        {
            chosenWeapon.Shuting();
        }

        public void StopShut()
        {
            chosenWeapon.StopShut();
        }

        public void ViewCountShell()
        {
            shellText.text = $"{chosenWeapon.CountInHands}/{chosenWeapon.CountInBag}";
        }
    }
}