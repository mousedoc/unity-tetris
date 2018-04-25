using System;

public class DialogDataConfirm : DialogData
{
    public string TitleKey { get; private set; }

    public string DescriptionKey { get; private set; }

    public string ConfirmTextKey { get; private set; }

    public Action ConfirmCallback { get; private set; }

    public DialogDataConfirm(string title, string description, string confirm, Action confirmCallback = null) : base(DialogType.Confirm)
    {
        TitleKey = title;
        DescriptionKey = description;
        ConfirmTextKey = confirm;
        ConfirmCallback = confirmCallback;
    }
}