using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnlockConfirm : Panel
{
    [SerializeField] private Button positiveButton = null;
    [SerializeField] private Button negativeButton = null;

    public delegate void Callback(Result result);
    private Callback callback = null;

    public enum Result
    {
        Positive = 1, Negative = 2
    }

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        positiveButton.onClick.AddListener(Positive);
        negativeButton.onClick.AddListener(Negative);
        base.Initialize();
    }

    public void Open(Callback callback)
    {
        Open();
        this.callback = callback;
    }

    private void Positive()
    {
        if (callback != null)
        {
            callback.Invoke(Result.Positive);
        }
        Close();
    }

    private void Negative()
    {
        if (callback != null)
        {
            callback.Invoke(Result.Negative);
        }
        Close();
    }

}