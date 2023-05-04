using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBacteria : MonoBehaviour
{
    public CharacterMovement player;
    public static float healthUp = 1f;
    public float respawnTime = 5f;
    public SpriteRenderer h;
    public Collider2D c;
    public bool isResapawning = false;
    private void Start() {
        h = GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag != "player character"){
            player = other.GetComponent<CharacterMovement>();
            
            if(player != null){
                player.healthPts += healthUp;
                player.healthUpSound.Play();
            }
        }


        StartCoroutine(respawn());
    }

    private IEnumerator respawn(){
        
        if(isResapawning) yield break;

        isResapawning = true;

        c.enabled = false;
        h.enabled = false;        

        yield return new WaitForSeconds(respawnTime);

        c.enabled = true;
        h.enabled = true;

        isResapawning = false;
    }
}
