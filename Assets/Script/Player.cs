using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region ---Public Members---
	[SerializeField] private Animator playerAnimator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float slideMinDistance;
	#endregion

	#region ---Private Members---
	/* User touch variables */
	private Vector2 touchStartPosition;
    private Vector2 previousTouchPosition;
    private bool isSliding = false;

    /* Screen boundary variable */
    private float minX;
    private float maxX;

	#endregion


	void Awake()
    {
		CalcPlayerSizeAndScreen();

	}

    void Update()
    {
        RunIdle();
        AutoAttack();
        PlayerInput();
    }

    private void CalcPlayerSizeAndScreen()
    {
		float distance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
		Vector3 leftScreenBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightScreenBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            float halfWidth = sr.bounds.size.x / 2f;
            minX = leftScreenBound.x + halfWidth;
            maxX = rightScreenBound.x - halfWidth;
        }
	}

    /// <summary>
    /// Play Animation to Run Upside
    /// </summary>
    private void RunIdle()
    {
        
    }

    /// <summary>
    /// Player Automatically Attack Monster When Monster Get Closer
    /// </summary>
    private void AutoAttack()
    {

    }

	/// <summary>
	/// Slide Touch to Player Side Move, 
    /// Point Touch to Critical Attack
	/// </summary>
	private void PlayerInput()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
					touchStartPosition = touch.position;
					previousTouchPosition = touch.position;
                    isSliding = false;
					break;

                case TouchPhase.Moved:
                    if(!isSliding)
                    {
                        if(Vector2.Distance(touchStartPosition, touch.position) >= slideMinDistance)
                            isSliding = true;                   
                    }
					if (isSliding)
					{
						Debug.Log("Player Touch Moved!");
						float deltaX = touch.position.x - previousTouchPosition.x;
						previousTouchPosition = touch.position;
						PlayerMove(deltaX);
					}
					break;

                case TouchPhase.Ended:
                    if(!isSliding)
                    {
                        Debug.Log("Player Point Touch!");
                        if (playerAnimator != null)
                            playerAnimator.SetTrigger("isAttack");
					}
					else Debug.Log("Player Touch Slide End");
					break;

                default: break;
            }
        }


	}

	/// <summary>
	/// Methods that directly handle player movement
	/// </summary>
	private void PlayerMove(float deltaX)
    {
        float moveAmount = deltaX * moveSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + new Vector3(moveAmount, 0, 0);
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        transform.position = newPos;
	}
}
