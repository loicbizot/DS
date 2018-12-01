using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSandOfTime : MonoBehaviour {

    new public Light light;
    new public Camera camera;

    private float time;
    public float dayLength;
    public Color red;
    public Color yellow;
    public Color blue;
    public Color sky;

    // Use this for initialization
    void Start () {
        time = 0;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > dayLength)
            time -= dayLength;

        Color brithness = Color.Lerp(yellow, blue, 0.5f + Mathf.Cos(time / dayLength * Mathf.PI * 2) / 2);
        brithness = Color.Lerp(brithness, red, Mathf.Exp(-(time / dayLength - 0.75f) * (time / dayLength - 0.75f) * 900));
        Color background = Color.Lerp(sky, blue, 0.5f + Mathf.Cos(time / dayLength * Mathf.PI * 2) / 2);
        background = Color.Lerp(background, red, Mathf.Exp(-(time / dayLength - 0.75f) * (time / dayLength - 0.75f) * 900));

        light.color = brithness;
        camera.backgroundColor = background;

        light.transform.rotation = Quaternion.Euler(50, time / dayLength * 75, 0);

    }

}
