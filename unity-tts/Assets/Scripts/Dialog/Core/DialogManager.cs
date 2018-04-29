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

    private Stack<DialogData> DialogStack { get; set; }

    #endregion Fields

    public void Initialize()
    {
        DialogStack = new Stack<DialogData>();

        CreateCanvas();
        LoadDialogs();
    }

    private void CreateCanvas()
    {
        var prefab = Resources.Load("Prefab/UI/Canvas/Dialog Canvas");
        GameObject.Instantiate(prefab);
    }

    private void LoadDialogs()
    {
        var prefabs = Resources.LoadAll<DialogController>("PrefabUI/Dialog");
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

        if (DialogStack.Count > 0)
        {
            var currentData = DialogStack.Peek();
            var controller = GetDialogController(currentData.Type);
            controller.gameObject.SetActive(true);

            controller.Build(currentData);
        }
    }

    public void Push(DialogData data)
    {
        DialogStack.Push(data);
        UpdateDialog();
    }

    public void Pop()
    {
        if (DialogStack.Count > 0)
            DialogStack.Pop();

        UpdateDialog();
    }

    public void PopAll()
    {
        DialogStack.Clear();
        UpdateDialog();
    }
}