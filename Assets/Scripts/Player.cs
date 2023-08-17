using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    public float _yAngle = 0;
    void Update()
    {
        _yAngle += Time.deltaTime * 100;
        //transform.eulerAngles = new Vector3(0, _yAngle, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, _yAngle, 0));
        //transform.Rotate(new Vector3(0, Time.deltaTime * 100, 0));
        
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
    
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
    
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
    }
}
