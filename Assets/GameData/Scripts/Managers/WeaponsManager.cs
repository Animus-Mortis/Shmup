using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Manager
{
    public class WeaponsManager : MonoBehaviour
    {
        public static WeaponsManager instance;

        public Transform shutPoint;
        public List<Weapon.Weapon> weapons;
        public Image iconWeapon;
        public Text shellText;

        private int numberWeapon;
        private Weapon.Weapon chosenWeapon;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this);

            chosenWeapon = weapons[numberWeapon];
            iconWeapon.sprite = chosenWeapon.icon;
        }

        public void ChangeWeapon()
        {
            numberWeapon++;

            if (numberWeapon >= weapons.Count)
                numberWeapon = 0;

            chosenWeapon = weapons[numberWeapon];
            iconWeapon.sprite = chosenWeapon.icon;
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
           shellText.text = $"{chosenWeapon.countInHands}/{chosenWeapon.countInBag}";
        }
    }
}