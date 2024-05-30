using FMODUnity;
using UnityEngine.Events;
using UnityEngine;

public abstract class BeatListener : MonoBehaviour
{
    [SerializeField] protected EventReference listenTo;
    public EventReference ListenTo { get { return this.listenTo; } set { listenTo = value; UpdateListener(); } }

    protected float speed;
    protected UnityAction<int> onBeat;

    protected FMODEventContainer container;

    protected virtual void Awake()
    {
        onBeat = (int currentBeat) => { StartCoroutine(Helpers.InvokeAfterTime(GameSettings.beatDelay, () => { OnBeat(currentBeat); })); };
    }

    protected virtual void UpdateListener()
    {
        container?.onBeat.RemoveListener(onBeat);
        container = AudioManager.instance.FindContainer(listenTo);
        container.onBeat.AddListener(onBeat);
    }

    protected virtual void Update()
    {
        speed = container == null ? 1f : container.timelineInfo.currentTempo / 60f;
    }

    protected abstract void OnBeat(int currentBeat);
}