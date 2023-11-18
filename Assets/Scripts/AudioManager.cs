using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] m_audios;
    List<AudioSource> m_sources = new List<AudioSource>();
    [SerializeField] float m_volume = 0.3f;

    static AudioManager instance = null;

    public enum AudioTypes{
        GunCharge,
        GunShot,
        GlassBreaking,
        GunTake,
        DoorOpen,
        DoorClose,
    }

    public static AudioManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

            return instance;
        }
    }

    public void Start() {
        for(int i = 0; i < m_audios.Length; i++) {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            m_sources.Add(temp);
            temp.playOnAwake = false;
            temp.clip = m_audios[i];
            temp.volume = m_volume;
        }
    }
    public void triggerAudio(AudioTypes type) {
        if ((int)type >= m_sources.Count) return; 
        m_sources[(int)type].Play();
    }

    public void stopAudio(AudioTypes type) {
        if ((int)type >= m_sources.Count) return; 
        m_sources[(int)type].Stop();
    }
}