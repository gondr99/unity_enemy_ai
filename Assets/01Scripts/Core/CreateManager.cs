using GondrLib.EventSystem;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    [field: SerializeField] public GameEventChannelSo CreateChannel { get; private set; }
}
