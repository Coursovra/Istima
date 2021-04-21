using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkinsScriptableObject", menuName = "ScriptableObjects/PlayerSkins")]
public class PlayerModelScriptableObject : ScriptableObject
{
    [SerializeField] public List<SkinButtonView> PlayerSkins;
}
