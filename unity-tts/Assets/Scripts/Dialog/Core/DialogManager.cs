using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    #region Fields

    private Dictionary<DialogType, DialogController> dialogPrefabs = new Dictionary<DialogType, DialogController>();

    private Dictionary<DialogType, DialogController> instantiatedDialogs = new Dictionary<DialogType, DialogController>();

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

    public List<DialogData> DialogList { get; private set; }

    #endregion Fields

    public DialogManager()
    {
        Initialize();
    }

    public void Initialize()
    {
        DialogList = new List<DialogData>();

        CreateCanvas();
        LoadDialogs();
    }

    private void CreateCanvas()
    {
        var prefab = Resources.Load("Prefab/Dialog/Canvas/Dialog Canvas");
        GameObject.Instantiate(prefab);
    }

    private void LoadDialogs()
    {
        var prefabs = Resources.LoadAll<DialogController>("Prefab/Dialog/Popup");
        foreach (var prefab in prefabs)
        {
            var type = (DialogType)Enum.Parse(typeof(DialogType), prefab.name);
            dialogPrefabs[type] = prefab;
        }
    }

    public DialogController GetDialogController(DialogType type)
    {
        if (instantiatedDialogs.ContainsKey(type) == false)
        {
            var controller = GameObject.Instantiate(dialogPrefabs[type]) as DialogController;
            controller.transform.SetParent(DialogCanvas);
            controller.transform.localPosition = Vector3.zero;
            controller.transform.localScale = Vector3.one;
            controller.transform.localRotation = Quaternion.identity;

            instantiatedDialogs[type] = controller;
        }

        return instantiatedDialogs[type];
    }

    private void UpdateDialog()
    {
        foreach (var controller in instantiatedDialogs.Values)
            controller.gameObject.SetActive(false);

        if (DialogList.Count > 0)
        {
            var currentData = DialogList[DialogList.Count - 1];
            var controller = GetDialogController(currentData.Type);
            controller.gameObject.SetActive(true);

            controller.Build(currentData);
        }
    }

    public void Push(DialogData data)
    {
        DialogList.Add(data);
        UpdateDialog();
    }

    public void Pop()
    {
        if (DialogList.Count < 0)
            return;

        DialogList.RemoveAt(DialogList.Count - 1);
        UpdateDialog();
    }

    public void Pop(DialogData data)
    {
        if(instantiatedDialogs.ContainsKey(data.Type))
            instantiatedDialogs[data.Type].gameObject.SetActive(false);

        DialogList.Remove(data);
        UpdateDialog();
    }

    public void PopAll()
    {
        DialogList.Clear();
        UpdateDialog();
    }
}