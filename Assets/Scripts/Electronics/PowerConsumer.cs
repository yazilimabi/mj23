using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class PowerConsumer : MonoBehaviour
{
    [SerializeField] List<PowerGenerator> generators = new List<PowerGenerator>();
    enum LogicEnum{
        AND,
        OR,
        XOR
    } 
    [SerializeField] LogicEnum LogicMode;
    [SerializeField] bool Reversed = false;
    protected bool currentState = false;
    void Start()
    {
        UpdateState(true);
    }

    public void UpdateState(bool forceUpdate = false){
        bool tempState = LogicMode == LogicEnum.AND;
        foreach(PowerGenerator generator in generators){
            switch(LogicMode){
                case LogicEnum.AND:
                    tempState &= generator.state;
                break;
                case LogicEnum.OR:
                    tempState |= generator.state;
                break;
                case LogicEnum.XOR:
                    tempState ^= generator.state;
                break;
            }
        }

        if(forceUpdate || tempState != currentState){
            currentState = tempState;

            if(currentState == !Reversed) 
                TurnOn();
            else 
                TurnOff();
        }
    }

    public virtual void TurnOn() {
        throw new NotImplementedException();
    }
    public virtual void TurnOff() {
        throw new NotImplementedException();
    }
}
