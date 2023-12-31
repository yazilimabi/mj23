using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PowerBox : PowerGenerator
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Light2D globalLight;
    [SerializeField] PowerGenerator disableLever;
    [SerializeField] PowerGenerator activateLever;
    [SerializeField] GameObject extraVoid;
    [SerializeField] bool ikinci = false;


    public override void UpdateState(){
        
    }
    
    public void Explode(){
        if(state) return;
        AudioManager.Instance.triggerAudio(AudioManager.AudioTypes.PowerboxBoom);
        SetState(true);
        if(!ikinci){
            AudioManager.Instance.stopAudio(AudioManager.AudioTypes.AlarmFadeIn);
            AudioManager.Instance.stopAudio(AudioManager.AudioTypes.AlarmContinous);
            globalLight.intensity = 0.2f;

            var rooms = GameObject.FindGameObjectsWithTag("Room");

            foreach (GameObject room in rooms)
            {
                room.GetComponent<Room>().music = MusicManager.Musics.Ambient;
            }

            MusicManager.Instance.PlayMusic(MusicManager.Musics.Ambient);
            disableLever.SetState(false);
            activateLever.SetState(true);
            extraVoid.SetActive(true);

            GameManager.Instance.IsPowerOn = true;
        }
    }
}
