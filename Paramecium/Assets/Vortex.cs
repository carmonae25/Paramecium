using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    public float vortexForce = 10f;
    public Rigidbody2D playerRb;
    public Transform target, player;
    //public GameObject player;
    //public Transform target;
    float distanceToPlayer;
    Vector2 pullForce;
    // Start is called before the first frame update
    private void Start() {
        target = GetComponentInChildren<Transform>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "player character"){
            playerRb = other.GetComponent<Rigidbody2D>();
            player = other.GetComponent<Transform>();

            if(playerRb && player != null){

                distanceToPlayer = Vector2.Distance(target.position, player.position);   

                pullForce = (target.position - player.position).normalized / distanceToPlayer * vortexForce;
                playerRb.AddForce(pullForce, ForceMode2D.Force);
                
            }
        }
    }

    // private void FixedUpdate() {

    //     distanceToPlayer = Vector2.Distance(player.position, target.position);   

    //     if(distanceToPlayer <= 3f){
    //         pullForce = (target.position - player.position).normalized / distanceToPlayer * vortexForce;
    //         playerRb.AddForce(pullForce, ForceMode2D.Force);
    //     }      
    // }

    // private void OnTriggerStay2D(Collider2D other) {
    //     if(other.tag == "player character"){

    //         player = GetComponent<GameObject>();

    //         if(player != null){
    //             Vector2 direction = (transform.position - other.transform.position).normalized;
    //             playerRb.AddForce(direction * vortexForce, ForceMode2D.Force);
    //         }
    //     }
    // }
}
