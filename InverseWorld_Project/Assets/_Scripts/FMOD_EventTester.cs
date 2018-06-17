using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class FMOD_EventTester : MonoBehaviour
{
    public KeyCode startEvent = KeyCode.Alpha1;
    public KeyCode stopEvent = KeyCode.Alpha2;
    [FMODUnity.EventRef]
    public string MusicEventName = "event:/EVENT";
    public string invertParameterName = "";

    public KeyCode setParameter_0 = KeyCode.Alpha3;
    public KeyCode setParameter_1 = KeyCode.Alpha4;

    public FMOD.Studio.EventInstance _event;
    public FMOD.Studio.ParameterInstance _parameter;

    private bool eventIsOn = false;

    void Start()
    {
        _event = FMODUnity.RuntimeManager.CreateInstance(MusicEventName);
        _event.getParameter(invertParameterName, out _parameter);
    }

    void Update()
    {
        if (Input.GetKeyDown(startEvent))
        {
            _event.start();
            //eventIsOn = !eventIsOn;
            //if (eventIsOn)
            //{

            //}
            //else
            //{
            //    _event.stop(STOP_MODE.ALLOWFADEOUT);
            //}
        }
        if (Input.GetKeyDown(stopEvent))
        {
            _event.stop(STOP_MODE.ALLOWFADEOUT);
        }
        if (Input.GetKeyDown(setParameter_0))
        {
            _event.getParameter(invertParameterName, out _parameter);
           _parameter.setValue(0f);
        }
        if (Input.GetKeyDown(setParameter_1))
        {
            _event.getParameter(invertParameterName, out _parameter);
            _parameter.setValue(1f);
        }
    }
}
