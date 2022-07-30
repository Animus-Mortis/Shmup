
using UnityEngine;

namespace Game.Pickable
{
    public class Ammunition : Pickable
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                PickUp();
                gameObject.SetActive(false);
            }
        }
    }
}