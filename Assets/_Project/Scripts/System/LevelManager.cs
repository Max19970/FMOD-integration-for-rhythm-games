using FMODUnity;
using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public EventReference levelTheme;

    private FMODEventContainer container;

    public static LevelManager instance { get; private set; }

    private float fury = 0f;
    public float Fury 
    { 
        get 
        { 
            return this.fury;
        } 
        set 
        { 
            value = Mathf.Clamp(value, 0, 100);

            if (value - fury > 0)
            { 
                tierDecrease = false;
                timer_decrease.Reset();
            } 

            fury = value;
            if (container != null) AudioManager.instance.SetParameter(container.name, "Tier", value / 100f);
        }
    }

    private Timer timer_decrease;

    private bool tierDecrease;
    private float decreaseSpeed = 2f;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Found more than one {GetType().Name} object in the scene.");
        }
        instance = this;

        timer_decrease = new Timer(1f, () => { tierDecrease = true; }, true);
    }

    private void Start()
    {
        container = AudioManager.instance.FindContainer(levelTheme);
        AudioManager.instance.Play(levelTheme);
    }

    private void Update()
    {
        timer_decrease.Update();

        TierDecrease();

        Debug.Log(fury);
    }

    private void TierDecrease()
    {
        if (!tierDecrease) return;

        Fury -= decreaseSpeed * Time.deltaTime;
    }
}