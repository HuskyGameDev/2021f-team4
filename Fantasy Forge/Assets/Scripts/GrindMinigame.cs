using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GrindMinigame : MonoBehaviour
{
    public GameObject    grindStone;    // Object to be rotated as visual representation of grindstone speed
    public RectTransform grindArea;     // Region where blade is positioned to be grinded
    public ChangeShape   swordShape;    // Object moved against edge of grindstone to be shaped

    public float speedCap;          // Maximum rotation speed of grindstone in degrees/second
    public float scrollPower;       // Degrees/second added to speed each scroll
    public float friction;          // Degrees/second lost every second
    public float grindDist;         // Distance/seconds moved by points when ground at full speed
    public float _bladeBoundLeft;   // Left boundary of blade X position in grindArea local space
    public float _bladeBoundRight;  // Right boundary of "   "
    public float flipCooldownTime;  // Minimum time allowed between flips in seconds
    public float acceptanceThreshold;

    private float  _speed;           // Degrees rotated by grindstone every second
    private float  _grindBoundX;     // Leftmost extent of grind area centered at 0
    private float  _grindBoundY;     // Rightmost "   "
    private bool   _flipReady;       // Indicates whether or not sufficient cooldown time has passed since last flip
    private int    _orientation;
    private int    _verticesComplete;
    private bool[] _grindComplete;   // Indicates when each point has been ground below acceptanceThreshold
    private InventoryItem _inputItem;

    // Start is called before the first frame update
    void Start()
    {
        _speed       = 0;
        _grindBoundX = grindArea.rect.width  / 2;
        _grindBoundY = grindArea.rect.height / 2;
        _flipReady   = true;
        _orientation = 0;
        _verticesComplete = 0;

        _inputItem = GetComponentInParent<Prompt>().inputItem;
        //Debug.Log("Input to grindstone is " + _inputItem.itemState + " " + _inputItem.metalType);

        /*
        if (inputObject != null)
        {
            swordShape.setShape(inputObject.GetComponent<SpriteShapeController>());
            Destroy(inputObject);
        }*/

        _grindComplete = new bool[swordShape.numPoints()];

        for (int i = 0; i < swordShape.numPoints(); i++)
        {
            _grindComplete[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotateGrindstone();
        moveBlade();
        if (Input.GetAxis("Space") > 0 && _flipReady)
        {
            swordShape.transform.Rotate(Vector3.forward * 180);
            if (_orientation == 0)
                _orientation = 1;
            else
                _orientation = 0;
            StartCoroutine("FlipCooldown");
        }

        // Move/"grind" points when in grind space
        for (int i = 0; i < swordShape.numPoints(); i++)
        {
            if (!_grindComplete[i])
            {
                Vector2 worldPos = swordShape.transform.TransformPoint(swordShape.getPoint(i)); // Position of point in world space
                Vector2 areaPos = grindArea.transform.InverseTransformPoint(worldPos);          // Position of point in local space of grind area

                if (-_grindBoundX < areaPos.x && areaPos.x < _grindBoundX &&
                    -_grindBoundY < areaPos.y && areaPos.y < _grindBoundY)
                {
                    Vector2 distRemaining = swordShape.movePoint(i, (_speed / speedCap) * Time.deltaTime * grindDist);

                    if (distRemaining.magnitude < acceptanceThreshold)
                    {
                        _grindComplete[i] = true;
                        _verticesComplete++;
                    }
                }
            }
        }

        if (_verticesComplete >= swordShape.numPoints())
        {
            _inputItem.itemState = ItemState.Blade;
            //Debug.Log("Output from grindstone is " + _inputItem.itemState + " " + _inputItem.metalType);

            GetComponent<Prompt>().promptingInteractable.closePrompt();
            
        }
        
    }

    // Take mousewheel input, adjust grindstone speed, and rotate accordingly
    void rotateGrindstone()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");  // Direction/magnitude scrolled

        // Slow down grindstone w/ constant
        _speed -= friction * Time.deltaTime;

        // Speed up grindstone if scroll wheel moved in negative/downwards direction
        if (scroll < 0)
        {
            _speed += -1 * scrollPower * scroll;
        }

        // Clamp speed between stopped-speed cap after acceleration/decelleration
        _speed = Mathf.Clamp(_speed, 0, speedCap);

        //Debug.Log(_speed);

        // Rotate grindstone & edge
        //grindStone.transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
        grindStone.transform.Rotate(Vector3.up, _speed * Time.deltaTime);
    }

    void moveBlade()
    {
        // Calculate position of mouse in local space of grind area
        Vector3 mousePosScreen = Input.mousePosition;                               // Location of mouse on screen at time of click
        Vector3 mousePosWorld  = Camera.main.ScreenToWorldPoint(mousePosScreen);    // Location of mouse click in world space
        Vector3 mousePosLocal  = grindArea.InverseTransformPoint(mousePosWorld);    // Location of mouse click in local space of grind area

        // Move blade to mouse position within bounds
        swordShape.transform.position = new Vector3(
            Mathf.Clamp(mousePosLocal.x, _bladeBoundLeft, _bladeBoundRight) - (_bladeBoundRight * _orientation),
            swordShape.transform.position.y,
            swordShape.transform.position.z);
    }

    IEnumerator FlipCooldown()
    {
        _flipReady = false;
        yield return new WaitForSeconds(flipCooldownTime);
        _flipReady = true;
    }
}
