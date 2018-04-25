using UnityEngine.UI;

public class DialogControllerConfirm : DialogControllerWithData<DialogDataConfirm>
{
    #region Inspector set

    public Text TitleText;
    public Text DescriptionText;
    public Text ConfirmText;

    #endregion Inspector set

    public override void Build(DialogData data)
    {
        base.Build(data);

        SetText();
    }

    private void SetText()
    {
        TitleText.text = Data.TitleKey.ToLocalize();
        DescriptionText.text = Data.DescriptionKey.ToLocalize();
        ConfirmText.text = Data.ConfirmTextKey.ToLocalize();
    }

    public void ConfirmButtonClick()
    {
        if (Data.ConfirmCallback != null)
            Data.ConfirmCallback();
    }
}