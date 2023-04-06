using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float cameraZoom;
    private float zoomFactor = 2f;
    private Collider2D myCameraTrigger;
    private Camera myCamera;
    private bool isZoom = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        cameraZoom = myCamera.orthographicSize;
        transform.position = new Vector3(0f,0f,-20);
    }

    private void Update() {
        if(isZoom){
            cameraZoom -= zoomFactor;
            cameraZoom = Mathf.Clamp(cameraZoom, 4f, 10f);
            myCamera.orthographicSize = Mathf.Lerp(
                myCamera.orthographicSize, cameraZoom, Time.deltaTime);
        }

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        this.transform.position = new Vector3(target.position.x, target.position.y, -20);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        // Not doing anything. Need to figure this out.
        if(other.tag == "camera trigger")
        {
            Debug.Log("Now entering open area");
            isZoom = true;
        }
    }
}
