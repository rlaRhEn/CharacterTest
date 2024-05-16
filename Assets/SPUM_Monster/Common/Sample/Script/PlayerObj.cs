﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyEvent : UnityEvent<PlayerObj.PlayerState>
{

}

[ExecuteInEditMode]
public class PlayerObj : MonoBehaviour
{
    public SPUM_Prefabs _prefabs;
    public float _charMS;
    public enum PlayerState
    {
        idle,
        run,
        attack,
        death,
    }
    public PlayerState _currentState;
    public PlayerState CurrentState{
        get => _currentState;
        set {
            _stateChanged.Invoke(value);
            _currentState = value;
        }
    }
   
    private MyEvent _stateChanged = new MyEvent();

    public Vector3 _goalPos;
    void Start()
    {
        if(_prefabs == null )
        {
            _prefabs = transform.GetChild(0).GetComponent<SPUM_Prefabs>();
        }
        
        _stateChanged.AddListener(PlayStateAnimation);


    }
    private void PlayStateAnimation(PlayerState state){
        _prefabs.PlayAnimation(state.ToString());
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.localPosition.y * 0.01f);
        switch(_currentState)
        {
            case PlayerState.idle:
            break;

            case PlayerState.run:
            DoMove();
            break;
        }


    }

    void DoMove()//목표 지점으로 이동
    {
        Vector3 _dirVec  = _goalPos - transform.position ;
        Vector3 _disVec = (Vector2)_goalPos - (Vector2)transform.position ;
        if( _disVec.sqrMagnitude < 0.1f )
        {
            _currentState = PlayerState.idle;
            PlayStateAnimation(_currentState);
            return;
        }
        Vector3 _dirMVec = _dirVec.normalized;
        transform.position += (_dirMVec * _charMS * Time.deltaTime );
        

        if(_dirMVec.x > 0 ) _prefabs.transform.localScale = new Vector3(-1,1,1);
        else if (_dirMVec.x < 0) _prefabs.transform.localScale = new Vector3(1,1,1);
    }

    public void SetMovePos(Vector2 pos)
    {
        _goalPos = pos;
        _currentState = PlayerState.run;
        PlayStateAnimation(_currentState);
    }
}
