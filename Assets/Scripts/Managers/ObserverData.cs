using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class ObserverData : MonoBehaviour
    {
        private List<IObserver> list_Observers = new List<IObserver>();

        public void ResisterObserver(IObserver observer)
        {
            list_Observers.Add(observer);
        }
        public void RemoveObserver(IObserver observer)
        {
            list_Observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in list_Observers)
            {
                observer.DestroyObj();
            }
        }
    }
}
