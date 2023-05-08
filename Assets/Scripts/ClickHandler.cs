using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    public UnityEvent downEvent;
    public UnityEvent upEvent;
    public Material green;
    public Material red;

    private void OnMouseDown()
    {
        Debug.Log("On Mouse Down");
        gameObject.GetComponent<Renderer>().material = red;
        downEvent?.Invoke();

    }

    private void OnMouseUp()
    {
        Debug.Log("On Mouse Up");
        gameObject.GetComponent<Renderer>().material = green;
        upEvent?.Invoke();

    }

}
