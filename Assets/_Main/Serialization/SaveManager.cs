using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveManager : MonoBehaviour
{
    [FormerlySerializedAs("character")] public SP_CharacterModel mpCharacter;
    [SerializeField] private string path = "Saves";
    [SerializeField] private string fileName = "Fiumba";
    public void SaveInfoCrash()
    {
        CharacterInfoData data = new CharacterInfoData();
        data.speed = mpCharacter.speed;
        data.IsAlive = mpCharacter.isActiveAndEnabled;
        MySerialization.SerializationJSON(data, path,fileName);
    }
    public void LoadInfoCrash()
    {
        CharacterInfoData data = MySerialization.DeSerializationJSON<CharacterInfoData>(path, fileName);
        mpCharacter.speed = data.speed;
        mpCharacter.gameObject.SetActive(data.IsAlive);
    }
}
