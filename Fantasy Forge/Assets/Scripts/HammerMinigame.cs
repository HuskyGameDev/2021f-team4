using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class HammerMinigame : MonoBehaviour
{
    public float hammerRadius;          // Radius of hammer hit around mouse click where points are moved
    public float hitDistanceMultiplier; // Proportion of hammer radius that is the maximum distance a point can move when hit
    public float hitCooldownTime;       // Minimum time allowed between hits in seconds
    public float targetScale;           // Amount X targets are scaled by to allow free-form shaping.

    private ChangeShape _swordShape;    // ChangeShape of this HameObject/sword being formed
    private bool _hitReady;             // Indicates whether or not sufficient cooldown time has passed since last hit
    private bool[] _hammerComplete;     // Indicates when each point has been ground below acceptanceThreshold
    private int _verticesComplete;

    // Start is called before the first frame update
    void Start()
    {
        _swordShape = GetComponent<ChangeShape>();
        _hitReady = true;

        _hammerComplete = new bool[_swordShape.numPoints()];
        _verticesComplete = 0;

        for (int i = 0; i < _swordShape.numPoints(); i++)
        {
            _hammerComplete[i] = false;
        }

        //_swordShape.addTargetBuffer(targetBuffer);
        //_swordShape.scaleTargetBuffer(targetScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (_hitReady)
        {
            // Calculate position of mouse click in local space
            Vector3 mousePosScreen = Input.mousePosition;                           // Location of mouse on screen at time of click
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen); // Location of mouse click in world space
            Vector3 mousePosSword = _swordShape.transform.InverseTransformPoint(mousePosWorld); // Location of mouse click in local space of sword GameObject

            // Move all points near click towards their target
            for (int i = 0; i < _swordShape.numPoints(); i++)
            {
                if (!_hammerComplete[i])
                {
                    float distFromClick = Vector2.Distance(_swordShape.getPoint(i), mousePosSword); // Distance between point on sword and mouse click
                    Vector3 distRemaining;

                    // If point is within hammer hit radius, move it towards its target
                    if (distFromClick <= hammerRadius)
                    {
                        distRemaining = _swordShape.movePoint(i, (hammerRadius - distFromClick) * hitDistanceMultiplier);

                        if (Mathf.Abs(distRemaining.magnitude) <= .1) // (targetScale - 1) / 4.0)
                        {
                            Debug.Log("Vertex " + i +  " complete");
                            _hammerComplete[i] = true;
                            _verticesComplete++;
                        }
                    }
                }
            }

            if (_verticesComplete >= _swordShape.numPoints())
            {
                Debug.Log("COMPLETE");

                //Instantiate(_swordShape.getSpriteShape()).tag = "hammerout";

                GetComponentInParent<Prompt>().promptingInteractable.closePrompt();
            }

            StartCoroutine("HitCooldown");
        }
    }

    IEnumerator HitCooldown()
    {
        _hitReady = false;
        yield return new WaitForSeconds(hitCooldownTime);
        _hitReady = true;
    }
}
