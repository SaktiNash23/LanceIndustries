using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_LaserOrigin : MonoBehaviour
{
    public GameObject projectileSphere;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(projectileSphere, transform.position + new Vector3(0.0f, 0.1f, 0.0f), Quaternion.identity);
            //projectile.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
