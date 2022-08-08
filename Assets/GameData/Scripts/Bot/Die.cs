using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bot
{
    public class Die : MonoBehaviour
    {
        [SerializeField] private float speedDissolve = 1;
        private Material material;
        private float alfa;

        private void Awake()
        {
            material = GetComponent<MeshRenderer>().material;
            alfa = material.GetFloat("_Alfa");
        }

        public void DieEffect()
        {
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            while (material.GetFloat("_Alfa") < 0.6f)
            {
                alfa += Time.fixedDeltaTime * speedDissolve;
                material.SetFloat("_Alfa", alfa);
                yield return new WaitForFixedUpdate();
            }
            gameObject.SetActive(false);
        }
    }
}