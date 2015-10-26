using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
	private float startTime;
	private Dictionary<float, List<Action>> timeToActionMap = new Dictionary<float, List<Action>>();
	//private List<float> times = new List
	//private Dictionary<int, Action> timeToActionMap = new Dictionary<float, List<Action>>();
	private List<float> timeToDeleteList = new List<float>();

	public void Register(float time, Action action)
	{
		List<Action> actionList;
		if (!timeToActionMap.TryGetValue(time, out actionList))
		{
			actionList = new List<Action>();
			actionList.Add(action);
		}

		timeToActionMap[time] = actionList;
	}

	void Update()
	{
		timeToDeleteList.Clear();

		List<float> keys = new List<float>(timeToActionMap.Keys);

		foreach (var key in keys) 
		{
			if (Time.time >= key)
			{
				List<Action> actions = timeToActionMap[key];
				for (int i = 0; i < actions.Count; ++i)
				{
					if (actions[i] != null)
					{
						actions[i]();
					}
				}

				timeToDeleteList.Add(key);
			}
		}

		for (int i = 0; i < timeToDeleteList.Count; ++i)
		{
			timeToActionMap.Remove(timeToDeleteList[i]);
		}
	}
}
