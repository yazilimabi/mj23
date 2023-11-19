using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
public class GunAltar : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float fadeTime = 2f;
    [SerializeField] bool tookGun = false;
    [SerializeField] PowerBox powerBox;

    private YieldInstruction fadeInstruction = new YieldInstruction();
        IEnumerator FadeOut()
        {
            float elapsedTime = 0.0f;
            Color c = image.color;
            while (elapsedTime < fadeTime)
            {
                yield return fadeInstruction;
                elapsedTime += Time.deltaTime ;
                c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
                image.color = c;
            }
        }

    IEnumerator Cutscene(){
        image.color = Color.black;
        AudioManager.Instance.triggerAudio(AudioManager.AudioTypes.GlassBreaking);
        GameManager.Instance.player.GetComponent<PlayerMovement>().DisableMovement();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.player.GetComponent<GunController>().EnableGun();
        GameManager.Instance.player.GetComponent<PlayerMovement>().EnableMovement();
        AudioManager.Instance.triggerAudio(AudioManager.AudioTypes.AlarmFadeIn);
        powerBox.SetState(false);
        yield return new WaitForSeconds(3.3f);
        AudioManager.Instance.stopAudio(AudioManager.AudioTypes.AlarmFadeIn);
        AudioManager.Instance.triggerAudio(AudioManager.AudioTypes.AlarmContinous, true);
        
        StartCoroutine(FadeOut());
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !tookGun){
            tookGun = true;
            StartCoroutine(Cutscene());
        }
    }
}
