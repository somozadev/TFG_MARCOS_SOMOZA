using UnityEngine;

public class ObserverPat : MonoBehaviour
{
    public delegate void sujetoNotificador(ObserverPat s);
    public event sujetoNotificador sujetoNotifEvent;

    private void Notify()
    {
        sujetoNotifEvent(this);
        Destroy(this);
    }

}

public class Sujeto : MonoBehaviour
{
    ObserverPat pat = new ObserverPat();
    private void Start() { pat.sujetoNotifEvent += Notificated; }
    public void Notificated(ObserverPat s) { print("I have been notified"); }
}





