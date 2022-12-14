using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// using DG.Tweening;
using Mirror;

public class PathfinderController : NetworkBehaviour
{
    private Tilemap cells;
    private Transform _movementSelector;

    private bool _isMoving = false;
    public bool isMoving {
        get {
            return _isMoving;
        }
    }

    private Vector3Int _lastPosition;

    private Vector3 _moveDirection;
    public Vector3 moveDirection{
        get {
            return _moveDirection;
        }
    }

    private bool _canInteract = true;

    private Transform _player;

    private bool _exploringModeOn = false;

    public void OnEnterExploration()
    {
        if(isLocalPlayer)
        {
            cells = Pathfinding.Instance.cells;

            _movementSelector = GameObject.FindGameObjectWithTag("MovementSelector").transform;

            // _player = transform.GetChild(0);
            // TopDownCameraController.Instance.target = transform;

            _exploringModeOn = true;
        }
    }

    public void OnExitExploration()
    {
        _exploringModeOn = false;
    }

    private void OnEnableMovement()
    {
        PathfinderEnableInteraction();
    }

    private void OnDisableMovement()
    {
        PathfinderDisableInteraction();
    }

    private void PathfinderEnableInteraction()
    {
        _canInteract = true;
    }

    private void PathfinderDisableInteraction()
    {
        _canInteract = false;
    }

    private void Update()
    {
        if(!_isMoving && _canInteract && isLocalPlayer && _exploringModeOn)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePos = cells.LocalToCell(worldPos);

            Vector2Int destination = new Vector2Int(tilePos.x, tilePos.y);
            _movementSelector.position = cells.GetCellCenterWorld(tilePos);

            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int startPosition = cells.LocalToCell(transform.position);
                Vector2Int start = new Vector2Int(startPosition.x, startPosition.y);

                if (Pathfinding.Instance.EligibleClick(destination))
                {
                    List<Node> path = new List<Node>();
                    Pathfinding.Instance.FindPath(start, destination, out path);

                    _isMoving = true;
                    MovePlayer(path);
                }
                else
                {
                    Debug.Log("Can't make a path to a wall!");
                }
            }
        }
    }
    
    private int _currentNodeIndex = 0;
    private void MovePlayer(List<Node> path)
    {
        _lastPosition = cells.WorldToCell(transform.position);

        if(_currentNodeIndex >= path.Count)
        {
            _currentNodeIndex = 0;
            _isMoving = false;
            OnPlayerMoveComplete();
            return;
        }

        Vector3Int logicalPosition = new Vector3Int(path[_currentNodeIndex].Position.x,path[_currentNodeIndex].Position.y,0);
        Vector3 realPosition = cells.GetCellCenterWorld(logicalPosition);
        _moveDirection = GetMovingDirection(_lastPosition, logicalPosition);
        _lastPosition = logicalPosition;
        _currentNodeIndex++;
        // transform.DOLocalMove(new Vector3(realPosition.x, realPosition.y+0.3f, realPosition.z), 0.15f).OnComplete(()=>{
        //     MovePlayer(path);
        // });
    }

    public void OnPlayerMoveComplete()
    {
        Collider2D[] overlapedCols = Physics2D.OverlapCircleAll(new Vector3(transform.position.x, transform.position.y-0.3f, transform.localPosition.z), 0.2f);
        foreach(Collider2D col in overlapedCols)
        {
            // PVPZoneInteractable pvp = col.GetComponent<PVPZoneInteractable>();
            // if(pvp != null)
            // {
            //     PathfinderDisableInteraction();
            //     pvp.OnEnterPvpZone(PathfinderEnableInteraction);
            // }

            // ShopZoneInteractable shop = col.GetComponent<ShopZoneInteractable>();
            // if(shop != null)
            // {
            //     PathfinderDisableInteraction();
            //     shop.OpenShop(()=>{
            //         GetComponent<AvatarSkin>().RefreshSkin();
            //         PathfinderEnableInteraction();
            //     });
            // }
        }
    }

    public Vector3 GetMovingDirection(Vector3Int current,  Vector3Int next)
    {
        Vector3 currentCell = new Vector3Int(current.x, current.y, current.z);
        Vector3 nextCell = new Vector3Int(next.x, next.y, next.z);
        Vector3 direction = nextCell - currentCell;

        return direction;
    }

    /// <summary>
    /// Rounds a Vector3 to a point on the grid
    /// </summary>    
    private Vector2Int RoundToLogicalPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
    }
}
