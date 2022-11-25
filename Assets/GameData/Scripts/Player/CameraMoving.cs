using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class CameraMoving : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        [SerializeField] private float InterpolationSpeed = 10.0f;
        [SerializeField] private float stepOfPlayer = 1;
        [SerializeField] private float speedAround = 1;

        Vector3 target;
        private bool finished;

        private void Start()
        {
            transform.position = new Vector3(Player.position.x, 0, Player.position.z);
        }

        private void FixedUpdate()
        {
            if (finished) return;

            target = new Vector3(Player.position.x + PlayerMove.instance.move.x * stepOfPlayer, 0, Player.position.z + PlayerMove.instance.move.z * stepOfPlayer);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * InterpolationSpeed);
        }

        public void FinishView()
        {
            finished = true;
            StartCoroutine(VortexCamera());
        }

        private IEnumerator VortexCamera()
        {
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                target = new Vector3(Player.position.x, 0, Player.position.z);
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * InterpolationSpeed);
                transform.RotateAround(Player.position, Vector3.up, speedAround);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}