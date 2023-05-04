using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticCode : MonoBehaviour
{
    public SpriteRenderer mySr;
    // Start is called before the first frame update
    void Start()
    {
        mySr = GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeColor", 0.1f, 0.1f);
    }

    private void ChangeColor(){
        mySr.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }
}
