using UnityEngine;

[CreateAssetMenu(fileName = "SkinInfo", menuName = "ScriptableObjects/SkinInfo")]
public class SkinInfoScriptableObject : ScriptableObject
{
    [SerializeField] public float Price;
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    //[SerializeField] public Sprite Sprite;
    [SerializeField] public int Damage;
    [SerializeField] public int AttackSpeed;
    [SerializeField] public int Id;
    [SerializeField] public GameObject SkinGameObject;
}
