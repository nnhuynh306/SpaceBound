using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Transform portalImageTransform;

    private float spinSpeed = 1;

    private bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        portalImageTransform = gameObject.transform.Find("Portal Image").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        portalImageTransform.Rotate( new Vector3( 0, 0, -100 * Time.deltaTime * spinSpeed) );
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!finished) {
            spinSpeed = 5;
            finished = true;
            GameManager.Instance.finishGame();
            AudioManager.Instance.playOneAtATime("Portal");
            other.gameObject.GetComponentInParent<PlayerMovementController>().stickToPosition(gameObject.transform.position);
        }
    }
}
