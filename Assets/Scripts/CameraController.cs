using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Transform target;

    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private float _sizeDefault;
    private float _zoomValue = 0.8f;
    private float _boundX;
    private bool _isZoomingIn = false;
    private bool _isZoomingOut = false;
    private Camera _camera;

    public float zoomSpeed = 0.7f;

    private void Awake()
    {
        //saving value before zooming
        _boundX = boundX;
        _camera = GetComponent<Camera>();
        _sizeDefault = _camera.orthographicSize;
    }

    public void ZoomOnPlayer()
    {
        boundX = 0;
        _isZoomingIn = true;
    }

    public void ZoomOutPlayer()
    {
        boundX = _boundX;
        _isZoomingOut = true;
    }

    private void LateUpdate()
    {
        if(_isZoomingIn)
        {
            _camera.orthographicSize -= zoomSpeed * Time.deltaTime;
            if(_camera.orthographicSize <= _zoomValue)
            {
                _isZoomingIn = false;
            }
        }

        if(_isZoomingOut)
        {
            _camera.orthographicSize += zoomSpeed * Time.deltaTime;
            if(_camera.orthographicSize >= _sizeDefault)
            {
                _isZoomingOut = false;
            }
        }

    	Vector3 delta = Vector3.zero;

    	float deltaX = target.position.x - transform.position.x;
    	if(deltaX > boundX || deltaX < -boundX)
    	{
    		if(transform.position.x < target.position.x)
    		{
    			delta.x = deltaX - boundX;
    		}else{
    			delta.x = deltaX + boundX;
    		}
    	}

    	float deltaY = target.position.y - transform.position.y;
    	if(deltaY > boundY || deltaY < -boundY)
    	{
    		if(transform.position.y < target.position.y)
    		{
    			delta.y = deltaY - boundY;
    		}else{
    			delta.y = deltaY + boundY;
    		}
    	}

    	transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
