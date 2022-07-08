using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlightscript : MonoBehaviour
{
    bool isOn = false;
    bool isFlickering = false;
    bool startFlicker = false;
    float flickerDelay = 0;
    float delay = 0;

    void Start()
    {
        this.GetComponent<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isOn)
            {
                this.GetComponent<Light>().enabled = true;
                isOn = true;
            }
            else if (isOn)
            {
                this.GetComponent<Light>().enabled = false;
                isOn = false;
            }
        }

        if (isOn && delay > 8.0f)
        {
            if (!isFlickering && delay > 8.0f)
            {
                StartCoroutine(FlickeringLight());
            }
            if (delay > 9.5f)
                delay = 0;
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        flickerDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(flickerDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        flickerDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(flickerDelay);
        isFlickering = false;
    }

    void FixedUpdate()
    {
        if (isOn)
            delay += Time.deltaTime;
        else
            delay = 0;
    }
}
