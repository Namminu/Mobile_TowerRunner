using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
	#region ---Public Members---
	[SerializeField] private Animator playerAnimator;

    [SerializeField, Range(0, 10), Tooltip("플레이어 캐릭터 이동 속도")] 
    private float moveSpeed;
    [SerializeField, Range(0, 50), Tooltip("슬라이드 터치 최소 인지 범위")] 
    private float slideMinDistance;
    [SerializeField, Range(0, 30), Tooltip("플레이어의 양 사이드 간 최소 이격 거리")]
    private float sideDistance;
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

			switch (touch.phase)
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
        float newTarget = transform.position.x + moveAmount;
        newTarget = Mathf.Clamp(newTarget, minX + sideDistance, maxX - sideDistance);

        Vector3 targetPos = new Vector3(newTarget, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
	}
}