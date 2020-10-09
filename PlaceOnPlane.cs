using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    // public ARSession aRSession; 정적으로 바로 가져올 수 있다.
    public ARRaycastManager arRaycaster;
    public GameObject spawnPrefab;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

  
    bool isActive = false;

    void Awake(){
        ARSession.stateChanged += OnStateChanged;
    }

    void OnStateChanged(ARSessionStateChangedEventArgs args){

        Debug.Log(args.state);

        if(args.state == ARSessionState.Unsupported)
        {
            Application.Quit();
            return;
        }

        if(args.state ==ARSessionState.Ready){
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }

    void Update()
    {
        if(!isActive) return;

        /* if(ARSession.state != ARSessionState.Ready)
        {
            return;
        }
         */

        if(Input.touchCount == 0)
        {
            return;
        }

        Touch  touch = Input.GetTouch(0); //여러개의 터치정보가 터치 type으로 오게됨. 마우스 커서 정보 포함 드래깅까지 또는 다른 부가정보까지 들어감

        if(touch.phase != TouchPhase.Began)
        {
            return;
        }

       if( arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;

            Instantiate(spawnPrefab, hitPose.position, hitPose.rotation); 
        }
    }
}
