public abstract class DialogData
{
    public DialogType Type { get; private set; }

    public DialogData(DialogType type)
    {
        Type = type;
    }
}