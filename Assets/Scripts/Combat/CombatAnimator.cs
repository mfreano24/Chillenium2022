using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimator : MonoBehaviour
{

    public Animator monsterAnimatorOne;
    public Animator monsterAnimatorTwo;
    public Animator transitionAnimator;
    public Animator combatUIFadeIn;
    public Animator cameraAnimator;
    public Animator shopAnimator;

    public void CallIntroAnimations()
    {
        StartCoroutine(IntroAnimations());
    }
    public IEnumerator IntroAnimations()
    {

        GameManager.playerSource.transform.parent = monsterAnimatorOne.transform;

        Debug.Log("Here goes");
        Debug.Log(GameManager.enemySource.name);
        GameManager.enemySource.transform.parent = monsterAnimatorTwo.transform;
        Debug.Log("Nothing");

        GameManager.playerSource.transform.localPosition = Vector3.zero;

        GameManager.enemySource.transform.localPosition = Vector3.zero;

        GameManager.playerSource.transform.localRotation = Quaternion.identity;

        GameManager.enemySource.transform.localRotation = Quaternion.identity;
        int introOne = Random.Range(1, 6);
        monsterAnimatorOne.SetInteger("ChooseIntro", introOne);
        int introTwo = Random.Range(1, 6);
        monsterAnimatorTwo.SetInteger("ChooseIntro", introTwo);
        transitionAnimator.SetInteger("ChooseIntro", 1);
        
        if (introOne == 4 || introTwo == 4)
        {
            AudioManager.Instance.PlaySFX("foot_steps");
        }

        yield return new WaitForSeconds(2.5f);

        if (introOne == 1 || introTwo == 1)
        {
            AudioManager.Instance.PlaySFX("foot_steps");
        }

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

    internal void CallFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator WinAnimations()
    {
        monsterAnimatorTwo.SetTrigger("Kill");
        Debug.Log("kill monster");
        combatUIFadeIn.SetTrigger("FadeOut");
        Debug.Log("fadeUI");
        cameraAnimator.SetTrigger("DeathCam");
        yield return new WaitForSeconds(1.4f);
        shopAnimator.SetTrigger("OpenShop");


        yield return null;
    }

    IEnumerator FadeOut()
    {
        transitionAnimator.SetTrigger("FadeOut");
        yield return null;
    }

}
