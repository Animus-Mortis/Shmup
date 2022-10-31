using Game.Player;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerHealth player;

    public override void InstallBindings()
    {
        Container.BindInstance(player).AsSingle();
    }
}