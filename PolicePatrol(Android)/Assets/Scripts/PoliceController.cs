﻿using System.Collections;
using UnityEngine;

public class PoliceController : MonoBehaviour 
{
    public enum PlayerState
	{
		ATTACK,
		BLEND,
		CROUCH,
		CROUCHMOVE,
		DEAD,
		DYING,
		IDLE,
		RUN,
		SLAP,
		WALK,
	};
	
	public PlayerState m_currentState;
	public PlayerState m_previousState;

    bool m_isFacingRight , m_isMovingLeft , m_isMovingRight , m_registeredInputEvents , m_tapped , m_tappedLeft , m_tappedRight , m_touchReleased;
    public bool m_isBlending , m_coverBlown , m_isDoingNothing , m_isRunning , m_isVisible , m_isWalking;
    [SerializeField] float m_walkSpeed , m_runSpeed;
    float m_xInput = 0f;
    [SerializeField] GameObject m_blendMeterObj;
    Rigidbody2D m_copBody2D;
    [SerializeField] SpriteRenderer m_copRenderer;
    [SerializeField] SriTouchInputListener m_touchInputListener;
    Thug m_thugController;
    Vector2 m_firstTouchPosition;

    void Reset()
    {
        m_runSpeed = 2.0f;
        m_walkSpeed = 1.25f;
    }

    void Start()
    {
        m_blendMeterObj.SetActive(false);
        m_copBody2D = GetComponent<Rigidbody2D>();
        m_copRenderer = GetComponent<SpriteRenderer>();
        m_isBlending = false;
        m_isDoingNothing = true;
        m_isFacingRight = true;
        m_isMovingLeft = false;
        m_isMovingRight = true;
        m_isRunning = false;
        m_isWalking = false;
        RegisterEvents();
        SetState(PlayerState.IDLE);
        m_thugController = FindObjectOfType<Thug>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        m_coverBlown = m_thugController.m_thugChasing;
        m_isRunning = m_coverBlown;
        m_isWalking = !m_coverBlown;

        UpdateAnimations();
        UpdateStateMachine();
    }

    void Attack()
    {

    }

    void Blend()
    {
        m_blendMeterObj.SetActive(true);
        m_copBody2D.velocity = new Vector2(0f , m_copBody2D.velocity.y);   
        m_isDoingNothing = false;
    }

    void Crouch()
    {

    }

    void CrouchWalk()
    {

    }

    void Death()
    {

    }

    void Dying()
    {

    }

