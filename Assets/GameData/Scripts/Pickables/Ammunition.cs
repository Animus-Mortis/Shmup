using Game.Weapon;
using UnityEngine;

namespace Game.Pickable
{
    public class Ammunition : Pickable
    {
        [SerializeField] protected WeaponType weaponType;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                PickUp(weaponType);
                gameObject.SetActive(false);
            }
        }
    }
}