using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 3.0f;
    
    private Vector3 _destPos;
    private bool _moveToDest = false;
    void Start()
    {
        Managers.Input.MouseAction += OnMouseClicked;
    }

    private float wait_run_ratio = 0;
    private void Update()
    {
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.00001f)
            {
                _moveToDest = false;
            }
            else
            {
                float moveDist = Math.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                if (dir.magnitude > 0.01f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir),
                        10 * Time.deltaTime);
                }
            }
        }
        
        //애니메이션
        if (_moveToDest)
        {
            Animator anim = GetComponent<Animator>();
            wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 20 * Time.deltaTime);
            anim.SetFloat("wait_run_ratio", 1);
            anim.Play("WAIT_RUN");
        }
        else
        {
            Animator anim = GetComponent<Animator>();
            wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 20 * Time.deltaTime);
            anim.SetFloat("wait_run_ratio", 0);
            anim.Play("WAIT_RUN");
        }
    }

    private void OnMouseClicked(Define.MouseEvent obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _moveToDest = true;
        }
    }
}
