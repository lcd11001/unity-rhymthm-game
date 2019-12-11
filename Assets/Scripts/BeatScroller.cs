using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [Tooltip("How fast arrows fall down")]
    public float beatTempo;
    public bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            /*
            // Managed by GameManager
            if (Input.anyKeyDown)
            {
                hasStarted = true;
            }
            */
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
