using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TEST : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> TestEvent;

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.T))
        {
            TestEvent.Invoke(10);
        }
    }
}
