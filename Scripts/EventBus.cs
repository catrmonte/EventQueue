using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : Singleton<EventBus>
{
    private Dictionary<string, UnityEngine.Events.UnityEvent> m_EventDictionary;
    private int eventsInQueue = 0;
    // time it takes for queue to dequeue and trigger event
    private float dequeueInterval = 2f;
    private List<string> eventQueue;
    bool queueRunning = false;

    public override void Awake()
    {
        base.Awake();
        Instance.Init();
    }

    private void Init()
    {
        // If not yet initialized, set up the Dictionary
        if (Instance.m_EventDictionary == null)
        {
            Instance.m_EventDictionary = new Dictionary<string, UnityEngine.Events.UnityEvent>();
        }
        // If not yet initialized, set up the eventQueue
        if (Instance.eventQueue == null)
        {
            Instance.eventQueue = new List<string>();
        }
    }

    public static void StartListening(string eventName, UnityEngine.Events.UnityAction listener)
    {
        UnityEngine.Events.UnityEvent thisEvent = null;
        if (Instance.m_EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEngine.Events.UnityEvent();
            thisEvent.AddListener(listener);
            Instance.m_EventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityEngine.Events.UnityAction listener)
    {
        UnityEngine.Events.UnityEvent thisEvent = null;
        if (Instance.m_EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEngine.Events.UnityEvent thisEvent = null;
        if (Instance.m_EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void AddToQueue(string eventName)
    {
        Instance.eventQueue.Add(eventName);
        Debug.Log("Event Added to Queue: " + eventName);
        Instance.eventsInQueue++;

    }
    public IEnumerator DequeueEvent()
    {
        yield return new WaitForSeconds(dequeueInterval);
        while (eventsInQueue > 0)
        {

            Debug.Log("Number of events in Queue: " + eventsInQueue);
            string currentEvent = eventQueue[0];
            Debug.Log("Event Dequeued: " + currentEvent);
            Instance.eventQueue.RemoveAt(0);
            Instance.eventsInQueue--;
            TriggerEvent(currentEvent);
            yield return new WaitForSeconds(dequeueInterval);
        }
        queueRunning = false;
    }

    public void Update()
    {
        if (eventsInQueue > 0 && queueRunning == false)
        {
            StartCoroutine("DequeueEvent");
            queueRunning = true;
        }

    }
}
