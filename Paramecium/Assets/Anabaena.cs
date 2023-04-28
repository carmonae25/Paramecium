using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Anabaena : MonoBehaviour
{
    public float healthPts = 3;
    [SerializeField] public UnityEvent _hit;

    private void Update() {
        if(healthPts <= 0)
            _hit?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PlayerCharacter")
            healthPts -= 1;
    }
}
