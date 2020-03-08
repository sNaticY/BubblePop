using System;
using System.Collections;
using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.UI;

public class BubbleController : MonoBehaviour
{
    public RectTransform RectTransform;
    public Image Image;
    public Text Text;
    public CircleCollider2D Collider;
    public GameObject TrailEffect;
    public Animation Animation;

    public void PlayAnimationDelay(string animationName, float delay)
    {
        if (delay <= 0)
        {
            Animation.Play(animationName);
        }
        else
        {
            StartCoroutine(AnimationDelay(animationName, delay));
        }
    }

    private IEnumerator AnimationDelay(string animationName, float delay)
    {
        yield return new WaitForSeconds(delay);
        Animation.Play(animationName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BoundDown"))
        {
            ((GameEntity) gameObject.GetEntityLink().entity).isReadyToDestroy = true;
        }
    }

    private void OnDestroy()
    {
        gameObject.Unlink();
    }
}
