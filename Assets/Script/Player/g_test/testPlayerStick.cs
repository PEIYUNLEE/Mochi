﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerStick : MonoBehaviour
{
    public bool getIsOnFloor;
    // Use this for initialization
    [SerializeField]
    public bool isStick { get { return m_isStick; } }

    [SerializeField]
    private bool m_isStick;  //黏的狀態

    [SerializeField]
    private bool canStick;  //有碰到東西可以黏
    private UnityJellySprite jellySprite;

    [SerializeField]
    private bool isAttachItem;  //有碰到Item

    [SerializeField]
    public List<GameObject> stickItemList;  //黏住的角色

    [SerializeField]
    private bool isAttachPlayer;  //有碰到角色

    [SerializeField]
    public List<GameObject> stickPlayerList;  //黏住的角色

    [SerializeField]
    private bool isAttachWall;  //有碰到wall or floor
    public enum DETECTTYPE
    {
        NONE,
        STICKHEAVY,
        STICKLIGHT
    }
    // public DETECTTYPE detectType{ get {return m_detectType;} } 
    DETECTTYPE m_detectType = DETECTTYPE.NONE;

    testPlayerMovement testPlayerMovement;

    bool isTouchPlayer;
    bool isTouchWall;
    bool isTouchItem;

    void Start()
    {
        jellySprite = gameObject.GetComponentInParent<UnityJellySprite>();
        testPlayerMovement = gameObject.GetComponentInParent<testPlayerMovement>();
    }
    void Update()
    {
        // Input.GetButtonDown("Stick_" + this.tag)
        // if ((Input.GetKeyDown("x") && testPlayerMovement.testType) || (Input.GetKeyDown("g") && !testPlayerMovement.testType))
        if ((Input.GetKeyDown("g") && testPlayerMovement.testType) || (Input.GetButtonDown("Stick_" + this.tag) && !testPlayerMovement.testType))
        {
            if (canStick && !m_isStick)
            {
                jellySprite.isStick = true;
                m_isStick = true;
            }
            else if (m_isStick)
            {
                m_isStick = false;
                ResetNotStick_Normal();
            }
        }


        if (m_isStick)
        {
            if (isTouchItem || isTouchWall)
                ItemToStick();
            if (isTouchPlayer)
                PlayerToStick();
        }


        isAttachItem = jellySprite.GetIsItemAttach();

        isAttachPlayer = jellySprite.GetIsPlayerAttach();

        isAttachWall = jellySprite.GetIsFloorOrWallAttach();

        if (getIsOnFloor || isAttachWall || isAttachPlayer || isAttachItem)
        {
            canStick = true;
        }
        else if (!getIsOnFloor && !isAttachItem && !isAttachPlayer && !isAttachWall)
        {
            canStick = false;
        }
    }

    private void ResetNotStick_Normal()
    {
        ResetItemNotStick();

        stickPlayerList = null;
        jellySprite.ResetPlayerStick();

        jellySprite.ResetFloorOrWallStick();
    }

    private void ItemToStick()
    {

        stickItemList = jellySprite.SetItemStick();


        jellySprite.SetFloorOrWallStick();
    }

    private void PlayerToStick()
    {

        stickPlayerList = jellySprite.SetPlayerStick();
    }


    public void ResetItemNotStick()
    {
        stickItemList = null;
        jellySprite.ResetItemStick();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != this.tag && other.gameObject.name != "floorDetect")
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("thing"))
            {
                isTouchItem = true;
            }
            else if (other.gameObject.tag == "ground" || other.gameObject.tag == "wall")
            {
                isTouchWall = true;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                isTouchPlayer = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != this.tag && other.gameObject.name != "floorDetect")
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("thing"))
            {
                isTouchItem = false;
            }
            else if (other.gameObject.tag == "ground" || other.gameObject.tag == "wall")
            {
                isTouchWall = false;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                isTouchPlayer = false;
            }
        }
    }
}
