using CortexDeveloper.ECSMessages.Components;
using Unity.Entities;

public struct StartCommand : IComponentData, IMessageComponent
{
    public bool IsStart { get; set; }
}