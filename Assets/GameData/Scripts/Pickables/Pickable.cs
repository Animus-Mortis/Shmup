using Game.Player;
using Game.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Pickable
{
    public class Pickable : MonoBehaviour
    {
        [SerializeField] protected int value;

        [Inject] protected PlayerHealth playerHealth;

        public virtual void PickUp(WeaponType weaponType)
        {
            for (int i = 0; i < Manager.WeaponsManager.instance.Weapons.Count; i++)
            {
                if (weaponType == Manager.WeaponsManager.instance.Weapons[i].Type)
                {
                    Manager.WeaponsManager.instance.Weapons[i].CountInBag += value;
                    Manager.WeaponsManager.instance.ViewCountShell();
                    break;
                }
            }
        }

        public virtual void PickUpMedicalChest()
        {
            playerHealth.SetHP(value);
        }
    }
}