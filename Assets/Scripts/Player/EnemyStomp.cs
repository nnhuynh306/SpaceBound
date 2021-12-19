using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Stomp Detector")) {
            gameObject.GetComponentInParent<PlayerController>().killEnemy(other.gameObject.transform.parent.gameObject);
        }
    }
}
