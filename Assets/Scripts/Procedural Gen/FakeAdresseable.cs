using UnityEngine;


[CreateAssetMenu(fileName ="FakeAdresseableObject",menuName = "Fake/FakeAdresseableObject",order =1)]
[System.Serializable]
public class FakeAdresseable : ScriptableObject
{
    public StageRoom stage;
    public SpecialRoom special;

    public GameObject scenePrefab;

    public FakeAdresseable(StageRoom _stage, SpecialRoom _special, GameObject _prefab)
    {
        stage = _stage;
        special = _special;
        scenePrefab = _prefab;
    }
}
[System.Serializable] public enum StageRoom { Stage1,Stage2,Stage3,Stage4,Stage5 }
[System.Serializable] public enum SpecialRoom { Null, StartRoom, BossRoom } 