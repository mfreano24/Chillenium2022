using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimator : MonoBehaviour
{

    public Animator monsterAnimatorOne;
    public Animator monsterAnimatorTwo;
    public Animator transitionAnimator;
    public Animator combatUIFadeIn;



    public void CallIntroAnimations() 
    {
        StartCoroutine(IntroAnimations());
    }
    public IEnumerator IntroAnimations() 
    {
        GameManager.playerSource.transform.parent = monsterAnimatorOne.transform;
        GameManager.enemySource.transform.parent = monsterAnimatorTwo.transform;
        GameManager.playerSource.transform.localPosition = Vector3.zero;
        GameManager.enemySource.transform.localPosition = Vector3.zero;
        GameManager.playerSource.transform.localRotation = Quaternion.identity;
        GameManager.enemySource.transform.localRotation = Quaternion.identity;
        monsterAnimatorOne.SetInteger("ChooseIntro",Random.Range(1, 6));
        monsterAnimatorTwo.SetInteger("ChooseIntro", Random.Range(1, 6));
        transitionAnimator.SetInteger("ChooseIntro", 1);
        yield return new WaitForSeconds(3.5f);
        combatUIFadeIn.SetTrigger("FadeIn");
        yield return null;
    }

    internal void CallLossAnimations()
    {

        StartCoroutine(LossAnimations());


    }
    IEnumerator LossAnimations() 
    {
        monsterAnimatorOne.SetTrigger("Kill");
        combatUIFadeIn.SetTrigger("FadeOut");
        yield return new WaitForSeconds(.2f);
        transitionAnimator.SetTrigger("FadeOut");

        yield return null;
    }

    internal void CallWinAnimations()
    {
        StartCoroutine(WinAnimations());
    }

    IEnumerator WinAnimations() 
    {
        monsterAnimatorTwo.SetTrigger("Kill");
        combatUIFadeIn.SetTrigger("FadeOut");
        yield return new WaitForSeconds(.2f);
        transitionAnimator.SetTrigger("FadeOut");


        yield return null;
    }
}
