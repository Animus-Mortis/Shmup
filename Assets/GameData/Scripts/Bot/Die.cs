using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Bot
{
    public class Die : MonoBehaviour
    {
        [SerializeField] private float speedDissolve = 1;
        [SerializeField] private Renderer rendererBody;
        [SerializeField] private UnityEvent ActionWithDieEvent;
        private Material material;
        private float alfa;

        private void Awake()
        {
            material = rendererBody.material;
            alfa = material.GetFloat("_Alfa");
        }

        public void DieEffect()
        {
            StartCoroutine(Dissolve());
            ActionWithDieEvent.Invoke();
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