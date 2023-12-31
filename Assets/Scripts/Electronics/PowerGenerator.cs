using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    [SerializeField] List<PowerConsumer> consumers = new List<PowerConsumer>();
    
    public bool state = false;
    void Awake(){
        UpdateState();
        foreach(PowerConsumer consumer in consumers){
            consumer.RegisterGenerator(this);
        }
        UpdateConsumerStates();
    }

    public void SetState(bool newState){
        if(newState != state){
            state = newState;
            
            UpdateState();
            UpdateConsumerStates();
        }
    }
    void UpdateConsumerStates(){
        foreach(PowerConsumer consumer in consumers){
            consumer.UpdateState();
        }
    }

    public virtual void UpdateState(){
        throw new NotImplementedException();
    }
}
