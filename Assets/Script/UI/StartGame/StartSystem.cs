using Unity.Entities;
using UnityEngine;

public partial struct StartSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<StartCommand>();
    }

    public void OnUpdate(ref SystemState state)
    {
        new StartGameCommandListenerJob().Schedule();

        state.Enabled = false;
    }
}

public partial struct StartGameCommandListenerJob : IJobEntity
{
    public void Execute(in StartCommand command)
    {
        Debug.Log($"Game started!");
    }
}