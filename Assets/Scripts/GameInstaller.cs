using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private Transform _dicePanel;
    
    public override void InstallBindings()
    {
        Container.Bind<IDiceRollingService>().To<DiceRollingService>().AsSingle();
        Container.Bind<IExpeditionFactory>().To<ExpeditionFactory>().AsSingle();
        
        
        Container.Bind<IDiceFactory>().To<DiceFactory>().AsSingle();
        Container.Bind<IDicePoolService>().To<DicePoolService>().AsSingle();

        Container.Bind<GameObject>().WithId("DicePrefab").FromInstance(_dicePrefab);
        Container.Bind<Transform>().WithId("DicePanel").FromInstance(_dicePanel);

        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();

    }
}