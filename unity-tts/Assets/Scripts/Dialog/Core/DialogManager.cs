using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    #region Fields

    private Dictionary<DialogType, DialogController> dialogPrefabs = new Dictionary<DialogType, DialogController>();

    private List<DialogController> activeDialogs = new List<DialogController>();

    private RectTransform dialogCanvas = null;

    private RectTransform DialogCanvas
    {
        get
        {
            if (dialogCanvas == null)
                dialogCanvas = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<RectTransform>();

            return dialogCanvas;
        }
    }

    #endregion Fields

    public void Initialize()
    {
        CreateCanvas();

        LoadDialogs();
    }

    private void CreateCanvas()
    {
        var canvas = Resources.Load("prefab/ui/canvas/Dialog Canvas");
        GameObject.Instantiate(canvas);
    }

    private void LoadDialogs()
    {
        var prefabs = Resources.LoadAll<DialogController>("prefab/ui/dialog");
        foreach (var prefab in prefabs)
        {
            var type = (DialogType)Enum.Parse(typeof(DialogType), prefab.name);
            dialogPrefabs[type] = prefab;
        }
    }

    public void Push(DialogData data)
    {
        var dialog = GameObject.Instantiate(dialogPrefabs[data.Type]) as DialogController;

        dialog.transform.SetParent(DialogCanvas);
        dialog.transform.localPosition = Vector3.zero;
        dialog.transform.localScale = Vector3.one;
        dialog.transform.localRotation = Quaternion.identity;

        dialog.Build(data);
    }

    public void Pop(DialogController dialog)
    {
        if (activeDialogs.Contains(dialog) == false)
        {
			Debug.LogError("Invalid Dialog");
            return;
        }	

        activeDialogs.Remove(dialog);

        PopDialog(dialog);
    }

    public void PopAll()
    {
        foreach (var dialog in activeDialogs)
            PopDialog(dialog);
    }

    private void PopDialog(DialogController dialog)
    {
        switch (dialog.Data.Type)
        {
            case DialogType.Confirm:
            case DialogType.ConfirmCancel:
                dialog.gameObject.SetActive(false);
                break;

            default:
                GameObject.Destroy(dialog.gameObject);
                break;
        }
    }
}