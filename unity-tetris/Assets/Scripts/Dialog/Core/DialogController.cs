using UnityEngine;

public abstract class DialogController : MonoBehaviour
{
    public DialogData Data { get; private set; }

    public virtual void Build(DialogData data)
    {
        Data = data;
    }
}