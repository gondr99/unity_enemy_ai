using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/NinjaStateChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "NinjaStateChannel", message: "Change [ninjaState]", category: "Events", id: "2a9a18c5fc16b9c887cd704cde7fca83")]
public sealed partial class NinjaStateChannel : EventChannel<NinjaBTState> { }

