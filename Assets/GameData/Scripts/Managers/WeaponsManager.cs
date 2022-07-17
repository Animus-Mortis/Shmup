using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Manager
{
    public class WeaponsManager : MonoBehaviour
    {
        public static WeaponsManager instance;

        public Transform shutPoint;
        public List<Game.Weapon.Weapon> weapons;
        public Image iconWeapon;
        public Text shellText;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this);
        }
    }
}