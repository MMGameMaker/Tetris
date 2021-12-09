using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventDispatcher : MonoBehaviour
{
    #region Singleton
    public static EventDispatcher Instance
    {
        get;
        private set;
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }
    #endregion

    #region Field
    //Store all listenner
    Dictionary<GameSettings.EventID, Action<object>> gameEventListener = new Dictionary<GameSettings.EventID, Action<object>>();
    #endregion

    #region Add Listener, Post Event, Remove Listenner
    /// <summary>
    /// Register to listen for EventID
    /// </summary>
    public void RegisterListener(GameSettings.EventID eventID, Action<object> callback)
    {
        //Check if listener exist in dictionary
        if (gameEventListener.ContainsKey(eventID))
        {
            gameEventListener[eventID] += callback;
        }
        else
        {
            gameEventListener.Add(eventID, null);
            gameEventListener[eventID] += callback;
        }
    }

    /// <summary>
    /// Posts the event. This will notify all listener that registered for this event
    /// </summary>
    
    public void PostEvent(GameSettings.EventID eventID, object param = null)
    {
        if (!gameEventListener.ContainsKey(eventID))
        {
            Debug.Log("No listeners for this event : " + eventID);
            return;
        }

        // posting event
        var callback = gameEventListener[eventID];
        // if there's no listener remain, then do nothing
        if (callback != null)
        {
            callback(param);
        }
        else
        {
            Debug.Log("PostEvent " + eventID + ", but no listener remain, Remove this key");
            gameEventListener.Remove(eventID);
        }
    }
    

    /// <summary>
    /// Removes the listener. Use to Unregister listener
    /// </summary>
    
    public void RemoveListener(GameSettings.EventID eventID, Action<object> callback)
    {
        // checking params
        if (gameEventListener.ContainsKey(eventID))
        {
            gameEventListener[eventID] -= callback;
        }
        else
        {
            Debug.Log("RemoveListener, not found key: " + eventID);
        }
    }


    /// <summary>
    /// Clear all listener
    /// </summary>
    /// 
    public void ClearAllListener()
    {
        gameEventListener.Clear();
    }

    #endregion
}
