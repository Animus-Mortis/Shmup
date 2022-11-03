using UnityEngine;
using Zenject;

namespace Game.Factory
{
    public class FactoryObject : MonoBehaviour
    {
        [Inject] private DiContainer diContainer;

        public GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion quaternion, Transform parent)
        {
            return Instantiate(prefab, position, quaternion, parent);
        }

        public GameObject InstantiateObjectWithInzect(GameObject prefab, Vector3 position, Quaternion quaternion, Transform parent)
        {
            return diContainer.InstantiatePrefab(prefab, position, quaternion, parent);
        }
    }
}