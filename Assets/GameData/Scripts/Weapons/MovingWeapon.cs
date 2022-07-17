using UnityEngine;
namespace Game.Weapon
{
    public class MovingWeapon : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        private void FixedUpdate()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}