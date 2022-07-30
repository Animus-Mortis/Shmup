using Game.Weapon;
using UnityEngine;

namespace Game.Pickable
{
    public class Pickable : MonoBehaviour
    {
        [SerializeField] protected int value;
        [SerializeField] protected WeaponType weaponType;

        public virtual void PickUp()
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
    }
}