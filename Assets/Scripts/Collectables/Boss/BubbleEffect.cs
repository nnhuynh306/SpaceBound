using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEffect : MonoBehaviour
{
    [Range(1.00001f, 2f)]
    public float scaleMultiplierToUpper = 1.001f;

    [Range(0.001f, 0.99999f)]
    public float scaleMultiplierToLower = 0.999f;

    private float currentScaleMultiplier;

    public Vector3 scaleUpperBound = Vector3.one;
    public Vector3 scaleLowerBound = new Vector3(0.7f, 0.7f, 0.7f);

    // Start is called before the first frame update
    void Start()
    {
        currentScaleMultiplier = scaleMultiplierToLower;
    }

    // Update is called once per frame
    void Update()
    {
        calculateMultiplier();

        transform.localScale *= currentScaleMultiplier;
    }

    void calculateMultiplier() {
        if (Vector3.Magnitude(transform.localScale).CompareTo(Vector3.Magnitude(scaleUpperBound)) > 0)  {
            currentScaleMultiplier = scaleMultiplierToLower;
        } else if (Vector3.Magnitude(transform.localScale).CompareTo(Vector3.Magnitude(scaleLowerBound)) < 0)  {
            currentScaleMultiplier = scaleMultiplierToUpper;
        }
    }
}
