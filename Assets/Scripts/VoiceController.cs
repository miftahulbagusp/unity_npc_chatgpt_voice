using System.Collections;
using System.Collections.Generic;
using OpenAI;
using TextSpeech;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "id-ID";
    [SerializeField]
    InputField inputField;
    [SerializeField]
    GameObject chatGPT;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Setup(LANG_CODE);
//#if UNITY_ANDROID
//        SpeechToText.Instance.onResultCallback = OnPartialSpeechResult;
//#endif
        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
        CheckPermission();
    }

    void CheckPermission()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
                {
                    Permission.RequestUserPermission(Permission.Microphone);
                }
                break;
            case RuntimePlatform.IPhonePlayer:
                if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
                {
                    Application.RequestUserAuthorization(UserAuthorization.Microphone);
                }
                break;
        }
    }

    void Setup(string code)
    {
        TextToSpeech.Instance.Setting(code, 1, 1);
        SpeechToText.Instance.Setting(code);
    }

    #region Text to Speech
    private void StartSpeaking(string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
    }

    private void StopSpeaking()
    {
        TextToSpeech.Instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Talking started . . .");
    }

    void OnSpeakStop()
    {
        Debug.Log("Talking stopped . . .");
    }
    #endregion

    #region Speech to Text
    public void StartListening()
    {
        StopSpeaking();
        Debug.Log("Start Listening");
        SpeechToText.Instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.Instance.StopRecording();
    }

    async void OnFinalSpeechResult(string result)
    {
        inputField.enabled = false;
        inputField.text = result;
        var response = await chatGPT.GetComponent<ChatGPT>().SendReply(result);
        StartSpeaking(response);
        inputField.text = "";
        inputField.enabled = true;
    }

    //void OnPartialSpeechResult(string result)
    //{
    //    inputField.text = result;
    //}
    #endregion
}
