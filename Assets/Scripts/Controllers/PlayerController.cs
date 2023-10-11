using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private PlayerStat _stat;
    
    private Vector3 _destPos;


    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction += OnMouseClicked;
    }

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    private PlayerState _state = PlayerState.Idle;
    
    private void Update()
    {
        switch (_state)
        {
            case PlayerState.Die :
                UpdateDie();
                break;
            case PlayerState.Moving :
                UpdateMoving();
                break;
            case PlayerState.Idle :
                UpdateIdle();
                break;
        }
    }

    private void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

    private void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
            return;
        }
        
        //이동
        float moveDist = Math.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
        //transform.position += dir.normalized * moveDist;
        NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
        nma.Move(dir.normalized * moveDist);

        Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
        {
            _state = PlayerState.Idle;
            return;
        }

        if (dir.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir),
                10 * Time.deltaTime);
        }
        
        //애니메이션
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _stat.MoveSpeed);
    }

    private void UpdateDie()
    {
        Debug.Log("Player die!!!!!");
    }

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster); 
    private void OnMouseClicked(Define.MouseEvent obj)
    {
        if (_state == PlayerState.Die)
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _mask))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Debug.Log("Monster Cliecke!");
            }
            else
            {
                Debug.Log("Ground Clicked!");
            }
        }
    }
}
