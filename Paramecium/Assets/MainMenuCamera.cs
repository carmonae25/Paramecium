using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public paramecium p;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<paramecium>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(p.transform.position.x, p.transform.position.y, -20);
    }
}
