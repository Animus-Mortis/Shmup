using Game.Player;
using Game.Weapon;
using UnityEngine;

namespace Game.Pickable
{
    public class Pickable : MonoBehaviour
    {
        [SerializeField] protected int value;

        protected PlayerHealth playerHealth;

        private void Start()
        {
            playerHealth = Manager.ListLinkObject.instance.Player.GetComponent<PlayerHealth>();
        }

        public virtual void PickUp(WeaponType weaponType)
        {
            for(int i=0; i < Manager.WeaponsManager.instance.weapons.Count; i++)
            {
                if(weaponType == Manager.WeaponsManager.instance.weapons[i].type)
                {
                    Manager.WeaponsManager.instance.weapons[i].countInBag += value;
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