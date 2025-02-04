using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; set; }

    [Header("General")]
    public string displayName;

    [Header("Client's Name")]
    public string clientName;

    [Header("Location")]
    public string location;

    [Header("Description")] 
    public string description;

    [Header("Difficulty")]
    public string difficulty;

    [Header("Requirements")]
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int MoneyReward;

    //ensure the id is always the name of the Scriptable Object Asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

}
