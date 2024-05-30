using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuryMeter : MonoBehaviour
{
    [SerializeField] private Image tier1;
    [SerializeField] private Image tier2;
    [SerializeField] private Image tier3;
    [SerializeField] private Image tier4;
    [SerializeField] private TextMeshProUGUI meterText;

    private void Update()
    {
        tier1.fillAmount = Mathf.Clamp(LevelManager.instance.Fury / 25f, 0f, 1f);
        tier2.fillAmount = Mathf.Clamp((LevelManager.instance.Fury - 25) / 25f, 0f, 1f);
        tier3.fillAmount = Mathf.Clamp((LevelManager.instance.Fury - 50) / 25f, 0f, 1f);
        tier4.fillAmount = Mathf.Clamp((LevelManager.instance.Fury - 75) / 25f, 0f, 1f);

        meterText.text = ((int)LevelManager.instance.Fury).ToString();
    }
}
