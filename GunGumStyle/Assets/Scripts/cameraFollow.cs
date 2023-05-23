using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform target;
    [SerializeField]
    float maxX,minX,minY,maxY;
    [SerializeField]
    float lerpTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = transform.position;
        transform.position = Vector3.Lerp(currPos, new Vector3(Mathf.Clamp(target.position.x,minX,maxX), Mathf.Clamp(target.position.y,minY,maxY), transform.position.z), lerpTime);
    }


}
