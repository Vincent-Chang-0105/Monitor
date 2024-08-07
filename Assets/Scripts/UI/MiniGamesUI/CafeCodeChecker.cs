using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeCodeChecker : MonoBehaviour
{
    public bool CheckCode(string input)
    {
        return input.ToLower() == "cafe".ToLower();
    }

    public string SuccessMessage => "Deciphered Complete!";
    public Color SuccessColor => Color.green;
    public string FailureMessage => "Wrong Code";
    public Color FailureColor => Color.red;
}
