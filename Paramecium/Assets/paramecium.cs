using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paramecium : MonoBehaviour
{
    public Vector2 movementDirection;
    public float movementSpeed = 5f, angle = 0.0f;
    public float rotateTime = 1f;
    public bool isInAWall = false;
    public Quaternion targetRotation, startRotation;
    private Rigidbody2D myRb;

    private void Awake() {
        myRb = GetComponent<Rigidbody2D>();        
    }
    // Start is called before the first frame update
    void Start()
    {
        myRb.angularVelocity = 0f;
        myRb.angularDrag = 0f;
        angle = Random.Range(-180, 180);
        transform.eulerAngles = new Vector3(0f,0f,angle);
        StartCoroutine(move());
    }

    private IEnumerator move(){
        while(true)
        {
            //Move forward for a little bit of time
            myRb.velocity = transform.up * movementSpeed;  
            yield return new WaitForSeconds(Random.Range(1f,5f));
            
            //stop moving
            myRb.velocity = Vector2.zero;   

            //myRb.isKinematic = true;

            // get current rotation and target rotation
            angle = Random.Range(-180,180);
            startRotation = transform.rotation;
            targetRotation = Quaternion.Euler(0f,0f, Random.Range(-180,180));

            // rotate in place over a small interval of time
            float t = 0f;
            rotateTime = Random.Range(0.5f,2.0f);
            while(t < rotateTime)
            {
                    transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t / rotateTime);
        

                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
                yield return null;
            }

            // reset rotation
            transform.rotation = targetRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "wall"){
            myRb.angularVelocity = 0f;
            myRb.angularDrag = 0f;
            StopCoroutine(move());
        }
    }
}
