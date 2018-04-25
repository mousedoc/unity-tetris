public class DialogControllerWithData<T> : DialogController where T : DialogData
{
    public new T Data { get; private set; }

    public override void Build(DialogData data)
    {
        base.Build(data);

        Data = (T)base.Data;
    }
}