using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    

    public float speed = 3;

    public float stoppingDistance = 0.5f;

    public Transform player;

    public Vector3[] _targetPath;

    public int _indexPath = 0;

    public bool isEnabled = false;
    public bool stayOnGround = true;

    private void Start()
    {
    }


    private void FixedUpdate()
    {
        if(!isEnabled) {
            transform.LookAt(player);
            return;
        }
        //if(Input.GetMouseButtonDown(0))
        //{
        SetNewTarget();
        //}

        if(_targetPath == null )
        {
            return;
        }

        MoveToTarget();
    }
    
    
    private void OnDrawGizmos()
    {
        if (_targetPath != null)
        {
            for (int i = 0; i < _targetPath.Length - 2; i++)
            {
                if (_indexPath <= i)
                {
                    Gizmos.color = Color.green;
                } else
                {
                    Gizmos.color = Color.blue;
                }
                if (_targetPath[i + 1] != null)
                {
                    Gizmos.DrawLine(new Vector3(_targetPath[i].x, _targetPath[i].y + 1, _targetPath[i].z),
                        new Vector3(_targetPath[i + 1].x, _targetPath[i + 1].y + 1, _targetPath[i + 1].z));
                }
                else
                {
                    Gizmos.DrawLine(new Vector3(_targetPath[i].x, _targetPath[i].y + 1, _targetPath[i].z),
                        player.position);
                }
            }
        }
    }

    void MoveToTarget()
    {
        if (_indexPath  >=  _targetPath.Length)
        {
            return;
        }


        RoateToTarget(_targetPath[_indexPath]);
     

        transform.position = Vector3.MoveTowards(transform.position, _targetPath[_indexPath], speed * Time.deltaTime);


        float distanceToTheNextWayPoint = Vector3.Distance(transform.position, _targetPath[_indexPath]);

        float distanceToFinaltWayPoint= Vector3.Distance(transform.position, _targetPath[_targetPath.Length - 1]);



        if (distanceToTheNextWayPoint < 0.05f)
        {
            _indexPath++;
        }


        if(distanceToFinaltWayPoint < stoppingDistance)
        {
            _indexPath = _targetPath.Length;
        }
        
        
    }

    void RoateToTarget(Vector3 target)
    {
        transform.LookAt(target);
    }


    void SetNewTarget()
    {
        if(!stayOnGround) {
            PathRequest pathRequest = new PathRequest(transform.position, player.position, OnRequestReceived);

            PathRequestManager.Singleton.Request(pathRequest);
        } else {
            PathRequest pathRequest = new PathRequest(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), OnRequestReceived);

            PathRequestManager.Singleton.Request(pathRequest);
        }
    }

    public void OnRequestReceived(Vector3[] path, bool succes)
    {
        _targetPath = path;
        _indexPath = 0;
    }

}
