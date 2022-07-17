using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Manager {
    public class ListLinkObject : MonoBehaviour
    {
        public static ListLinkObject instance;

        public Transform Player;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this);
        }
    }
}