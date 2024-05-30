using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class RhythmElement : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float complition = 0f;
    [SerializeField] private float radius = 1f;

    private Transform inactivePool;
    private Image image;

    private bool checkForInput = false;
    private float time = 0;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        AlignToPosition();
        CheckInput();
    }

    private void CheckInput()
    {
        if (!(complition >= 0.4f && complition <= 0.6f) || !checkForInput) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if ((complition >= 0.4f && complition <= 0.45f) || (complition >= 0.55f && complition <= 0.6f))
                Awful();
            else if ((complition > 0.45f && complition <= 0.48f) || (complition >= 0.52f && complition < 0.55f))
                Normal();
            else
                Great();
        }
    }

    private void AlignToPosition() 
    {
        transform.localPosition = Quaternion.AngleAxis(-complition * 360f, Vector3.forward) * new Vector3(0, -radius, 0);
    }

    private void Awful()
    {
        LeanTween.cancel(gameObject);

        checkForInput = false;
        //transform.LeanScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
        LeanTween.value(gameObject, (float value) => { image.color = Color.black * new Color(1f, 1f, 1f, value); }, 1f, 0f, time / 2f).setOnComplete(() => { complition = 0f; transform.SetParent(inactivePool); });
    }

    private void Normal() 
    {
        LeanTween.cancel(gameObject);

        checkForInput = false;
        transform.LeanScale(new Vector3(1.1f, 1.1f, 1.1f), time / 2f);
        LeanTween.value(gameObject, (float value) => { image.color = Color.white * new Color(1f, 1f, 1f, value); }, 1f, 0f, time / 2f).setOnComplete(() => { complition = 0f; transform.SetParent(inactivePool); });

        LevelManager.instance.Fury += 0.5f;
    }

    private void Great()
    {
        LeanTween.cancel(gameObject);

        checkForInput = false;
        transform.LeanScale(new Vector3(1.5f, 1.5f, 1.5f), time / 2f);
        LeanTween.value(gameObject, (float value) => { image.color = Color.yellow * new Color(1f, 1f, 1f, value); }, 1f, 0f, time / 2f).setOnComplete(() => { complition = 0f; transform.SetParent(inactivePool); });

        LevelManager.instance.Fury += 1f;
    }

    public void Go(float time, float speedCount, Transform inactivePool) 
    {
        LeanTween.cancel(gameObject);

        Reset();

        checkForInput = true;
        this.inactivePool = inactivePool;
        this.time = time;
        LeanTween.value(gameObject, (float value) => { complition = value; }, 0f, 1f, time * speedCount).setOnComplete(() => { transform.SetParent(inactivePool); });
    }

    private void Reset()
    {
        image.color = Color.white;
        transform.localScale = Vector3.one;
    }
}
