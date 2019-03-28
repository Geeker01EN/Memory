using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    Animator animator;
    public bool selected;

    [SerializeField] AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("SoundFX/CardFlip");
        audio.volume = .5f;
        audio.playOnAwake = false;

        animator = GetComponent<Animator>();

        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/AnimationOfCards");
    }

    void OnMouseUp()
    {
        if (!GameObject.Find("PlayManager").GetComponent<PlayManager>().animationRunning)
        {
            selected = !selected;

            if (selected)
            {
                animator.SetBool("FaceUp", true);
                animator.SetBool("FaceDown", false);

                int index = int.Parse(gameObject.name[6].ToString());
                GameObject.Find("PlayManager").GetComponent<PlayManager>().UpdateState(index);

                audio.pitch = Random.Range(.8f, 1.2f);
                audio.Play();
            }
            else
            {
                FaceDown();

                GameObject.Find("PlayManager").GetComponent<PlayManager>().alreadyActiveCard = false;
            }
        }
    }

    public void FaceDown()
    {
        animator.SetBool("FaceUp", false);
        animator.SetBool("FaceDown", true);

        audio.pitch = Random.Range(.8f, 1.2f);
        audio.Play();
    }
}