    void Flip()
	{
		m_isFacingRight = !m_isFacingRight;
		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    PlayerState GetState()
	{
		return m_currentState;
	}

    void Idle()
    {
        m_isDoingNothing = true;
        m_isRunning = false;
        m_isWalking = false;
        m_copBody2D.velocity = new Vector2(0f , m_copBody2D.velocity.y);
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    void OnTapped(TouchInfo touchInfo)
    {
        Debug.Log("Tapped");

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		m_firstTouchPosition = new Vector2(screenToWorld.x , screenToWorld.y);

        if(screenToWorld.x < m_copBody2D.position.x)
        {
            //Debug.Log("Left to Constable");
            m_isMovingLeft = true;
            m_isMovingRight = false;
        }

        else if(screenToWorld.x > m_copBody2D.position.x)
        {
           //Debug.Log("Right to Constable");
           m_isMovingLeft = false;
            m_isMovingRight = true;
        }

        if(!m_isBlending)
        {
            if(!m_coverBlown)
            {
                if(m_isDoingNothing)
                {
                    SetState(PlayerState.WALK);
                }

                else if(m_isWalking)
                {
                    SetState(PlayerState.IDLE);
                }
            }

            else if(m_coverBlown)
            {
                if(m_isDoingNothing)
                {
                    SetState(PlayerState.RUN);
                }

                else if(m_isRunning)
                {
                    SetState(PlayerState.IDLE);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col2DEnter)
    {
        if(col2DEnter.gameObject.tag.Equals("Light"))
        {
            m_isVisible = true;
            m_copRenderer.color = Color.red;
        }

        if(col2DEnter.gameObject.tag.Equals("Statue"))
        {
            if(m_isRunning)
            {
                m_isBlending = true;
                m_isRunning = false;
                m_isWalking = false;
                SetState(PlayerState.BLEND);   
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col2DExit)
    {
        if(col2DExit.gameObject.tag.Equals("Light"))
        {
            m_isVisible = false;
            m_copRenderer.color = Color.white;
        }

        //if(col2DExit.gameObject.tag.Equals("Statue"))
        //{
        //    m_isBlending = false;
        //    SetState(PlayerState.IDLE);
        //}
    }

    void RegisterEvents()
    {
        if(!m_registeredInputEvents && m_touchInputListener)
		{
			m_touchInputListener.Tap += OnTapped;
            // Register more events later
			m_registeredInputEvents = true;
		}
    }

    void Run()
    {
        m_isDoingNothing = false;

        if(m_isRunning)
        {
            if(m_isMovingRight)
		    {
			    if(!m_isFacingRight)
			    {
				    Flip();
			    }
			
			    m_copBody2D.velocity = new Vector2(m_runSpeed , m_copBody2D.velocity.y);
		    }
		
		    else if(m_isMovingLeft)
		    {
			    if(m_isFacingRight)
			    {
				    Flip();
			    }
			
			    m_copBody2D.velocity = new Vector2(-m_runSpeed , m_copBody2D.velocity.y);
		    }
		
		    else
		    {
			    return;
		    }
        }
    }

    void SetState(PlayerState newState)
	{
		if (m_currentState == newState)
		{
			return;
		}
		
		m_previousState = m_currentState;
		m_currentState = newState;
	}

    void ThighSlap()
    {

    }

    public void TouchInput(TouchInfo touchInfo)
    {
        SriTouchGestures gesture = touchInfo.touchGesture;

        switch(gesture)
        {
            case SriTouchGestures.SRI_NONE:
                Debug.Log("Nothing Happened");
            break;

            case SriTouchGestures.SRI_SWIPEDLEFT:
                Debug.Log("Swiped Left");
            break;

            case SriTouchGestures.SRI_SWIPEDRIGHT:
                Debug.Log("Swiped Right");
            break;

            case SriTouchGestures.SRI_SWIPEDUP:
                Debug.Log("Swiped Up");
            break;

            case SriTouchGestures.SRI_SWIPEDDOWN:
                Debug.Log("Swiped Down");
            break;

            case SriTouchGestures.SRI_DOUBLETAPPED:
                Debug.Log("Double Tapped");
            break;

            case SriTouchGestures.SRI_TAPHELD:
                Debug.Log("Tap Held");
            break;

            case SriTouchGestures.SRI_RELEASED:
                Debug.Log("Touch Released");
                m_tapped = false;
            break;

            case SriTouchGestures.SRI_TAPPED:
                Debug.Log("Tapped");
                m_tapped = true;
            break;
        }
    }

    void UnregisterEvents()
    {
        if(m_registeredInputEvents)
		{
			if(m_touchInputListener)
            {
                m_touchInputListener.Tap -= OnTapped;
            }
            
            // Unregister more events later
			m_registeredInputEvents = false;
		}
    }

    void Walk()
    {
        m_isDoingNothing = false;
        
		if(m_isWalking)
        {
            if(m_isMovingRight)
		    {
			    if(!m_isFacingRight)
			    {
				    Flip();
			    }
			
			    m_copBody2D.velocity = new Vector2(m_walkSpeed , m_copBody2D.velocity.y);
		    }
		
		    else if(m_isMovingLeft)
		    {
			    if(m_isFacingRight)
			    {
				    Flip();
			    }
			
			    m_copBody2D.velocity = new Vector2(-m_walkSpeed , m_copBody2D.velocity.y);
		    }
		
		    else
		    {
			    return;
		    }
        }
    }

    private void UpdateAnimations()
	{
		switch(m_currentState)
		{
			case PlayerState.ATTACK: break;
			case PlayerState.BLEND: break;
			case PlayerState.CROUCH: break;
			case PlayerState.CROUCHMOVE: break;
			case PlayerState.DEAD: break;
			case PlayerState.DYING: break;
			case PlayerState.IDLE: break;
			case PlayerState.RUN: break;
			case PlayerState.SLAP: break;
			case PlayerState.WALK: break;
		}
	}

    private void UpdateStateMachine()
    {
	    switch(m_currentState)
	    {
		    case PlayerState.ATTACK:
			    Attack();
		    break;

		    case PlayerState.BLEND:
			    Blend();
		    break;
			
		    case PlayerState.CROUCH:
			    Crouch();
		    break;
			
		    case PlayerState.CROUCHMOVE:
			    CrouchWalk();
		    break;
			
		    case PlayerState.DEAD: 
			    Death();
		    break;
			
		    case PlayerState.DYING: 
			    Dying();
		    break;
			
		    case PlayerState.IDLE: 
			    Idle();
		    break;
			
		    case PlayerState.RUN:
			    Run();
		    break;
			
		    case PlayerState.SLAP: 
			    ThighSlap();
		    break;
			
		    case PlayerState.WALK: 
			    Walk();
		    break;
	    }
    }
}
