using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;

public class TreesUis : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private Transform _parentTrees;

    [SerializeField]
    private GameObject _textTemplate;

    [SerializeField]
    private GameObject _parentUIPause;

    [SerializeField]
    private Color _treeColor;

    [SerializeField, ReadOnly(true)]
    private List<Collectible> _listTreeCollectible = new List<Collectible>();

    private List<GameObject> listTreeUIs = new List<GameObject>();

    void Start()
    {
        _listTreeCollectible = _parentTrees.GetComponentsInChildren<Collectible>().ToList();
        InitUI();
    }

    // Update is called once per frame
    void Update() { }

    void InitUI()
    {
        string uiTreeString = "t\nr\ne\ne";
        for (int i = 0; i < _listTreeCollectible.Count; i++)
        {
            var newTreeUI = Instantiate(_textTemplate, _parentUIPause.transform);
            listTreeUIs.Add(newTreeUI);

            // uiTreeString += "e";

            // setup of the new tree UI
            newTreeUI.name = "treeUI" + i.ToString();
            var texTreeUI = newTreeUI.GetComponent<TextMeshProUGUI>();
            texTreeUI.text = uiTreeString;
            texTreeUI.color = _treeColor;
            var followUIPosition = newTreeUI.AddComponent<FollowDuckPosition>();
            followUIPosition.SetTransformToFollow(_listTreeCollectible[i].transform);

            _listTreeCollectible[i]
                .OnCollected.AddListener(() =>
                {
                    Destroy(newTreeUI);
                });
        }
    }
}
