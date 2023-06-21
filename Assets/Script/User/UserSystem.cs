using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

public partial struct UserSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (user, transform) in SystemAPI.Query<RefRW<User>, RefRW<LocalTransform>>())
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.ValueRW.Position = MoveDirection(1, transform.ValueRW.Position, deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.ValueRW.Position = MoveDirection(2, transform.ValueRW.Position, deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.ValueRW.Position = MoveDirection(3, transform.ValueRW.Position, deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.ValueRW.Position = MoveDirection(4, transform.ValueRW.Position, deltaTime);
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                // set bullet position to user position
                Entity bulletPf = user.ValueRO.bulletPrefab;

                //* SetComponentData is a way to set the component to the entity.
                state.EntityManager.SetComponentData(bulletPf, new LocalTransform
                {
                    Position = transform.ValueRO.Position,
                    Rotation = quaternion.identity,
                    Scale = 1
                });
                state.EntityManager.Instantiate(bulletPf);
            }
        }
    }

    private float3 MoveDirection(int type, float3 position, float deltaTime)
    {
        float speed = 5;
        switch (type)
        {
            case 1:
                position.y = position.y + speed * deltaTime;
                break;
            case 2:
                position.y = position.y - speed * deltaTime;
                break;
            case 3:
                position.x = position.x - speed * deltaTime;
                break;
            case 4:
                position.x = position.x + speed * deltaTime;
                break;
        }

        return position;
    }
}
