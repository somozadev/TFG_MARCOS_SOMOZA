using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FakeAdresseables", menuName = "Fake/FakeAdresseables", order = 0)]
[System.Serializable]
public class FakeAdresseables : ScriptableObject
{
    public List<FakeAdresseable> adresseables;
    public IList<GameObject> GetStageFirstPrefabs()
    {

        IList<GameObject> prefabs = new List<GameObject>();
        foreach (FakeAdresseable adresseable in adresseables)
            if (adresseable.special.Equals(SpecialRoom.StartRoom))
                prefabs.Add(adresseable.scenePrefab);

        return prefabs;
    }
    public IList<GameObject> GetStagePrefabs(int stageNumber)
    {
        IList<GameObject> prefabs = new List<GameObject>();
        switch (stageNumber)
        {
            case 1:
                foreach (FakeAdresseable adresseable in adresseables)
                    if (adresseable.stage.Equals(StageRoom.Stage1))
                        prefabs.Add(adresseable.scenePrefab);
                break;
            case 2:
                foreach (FakeAdresseable adresseable in adresseables)
                    if (adresseable.stage.Equals(StageRoom.Stage2))
                        prefabs.Add(adresseable.scenePrefab);
                break;
            case 3:
                foreach (FakeAdresseable adresseable in adresseables)
                    if (adresseable.stage.Equals(StageRoom.Stage3))
                        prefabs.Add(adresseable.scenePrefab);
                break;
            case 4:
                foreach (FakeAdresseable adresseable in adresseables)
                    if (adresseable.stage.Equals(StageRoom.Stage4))
                        prefabs.Add(adresseable.scenePrefab);
                break;
            case 5:
                foreach (FakeAdresseable adresseable in adresseables)
                    if (adresseable.stage.Equals(StageRoom.Stage5))
                        prefabs.Add(adresseable.scenePrefab);
                break;


        }
        return prefabs;
    }
}
