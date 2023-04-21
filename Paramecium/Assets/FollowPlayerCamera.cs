using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    public CharacterMovement player;
    public float targetZoomSize = 7f, oldZoomSize, isCameraZoom = 0;
    private Collider2D myCameraTrigger;
    private Camera myCamera;

    public cameraZoomHandler currentCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        oldZoomSize = myCamera.orthographicSize;
        player = FindObjectOfType<CharacterMovement>();
        transform.position = new Vector3(0f,0f,-20);
        myCamera.transform.position = Vector3.forward;
    }

    // Update is called once per frame
    private void Update() {

        // smoothly zooms the camera
        myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, targetZoomSize, Time.deltaTime);


    }

    private void FixedUpdate()
    {
        // follows the player
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20);

        if(player.isHit)
            StartCoroutine(shakeCam());       
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.GetComponent<cameraZoomHandler>() != null)
        {
            currentCamera = other.GetComponent<cameraZoomHandler>();

            targetZoomSize = currentCamera.zoomSize;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {

        if(other.GetComponent<cameraZoomHandler>() != null)
        {
            if(other.GetComponent<cameraZoomHandler>() == currentCamera)
            {
                targetZoomSize = 7f;
            }
        }
    }

    private IEnumerator shakeCam(){

        Vector3 originalPos = transform.localPosition;

        float ShakeMagnitude = .1f;
        float elapsedTime = 0.0f;
        float y, x;


        while(elapsedTime < 0.2f)
        {            
            elapsedTime += Time.fixedDeltaTime;
            
            x = Random.Range(1f, -1f) * ShakeMagnitude * myCamera.orthographicSize;
            y = Random.Range(1f, -1f) * ShakeMagnitude * myCamera.orthographicSize;

            Vector3 newPos = new Vector3(originalPos.x, originalPos.y + y, originalPos.z);
            transform.localPosition = Vector3.Lerp(transform.position, newPos, Time.fixedDeltaTime * 10f);
            //transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z); 

            yield return null;                   
        }

        // transform.localPosition = new Vector3(transform.position.x,0.1f,originalPos.z);
        // yield return null;
        // transform.localPosition = new Vector3(transform.position.x,-0.1f,originalPos.z);
        // yield return null;

        transform.localPosition = originalPos;
    }
}
