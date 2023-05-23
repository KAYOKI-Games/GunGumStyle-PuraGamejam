using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBullets : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform[] bullets;
    
    void Start()
    {
        StartCoroutine(throwBullets());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator throwBullets()
    {
        yield return new WaitForSeconds(2f);
        int index = Random.Range(0, bullets.Length-1);
        Transform bullet = Instantiate(bullets[index],new Vector3(transform.position.x + Random.Range(-5,5),transform.position.y,transform.position.z),Quaternion.identity);
        bullet.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        StartCoroutine(throwBullets()); 
    }
}
