using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] lines;
    public float textSpeed;
    public GameObject panel;
    Animator animator;
    bool isStarted;

    public bool activate;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        activate = false;
        animator = GetComponent<Animator>();
        text.text = string.Empty;
        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        isStarted = true;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            panel.gameObject.SetActive(false);
            animator.SetTrigger("Death");
        }
    }

    public void Destroyer()
    {
        activate = true;
        gameObject.SetActive(false);
    }

    public void CutSound()
    {
        AudioController.Instance.musicSource.Stop();
        AudioController.Instance.PlayMusic("Tension");
    }

    public void PlayAudioClip(string s)
    {
        AudioController.Instance.PlaySFX(s);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isStarted)
        {
            panel.gameObject.SetActive(true);
            StartDialogue();
        }
    }
}
