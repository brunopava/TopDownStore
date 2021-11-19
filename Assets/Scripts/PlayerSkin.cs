using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/**


TODO: 
    - FEMALE SPRITES
    - ANIMATION IDDLE
**/

public class PlayerSkin : MonoBehaviour
{
    private Dictionary<string, Sprite> skinSpritesheet;
    private Dictionary<string, Sprite> hairSpritesheet;
    private Dictionary<string, Sprite> eyesSpritesheet;
    private Dictionary<string, Sprite> shirtSpritesheet;
    private Dictionary<string, Sprite> pantsSpritesheet;
    private Dictionary<string, Sprite> shoesSpritesheet;

    public SpriteRenderer skin;
    public SpriteRenderer hair;
    public SpriteRenderer eyes;
    public SpriteRenderer shirt;
    public SpriteRenderer pants;
    public SpriteRenderer shoes;

    private List<Animator> _animators = new List<Animator>();

    private Player _player;

    private void Awake()
    {
    	_player = GetComponent<Player>();
    	_animators.Add(skin.GetComponent<Animator>());
    	_animators.Add(hair.GetComponent<Animator>());
    	_animators.Add(eyes.GetComponent<Animator>());
    	_animators.Add(shirt.GetComponent<Animator>());
    	_animators.Add(pants.GetComponent<Animator>());
    	_animators.Add(shoes.GetComponent<Animator>());
    	
    	NotificationCenter.DefaultCenter.AddObserver(this, "ChangeSkin");
    	NotificationCenter.DefaultCenter.AddObserver(this, "ChangeShirt");
    	NotificationCenter.DefaultCenter.AddObserver(this, "ChangePants");
    	NotificationCenter.DefaultCenter.AddObserver(this, "ChangeEyes");
    	NotificationCenter.DefaultCenter.AddObserver(this, "ChangeShoes");
    	NotificationCenter.DefaultCenter.AddObserver(this, "ChangeHair");
        NotificationCenter.DefaultCenter.AddObserver(this, "ResetSkin");

        NotificationCenter.DefaultCenter.AddObserver(this, "DisableMovement");
    }

    private void Start()
    {
        LoadPlayerSkin();
    }

    private void ResetSkin()
    {
        LoadPlayerSkin();
    }

    private void DisableMovement()
    {
        _lastDirection = "walk-bot";
        SetAnimationState("idle-bot");
    }

    private void ChangeSkin(Hashtable table)
    {
    	skinSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            InventoryManager.Instance.AddItem(Constants.skin, (int)table["product_index"]);
        }
    }

    private void ChangeShirt(Hashtable table)
    {
    	shirtSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            InventoryManager.Instance.AddItem(Constants.shirt, (int)table["product_index"]);
        }
    }
    
    private void ChangePants(Hashtable table)
    {
    	pantsSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            InventoryManager.Instance.AddItem(Constants.pants, (int)table["product_index"]);
        }
    }

    private void ChangeShoes(Hashtable table)
    {
    	shoesSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            InventoryManager.Instance.AddItem(Constants.shoes, (int)table["product_index"]);
        }
    }

    private void ChangeHair(Hashtable table)
    {
    	hairSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            InventoryManager.Instance.AddItem(Constants.front_hair, (int)table["product_index"]);
        }
    }

    private void ChangeEyes(Hashtable table)
    {
    	eyesSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            InventoryManager.Instance.AddItem(Constants.eyes, (int)table["product_index"]);
        }
    }

    private string _lastDirection;
    private void LateUpdate()
    {
    	Vector3 movementVector = _player.movementVector;
        string movementDirection = "";

        if(_player.canMove)
        {
            movementDirection = CheckDirection(movementVector);
            SetAnimationState(_lastDirection);
        }else{
            SetAnimationState(CheckLastDirection(movementVector));
        }

        _lastDirection = movementDirection;

        skin.sprite = skinSpritesheet[skin.sprite.name];
        hair.sprite = hairSpritesheet[hair.sprite.name];
        eyes.sprite = eyesSpritesheet[eyes.sprite.name];
        shirt.sprite = shirtSpritesheet[shirt.sprite.name];
        pants.sprite = pantsSpritesheet[pants.sprite.name];
        shoes.sprite = shoesSpritesheet[shoes.sprite.name];

    }

    private void SetAnimationState(string animationState)
    {
    	foreach(Animator current in _animators)
    	{
    		current.Play(animationState);
    	}
    }

    private string CheckLastDirection(Vector3 movementVector)
    {
        string direction = "";
    	switch(_lastDirection)
        {
            case("walk-top-right"):
                direction = "idle-top-right";
            break;
            case("walk-bot-right"):
                direction = "idle-bot-right";
            break;
            case("walk-top-left"):
                direction = "idle-top-left";
            break;
            case("walk-bot-left"):
                direction = "idle-bot-left";
            break;

            case("walk-right"):
                direction = "idle-right";
            break;
            case("walk-top"):
                direction = "idle-top";
            break;
            case("walk-left"):
                direction = "idle-left";
            break;
            case("walk-bot"):
                direction = "idle-bot";
            break;

        }
    	return direction;
    }


    private string CheckDirection(Vector3 movementVector)
    {
    	string direction = "";
    	bool isIdle = false;
    	
    	if(movementVector.x > 0 && movementVector.y > 0)
    	{
    		direction = "walk-top-right";
    	}else if(movementVector.x < 0 && movementVector.y > 0)
    	{
    		direction = "walk-top-left";
    	}else if(movementVector.x < 0 && movementVector.y < 0)
    	{
    		direction = "walk-bot-left";
    	}else if(movementVector.x > 0 && movementVector.y < 0)
    	{
    		direction = "walk-bot-right";
    	}
    	else if(movementVector.x == 0 && movementVector.y == 0)
    	{
    		direction = "idle";
    	}else if(movementVector.x > 0 && movementVector.y == 0)
    	{
    		direction = "walk-right";
    	}else if(movementVector.x < 0 && movementVector.y == 0)
    	{
    		direction = "walk-left";
    	}else if(movementVector.x == 0 && movementVector.y > 0)
    	{
    		direction = "walk-top";
    	}else if(movementVector.x == 0 && movementVector.y < 0)
    	{
    		direction = "walk-bot";
    	}

    	if(direction == "idle")
    	{
    		direction = CheckLastDirection(movementVector);
    	}

    	return direction;
    }

    private void LoadPlayerSkin()
    {
    	Sprite[] sprites;

        sprites = ResourcesManager.Instance.GetSpritesheet(Slots.skin.ToString(), InventoryManager.Instance.equipedSkinIndex);
    	skinSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.GetSpritesheet(Slots.front_hair.ToString(), InventoryManager.Instance.equipedHairIndex);
    	hairSpritesheet = sprites.ToDictionary(x => x.name, x => x);
        
    	sprites = ResourcesManager.Instance.GetSpritesheet(Slots.eyes.ToString(), InventoryManager.Instance.equipedEyesIndex);
    	eyesSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.GetSpritesheet(Slots.shirt.ToString(), InventoryManager.Instance.equipedShirtIndex);
    	shirtSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.GetSpritesheet(Slots.pants.ToString(), InventoryManager.Instance.equipedPantsIndex);
    	pantsSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.GetSpritesheet(Slots.shoes.ToString(), InventoryManager.Instance.equipedShoesIndex);
    	shoesSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    }
}
