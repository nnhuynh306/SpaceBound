using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendAnimationController : MonoBehaviour
{
    Animator animator;

    public string parameterName;

    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(parameterName, rigidBody.velocity.x);
    }


}
