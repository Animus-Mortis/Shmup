using Game.Bot;
using UnityEngine;
namespace Game.Weapon
{
    public class DamageWeapon : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private LayerMask mask;
        private void OnTriggerEnter(Collider other)
        {
            if ((mask.value & (1 << other.gameObject.layer)) > 0)
            {
                print(123);
                if (other.GetComponent<HealthBot>())
                {
                    other.GetComponent<HealthBot>().TakeDamage(damage);
                }

                gameObject.SetActive(false);
            }
            print(3);
        }
    }
}