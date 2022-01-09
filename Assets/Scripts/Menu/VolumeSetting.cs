using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.assignSliderAndMute(
            transform.Find("Image").Find("Slider").GetComponent<Slider>(),
            transform.Find("Image").Find("Toggle").GetComponent<Toggle>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
