using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance = null;
    public Musics currentMusic = Musics.Chill;
    [SerializeField] AudioClip[] m_audios;
    List<AudioSource> m_sources = new List<AudioSource>();
    [SerializeField] float m_volume = 0.3f;

    public enum Musics{
        Ambient,
        Chill,
        Fast,
    }

    IEnumerator FadeMusic(Musics music, float duration, float targetVolume){
        float currentTime = 0;
        float currentVol = m_sources[(int)music].volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetVolume, currentTime / duration);
            m_sources[(int)music].volume = newVol;
            yield return null;
        }
        yield break;
    }

    public IEnumerator ChangeMusic(Musics from, Musics to){
        StartCoroutine(FadeMusic(from, 1f, 0));
        yield return new WaitForSeconds(1f);
        m_sources[(int)to].Stop();
        m_sources[(int)to].Play();
        StartCoroutine(FadeMusic(to, 1f, 0.3f));
    }

    public static MusicManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType(typeof(MusicManager)) as MusicManager;

            return instance;
        }
    }
    public void Start() {
        for(int i = 0; i < m_audios.Length; i++) {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            m_sources.Add(temp);
            temp.playOnAwake = false;
            temp.loop = true;
            temp.clip = m_audios[i];
            temp.volume = m_volume;
        }
        
        m_sources[(int)currentMusic].Play();
    }

    public void PlayMusic(Musics newMusic){
        if(newMusic == currentMusic) return;

        StopAllCoroutines();
        StartCoroutine(ChangeMusic(currentMusic, newMusic));

        currentMusic = newMusic;
    }
}