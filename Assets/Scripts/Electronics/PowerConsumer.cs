using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerConsumer : MonoBehaviour
{
    List<PowerGenerator> generators = new List<PowerGenerator>();
    [SerializeField] bool OR_Mode = false;
    [SerializeField] protected bool currentState = false;
    void Start()
    {
        UpdateState();
        if(currentState) TurnOn();
    }

    public void UpdateState(){
        bool tempState = !OR_Mode;
        foreach(PowerGenerator generator in generators){
            if(OR_Mode){
                tempState = tempState || generator.state;
            }else{
                tempState = tempState && generator.state;
            }
        }
    }

    public virtual void TurnOn() {
        throw new NotImplementedException();
    }
    public virtual void TurnOff() {
        throw new NotImplementedException();
    }
}
