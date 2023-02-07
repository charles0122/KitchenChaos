using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="",fileName ="")]
public class KitchenObjectSO : ScriptableObject {

    public Transform prefab;
    public Sprite sprite;
    [SerializeField] private string objectName;
}
