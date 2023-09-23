using System;
using Entity;
using GamePlay.Components.Interfaces;
using GamePlay.Custom.GameMachine;
using Zenject;

public class ClosestEntityAdapter : IStartListener, IDisableListener
{
   private ClosestEntitySearcher _closestEntity;
   private IGetEntityComponent _getEntityComp;
   private ISetEntityTargetComponent _setEntityCom;

   [Inject]
   private void Construct(ClosestEntitySearcher closestEntity, IGetEntityComponent getEntityComp)
   {
      _closestEntity = closestEntity;
      _getEntityComp = getEntityComp;
   }
   void IStartListener.StartGame()
   {
      if (!_getEntityComp.GetEntity().TryGet(out ISetEntityTargetComponent setTarget))
         throw new NullReferenceException($"Компонент ISetEntityTargetComponent не найден на сущности {_getEntityComp.GetEntity()}");
      _setEntityCom = setTarget;
      _closestEntity.OnClosestEntityChanged += SetTargetEntity;
   }

   void IDisableListener.Disable()
   {
      _closestEntity.OnClosestEntityChanged -= SetTargetEntity;
   }
   
   private void SetTargetEntity(Entity.Entity entity)
   {
      _setEntityCom.SetEntityTarget(entity);
   }
}
