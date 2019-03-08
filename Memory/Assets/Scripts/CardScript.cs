using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    Animator animator;
    public bool selected;

    void Start()
    {
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
    }
}
