using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    private void Awake()
    {
        DialogManager.Instance.Initialize();
    }

    private void asdf()
    {
        var confirm = new DialogDataConfirm("title", "desc", "confirm", () =>
        {
            Debug.Log("Complete");
        });

        DialogManager.Instance.Push(confirm);
    }

    private void Start()
    {
        asdf(); asdf(); asdf(); asdf(); asdf(); asdf();
    }
}
   