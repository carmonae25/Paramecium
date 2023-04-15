using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paramecium : MonoBehaviour
{
    public Vector2 movementDirection;
    public float movementSpeed = 5f, timer=0.0f, angle = 0.0f;
    public float rotateTime = 1f;
    private Rigidbody2D myRb;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();

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

            // get current rotation and target rotation
            angle = Random.Range(-180,180);
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f,0f, Random.Range(-180,180));

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
}
