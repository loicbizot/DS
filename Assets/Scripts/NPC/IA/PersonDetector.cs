using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDetector : MonoBehaviour {

    public GameObject detected;
    List<GameObject> inAreaGO;

    public Transform entityTransform;
    public ObjectOrientation orientation;
    public NPCFollowing NPCFollowing;
    public float FOV;

	// Use this for initialization
	void Start () {

        inAreaGO = new List<GameObject>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if(detected != null)
        {

            // poursuivre l'entité détectée si elle est encore visible
            
        }

        List<GameObject> seen = new List<GameObject>();

        // verifier s'il y a quelque chose de suivable
        foreach(GameObject entity in inAreaGO)
        {

            Vector3 ray = entity.transform.position - entityTransform.position;
            Vector3 ori = new Vector3(orientation.orientation.x, 0, orientation.orientation.y);
            float angle = Vector3.Angle(ray, ori);

            if(angle <= FOV / 2)
            {

                int layer = 1 << 9;
                layer = ~layer;

                bool raycast = Physics.Raycast(entityTransform.position, ray, 5, layer);
                if(raycast)
                {

                }
                else
                {
                    seen.Add(entity);
                }

            }

        }

        if(seen.Count > 0)
            NPCFollowing.toFollow = seen[0].transform;
        else
            NPCFollowing.toFollow = null;

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("main_char") ||other.tag.Equals("followable"))
            inAreaGO.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        inAreaGO.Remove(other.gameObject);
    }

}
