using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MRPlayerID
{
    First = 1,
    Second =2
};
[ExecuteInEditMode]
public class MRPalyers : MonoBehaviour
{
    [Header("PlayerID")]
    public MRPlayerID mrPlayerID = MRPlayerID.First;
    int posID;
    // Start is called before the first frame update
    void Start()
    {
        posID = PlayerPositionHashToShader(mrPlayerID);
    }

    // Update is called once per frame
    void Update()
    {
        
        Shader.SetGlobalVector(posID, transform.position);
    }

    private int PlayerPositionHashToShader(MRPlayerID ids)
    {
        switch (ids)
        {
            case MRPlayerID.First:
                return Shader.PropertyToID("_PlayerPosOne");
                break;
            case MRPlayerID.Second:
                return Shader.PropertyToID("_PlayerPosTwo");
                break;
            default:
                return Shader.PropertyToID("_InvalidPos");
                break;
        }
    }
}
