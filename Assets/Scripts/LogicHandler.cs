using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LogicHandler : MonoBehaviour
{
    private List<string> prevList;

    public List<string> stringsList;

    public GameObject listHolder;

    public GameObject listElementPref;

    public InputField textField;

    public Button bttnAdd;

    void Start()
    {
        if (stringsList == null)
        {
            stringsList = new List<string>();
        }
        prevList = new List<string>();

        bttnAdd.onClick.RemoveAllListeners();
        bttnAdd.onClick.AddListener(onBttnAddClick);
    }

    void Update()
    {
        if (!prevList.SequenceEqual(stringsList) && stringsList.Count!=0 )
        {
            UpdateUi();
            prevList = new List<string>(stringsList);
        }
    }

    private void OnValidate()
    {
        UpdateUiInEditor();
    }

    /// <summary>
    /// Clear and redraw "table"
    /// </summary>
    public void UpdateUi() 
    {
        for (int i = 0; i < listHolder.transform.childCount; i++)
        {
            var child = listHolder.transform.GetChild(i);
            if (Application.isPlaying)
            {
                Destroy(child.gameObject);
            }
        }

        CreateBttns();
    }
    

    /// <summary>
    /// TODO Findout how to get with error
    /// </summary>
    private void UpdateUiInEditor() 
    {
        if (Application.isEditor)
        {
            #if UNITY_EDITOR
            for (int i = 0; i < listHolder.transform.childCount; i++)
            {
                var child = listHolder.transform.GetChild(i);
                    UnityEditor.EditorApplication.delayCall += () =>
                    {
                        DestroyImmediate(child.gameObject);
                    };
            }
            UnityEditor.EditorApplication.delayCall += () =>
            {
                CreateBttns();
            };
            #endif
        }
    }

    private void onBttnDelClick(int index) 
    {
        stringsList.RemoveAt(index);
    }

    public void onBttnAddClick() 
    {
        stringsList.Add(textField.text);
        textField.text = "";
    }

    /// <summary>
    /// Instantiate raws of the list
    /// </summary>
    private void CreateBttns() 
    {
        for (int indexer = 0; indexer < stringsList.Count; indexer++)
        {
            var raw = Instantiate(listElementPref, listHolder.transform);
            foreach (Transform child in raw.transform)
            {
                if (child.gameObject.name == "Label")
                {
                    child.GetChild(0).gameObject.GetComponent<Text>().text = stringsList[indexer];
                }
                if (child.gameObject.name == "DellBttn")
                {
                    // dont know why but doesnt work without this
                    int k = indexer;

                    child.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { onBttnDelClick(k); });
                }
            }
        }
    }
}
