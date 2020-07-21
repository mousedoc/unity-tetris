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
        TitleText.text = Data.TitleKey;
        DescriptionText.text = Data.DescriptionKey;
        ConfirmText.text = Data.ConfirmTextKey;
    }

    public void ConfirmButtonClick()
    {
        if (Data.ConfirmCallback != null)
            DialogManager.Instance.Pop();
            Data.ConfirmCallback();
    }
}
