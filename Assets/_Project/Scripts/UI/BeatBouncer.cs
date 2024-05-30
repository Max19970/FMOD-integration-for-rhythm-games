using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBouncer : BeatListener
{
    [SerializeField] private AnimationCurve curve;
    [Space]
    [SerializeField] private float firstScale;
    [SerializeField] private bool customSecondScale;
    [ShowIf("customSecondScale")] [SerializeField] private float secondScale;
    [SerializeField] private bool customThirdScale;
    [ShowIf("customThirdScale")] [SerializeField] private float thirdScale;
    [SerializeField] private bool customFourthScale;
    [ShowIf("customFourthScale")] [SerializeField] private float fourthScale;

    private Vector3 initScale;

    protected override void Awake()
    {
        base.Awake();

        if (!customSecondScale) secondScale = firstScale;
        if (!customThirdScale) thirdScale = firstScale;
        if (!customFourthScale) fourthScale = firstScale;

        initScale = transform.localScale;
    }

    private void Start()
    {
        ListenTo = LevelManager.instance.levelTheme;
    }

    protected override void OnBeat(int currentBeat)
    {
        LeanTween.cancel(gameObject);

        switch (currentBeat) 
        {
            case 1:
                LeanTween.value(gameObject, (float value) => { transform.localScale = new Vector3(initScale.x * value, initScale.y * value, initScale.z); }, initScale.x, firstScale, 1f / speed / 2f).setEase(curve);
                //transform.LeanScale(new Vector3(firstScale, firstScale, 1), 1f / speed / 2f).setEase(curve);
                break;
            case 2:
                LeanTween.value(gameObject, (float value) => { transform.localScale = new Vector3(initScale.x * value, initScale.y * value, initScale.z); }, initScale.x, secondScale, 1f / speed / 2f).setEase(curve);
                //transform.LeanScale(new Vector3(secondScale, secondScale, 1), 1f / speed / 2f).setEase(curve);
                break;
            case 3:
                LeanTween.value(gameObject, (float value) => { transform.localScale = new Vector3(initScale.x * value, initScale.y * value, initScale.z); }, initScale.x, thirdScale, 1f / speed / 2f).setEase(curve);
                //transform.LeanScale(new Vector3(thirdScale, thirdScale, 1), 1f / speed / 2f).setEase(curve);
                break;
            case 4:
                LeanTween.value(gameObject, (float value) => { transform.localScale = new Vector3(initScale.x * value, initScale.y * value, initScale.z); }, initScale.x, fourthScale, 1f / speed / 2f).setEase(curve);
                //transform.LeanScale(new Vector3(fourthScale, fourthScale, 1), 1f / speed / 2f).setEase(curve);
                break;
            default:
                LeanTween.value(gameObject, (float value) => { transform.localScale = new Vector3(initScale.x * value, initScale.y * value, initScale.z); }, initScale.x, fourthScale, 1f / speed / 2f).setEase(curve);
                //transform.LeanScale(new Vector3(firstScale, firstScale, 1), 1f / speed / 2f).setEase(curve);
                break;
        }
    }
}
