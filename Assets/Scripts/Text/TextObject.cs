using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextObject : MonoBehaviour
{
    [SerializeField, TextArea] List<string> Texts;
    [SerializeField] List<UnityEvent> TextTriggers;

    bool m_textActive = false; 
    // Start is called before the first frame update
    void Update() {
        if(Input.anyKeyDown)
            TextBoxManager.Instance.AdvanceText();
    }
    void OnEnable() {
        TextBoxManager.Instance.OnTextStart.AddListener(OnTextStart);
        TextBoxManager.Instance.OnTextFinish.AddListener(OnTextFinish);
        TextBoxManager.Instance.OnTextTrigger.AddListener(OnTextTrigger);
        TextBoxManager.Instance.SetText(Texts,"bruh");
    }

    void OnTextStart() {
        m_textActive = true;
    }
    void OnTextFinish() {
        m_textActive = false;
        TextBoxManager.Instance.OnTextStart.RemoveListener(OnTextStart);
        TextBoxManager.Instance.OnTextFinish.RemoveListener(OnTextFinish);
    }
    void OnTextTrigger(int i){
        if(TextTriggers.Count > i){
            TextTriggers[i].Invoke();
        }
    }
}