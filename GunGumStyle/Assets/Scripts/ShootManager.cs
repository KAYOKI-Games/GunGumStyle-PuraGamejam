using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{

    [SerializeField]
    Transform muzzle;
    [SerializeField]
    GameObject acid;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            GameObject TempAsid = Instantiate(acid, muzzle.position, Quaternion.identity);
            Quaternion rotationy = transform.rotation;
            TempAsid.GetComponent<Rigidbody2D>().AddForce(new Vector2(500 * (rotationy.eulerAngles.y == 0 ? 1 : -1), 200));
        }
    }
}
