using Game.Bot;
using UnityEngine;
namespace Game.Weapon
{
    public enum WeaponType { Fire, Bullet}
    public class DamageWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponType type;
        [SerializeField] private float damage;
        [SerializeField] private LayerMask mask;
        private void OnTriggerEnter(Collider other)
        {
            if ((mask.value & (1 << other.gameObject.layer)) > 0)
            {
                if (other.GetComponent<HealthBot>())
                {
                    other.GetComponent<HealthBot>().TakeDamage(damage);
                }
                if(type == WeaponType.Fire)
                {
                    Manager.FireSplashManager.instance.SplashActive(transform.position);
                }
                gameObject.SetActive(false);
            }
        }
    }
}