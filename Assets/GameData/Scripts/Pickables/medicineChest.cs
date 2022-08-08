using UnityEngine;

namespace Game.Pickable
{
    public class medicineChest : Pickable
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && playerHealth.Damaged())
            {
                PickUpMedicalChest();
                gameObject.SetActive(false);
            }
        }
    }
}