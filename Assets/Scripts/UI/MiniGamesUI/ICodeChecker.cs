using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Code", menuName = "Code")]
public class ICodeChecker : ScriptableObject
{
    public new string name;
    public bool CheckCode(string input)
    {
        return input.ToLower() == name.ToLower();
    }

    public string SuccessMessage => "Deciphered Complete!";
    public Color SuccessColor => Color.green;
    public string FailureMessage => "Wrong Code";
    public Color FailureColor => Color.red;
}
