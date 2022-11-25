using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Bot
{
    public class Die : MonoBehaviour
    {
        [SerializeField] private float speedDissolve = 1;
        [SerializeField] private Renderer[] rendererBody;
        [SerializeField] private UnityEvent ActionWithDieEvent;

        private List<DissolvingMaterial> dissolvingMaterial = new List<DissolvingMaterial>();

        private void Awake()
        {
            for (int i = 0; i < rendererBody.Length; i++)
            {
                dissolvingMaterial.Add(new DissolvingMaterial(rendererBody[i].material, rendererBody[i].material.GetFloat("_Alfa")));
            }
        }

        public void DieEffect()
        {
            for (int i = 0; i < dissolvingMaterial.Count; i++)
            {
                StartCoroutine(Dissolve(dissolvingMaterial[i]));
            }

            ActionWithDieEvent.Invoke();
        }

        private IEnumerator Dissolve(DissolvingMaterial dissolvingMaterial)
        {
            while (dissolvingMaterial.material.GetFloat("_Alfa") < 0.6f)
            {
                dissolvingMaterial.alfa += Time.fixedDeltaTime * speedDissolve;
                dissolvingMaterial.material.SetFloat("_Alfa", dissolvingMaterial.alfa);
                yield return new WaitForFixedUpdate();
            }

            gameObject.SetActive(false);
        }
    }
}