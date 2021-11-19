using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSkin : MonoBehaviour
{
    private Dictionary<string, Sprite> skinSpritesheet;
    private Dictionary<string, Sprite> hairSpritesheet;
    private Dictionary<string, Sprite> eyesSpritesheet;
    private Dictionary<string, Sprite> shirtSpritesheet;
    private Dictionary<string, Sprite> pantsSpritesheet;
    private Dictionary<string, Sprite> shoesSpritesheet;

    public List<int> ownedSkins = new List<int>();
    public List<int> ownedHairs = new List<int>();
    public List<int> ownedEyes = new List<int>();
    public List<int> ownedShirts = new List<int>();
    public List<int> ownedPants = new List<int>();
    public List<int> ownedShoes = new List<int>();

    private int _skinIndex = 4;
    private int _hairIndex = 4;
    private int _eyesIndex = 4;
    private int _shirtIndex = 4;
    private int _pantsIndex = 4;
    private int _shoesIndex = 4;

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
            _skinIndex = (int)table["product_index"];
            ownedSkins.Add(_skinIndex);
        }
    }

    private void ChangeShirt(Hashtable table)
    {
    	shirtSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            _shirtIndex = (int)table["product_index"];
            ownedShirts.Add(_shirtIndex);
        }
    }
    
    private void ChangePants(Hashtable table)
    {
    	pantsSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            _pantsIndex = (int)table["product_index"];
            ownedPants.Add(_pantsIndex);
        }
    }

    private void ChangeShoes(Hashtable table)
    {
    	shoesSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            _shoesIndex = (int)table["product_index"];
            ownedShoes.Add(_shoesIndex);
        }
    }

    private void ChangeHair(Hashtable table)
    {
    	hairSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            _hairIndex = (int)table["product_index"];
            ownedHairs.Add(_hairIndex);
        }
    }

    private void ChangeEyes(Hashtable table)
    {
    	eyesSpritesheet = (Dictionary<string, Sprite>)table["spritesheet"];

        if(table["purshased"] != null)
        {
            _eyesIndex = (int)table["product_index"];
            ownedEyes.Add(_eyesIndex);
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

        sprites = ResourcesManager.Instance.LoadSkinIndex(Slots.skin.ToString(), _skinIndex);
    	skinSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.LoadSkinIndex(Slots.front_hair.ToString(), _hairIndex);
    	hairSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.LoadSkinIndex(Slots.eyes.ToString(), _eyesIndex);
    	eyesSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.LoadSkinIndex(Slots.shirt.ToString(), _shirtIndex);
    	shirtSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.LoadSkinIndex(Slots.pants.ToString(), _pantsIndex);
    	pantsSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    	sprites = ResourcesManager.Instance.LoadSkinIndex(Slots.shoes.ToString(), _shoesIndex);
    	shoesSpritesheet = sprites.ToDictionary(x => x.name, x => x);

    }
}
