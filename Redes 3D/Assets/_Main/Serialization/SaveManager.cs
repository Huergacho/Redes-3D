using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    public CharacterModel character;
    [SerializeField] private string path = "Saves";
    [SerializeField] private string fileName = "Fiumba";
    public void SaveInfoCrash()
    {
        CharacterInfoData data = new CharacterInfoData();
        data.speed = character.speed;
        data.IsAlive = character.isActiveAndEnabled;
        MySerialization.SerializationJSON(data, path,fileName);
    }
    public void LoadInfoCrash()
    {
        CharacterInfoData data = MySerialization.DeSerializationJSON<CharacterInfoData>(path, fileName);
        character.speed = data.speed;
        character.gameObject.SetActive(data.IsAlive);
    }
}
