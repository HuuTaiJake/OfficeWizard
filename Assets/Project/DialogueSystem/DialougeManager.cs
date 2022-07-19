using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue, navButtonText;
    public Image speakerSprite;

    private int _currentIndex;
    private Conversation _currentConvo;
    private static DialougeManager _instance;
    private Animator _anim;
    private Coroutine _typing;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            _anim = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StartConversation(Conversation convo)
    {
        _instance._anim.SetBool("isOpen", true);
        _instance._currentIndex = 0;
        _instance._currentConvo = convo;
        _instance.speakerName.text = "";
        _instance.dialogue.text = "";
        _instance.navButtonText.text = ">>";

        _instance.ReadNext();
    }

    public void ReadNext()
    {
        if(_currentIndex > _currentConvo.GetLength())
        {
            _instance._anim.SetBool("isOpen", false);
            return;
        }
        speakerName.text = _currentConvo.GetLineByIndex(_currentIndex).speaker.GetName();
        //dialogue.text = _currentConvo.GetLineByIndex(_currentIndex).dialogue;


        if(_typing == null)
        {
            _typing = _instance.StartCoroutine(TypeText(_currentConvo.GetLineByIndex(_currentIndex).dialogue));
        }
        else
        {
            _instance.StopCoroutine(_typing);
            _typing = null;
            _typing = _instance.StartCoroutine(TypeText(_currentConvo.GetLineByIndex(_currentIndex).dialogue));
        }

        speakerSprite.sprite = _currentConvo.GetLineByIndex(_currentIndex).speaker.GetSprite();
        _currentIndex++;

        if(_currentIndex >= _currentConvo.GetLength() + 1)
        {
            navButtonText.text = "X";
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";
        bool complete = false;
        int index = 0;

        while (!complete)
        {
            dialogue.text += text[index];
            index++;
            yield return new WaitForSeconds(0.02f);

            if(index == text.Length)
            {
                complete = true;
            }
        }

        _typing = null;
    }
}
