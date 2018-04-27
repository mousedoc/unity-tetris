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
		// Data.TitleKey.ToLocalize();
		TitleText.text = "Title Test";
		DescriptionText.text = "Description Text";
		ConfirmText.text = "Confirm Text";
    }

    public void ConfirmButtonClick()
    {
        if (Data.ConfirmCallback != null)
            DialogManager.Instance.Pop();
            Data.ConfirmCallback();
    }
}