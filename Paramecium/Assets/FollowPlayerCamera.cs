using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float targetZoomSize = 7f, oldZoomSize, isCameraZoom = 0;
    private Collider2D myCameraTrigger;
    private Camera myCamera;

    public cameraZoomHandler currentCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        oldZoomSize = myCamera.orthographicSize;
        transform.position = new Vector3(0f,0f,-20);
    }

    // Update is called once per frame
    private void Update() {

        // smoothly zooms the camera
        myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, targetZoomSize, Time.deltaTime);

    }

    private void FixedUpdate()
    {
        // follows the player
        this.transform.position = new Vector3(target.position.x, target.position.y, -20);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        // oldZoomSize = myCamera.orthographicSize;

        // if(other.tag == "cameraSize5")
        // {
        //     targetZoomSize = 5f;
        //     //Mathf.Max(oldZoomSize, 5f);
        // }            
        // else if(other.tag == "cameraSize10")
        // {
        //     targetZoomSize = 10f;
        // }       
        // else if(other.tag == "cameraSize15")
        // {
        //     targetZoomSize = 15f;
        // }
        // else if(other.tag == "cameraSize20")
        // {
        //     targetZoomSize = 20f;
        // }

        // oldZoomSize = targetZoomSize;

        if(other.GetComponent<cameraZoomHandler>() != null)
        {
            currentCamera = other.GetComponent<cameraZoomHandler>();

            targetZoomSize = currentCamera.zoomSize;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // if(other.tag == "cameraSize5" || other.tag == "cameraSize10" || other.tag == "cameraSize15" || other.tag == "cameraSize20")
        // {
        //     targetZoomSize = 5f;
        // }

        if(other.GetComponent<cameraZoomHandler>() != null)
        {
            if(other.GetComponent<cameraZoomHandler>() == currentCamera)
            {
                targetZoomSize = 7f;
            }
        }
    }
}
