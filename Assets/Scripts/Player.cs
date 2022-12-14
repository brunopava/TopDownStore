using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
	private BoxCollider2D _playerCollider;

	private Vector3 _movementVector;
	public Vector3 movementVector{
		get {
			return _movementVector;
		}
	}

	private RaycastHit2D _hit;

	private bool _canMove = true;
	public bool canMove{
		get{
			return _canMove;
		}
	}

    private void Awake()
    {
    	_playerCollider = GetComponent<BoxCollider2D>();

    	NotificationCenter.DefaultCenter.AddObserver(this, "EnableMovement");
    	NotificationCenter.DefaultCenter.AddObserver(this, "DisableMovement");
    }

    public override void OnStartLocalPlayer()
    {
    	CameraController.Instance.target = transform;
    }

    private void EnableMovement()
    {
    	_canMove = true;
    }

    private void DisableMovement()
    {
    	_canMove = false;
    }

    private	 void FixedUpdate()
    {
    	if(_canMove && isLocalPlayer)
    	{
	    	float x = Input.GetAxis("Horizontal");
	    	float y = Input.GetAxis("Vertical");

	    	_movementVector = new Vector3(x, y, 0);

			_hit = Physics2D.BoxCast(transform.position, _playerCollider.size, 0 , new Vector2(0, _movementVector.y), Mathf.Abs(_movementVector.y*Time.deltaTime), LayerMask.GetMask("Block"));	
			if(_hit.collider == null)
			{
				transform.Translate(0, _movementVector.y * Time.deltaTime, 0);
			}

			_hit = Physics2D.BoxCast(transform.position, _playerCollider.size, 0 , new Vector2(_movementVector.x, 0), Mathf.Abs(_movementVector.x*Time.deltaTime), LayerMask.GetMask("Block"));
			if(_hit.collider == null)
			{
				transform.Translate(_movementVector.x * Time.deltaTime, 0, 0);
			}			
    	}
    }
}
