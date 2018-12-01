using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLeaves : MonoBehaviour {

    public MeshFilter mesh;
    private Vector3[] vertices;
    private float time;
    public float loopTime = 5f;
    public float intensity = 0.03f;

	// Use this for initialization
	void Start () {

        time = 0;
        vertices = mesh.mesh.vertices;

	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;
        if(time > loopTime)
        {
            time -= loopTime;
        }

        Vector3[] movedVertices = new Vector3[4];
        movedVertices[1] = vertices[1] + new Vector3(intensity * Mathf.Cos(2 * time / loopTime * Mathf.PI), intensity * Mathf.Pow(Mathf.Sin(2 * time / loopTime * Mathf.PI),2), 0);
        movedVertices[3] = vertices[3] + new Vector3(intensity * Mathf.Cos(2 * time / loopTime * Mathf.PI), intensity * Mathf.Pow(Mathf.Sin(2 * time / loopTime * Mathf.PI), 2), 0);
        movedVertices[0] = vertices[0];
        movedVertices[2] = vertices[2];

        mesh.mesh.vertices = movedVertices;

	}
}
