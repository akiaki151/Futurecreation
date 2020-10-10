using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreObserver<T>
{
    //オブザーバー勉強してみた
    private T mValue;   

    public T Value
    {
        get { return mValue; }
        set
        {
            mValue = value;
            OnChanged(mValue);
        }
    }


#pragma warning disable 0067
    public Action<T> mChanged;
#pragma warning restore 0067


    public ScoreObserver()
    {
        mValue = default(T);
    }

    public ScoreObserver(T value)
    {
        mValue = value;
    }

    public void SetValueWithoutCallback(T value)
    {
        mValue = value;
    }

    public void SetValueIfNotEqual(T value)
    {
        if (mValue.Equals(value))
        {
            return;
        }
        mValue = value;
        OnChanged(mValue);
    }

    private void OnChanged(T value)
    {
        var onChanged = mChanged;
        if (onChanged == null)
        {
            return;
        }
        onChanged(value);
    }
}
