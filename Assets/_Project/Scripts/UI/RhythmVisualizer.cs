using FMODUnity;
using UnityEngine.Events;
using UnityEngine;

public class RhythmVisualizer : BeatListener 
{
    [SerializeField] private Transform objectPool;
    [SerializeField] private Transform activePool;

    private void Start()
    {
        ListenTo = LevelManager.instance.levelTheme;
    }

    protected override void OnBeat(int currentBeat) 
    {
        if (speed == 0 || objectPool.childCount == 0) return;

        RhythmElement element = objectPool.GetChild(0).gameObject.GetComponent<RhythmElement>();
        element.transform.SetParent(activePool);
        element.Go(1f / speed, 4, objectPool);
    }
}