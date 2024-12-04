using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Services.Runtime;
using Odumbrata.Behaviour.Rooms.Abstractions;
using Odumbrata.Core;

namespace Odumbrata.Services.Rooms
{
    public enum RoomTransitionType
    {
        Additional,
        Single
    }

    public sealed class RoomsService : PocoService, IRegister<BaseRoomBehaviour>
    {
        private IRegister<BaseRoomBehaviour> _register;
        public IReadOnlyList<BaseRoomBehaviour> Elements => _register.Elements;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _register = new Register<BaseRoomBehaviour>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Activate(Type type, RoomTransitionType transitionType)
        {
            if (Elements.IsNullOrEmpty())
            {
                return;
            }

            var canNotFindRoom = !TryFind(type, out var targetRoom);

            if (canNotFindRoom)
            {
                return;
            }

            if (transitionType == RoomTransitionType.Single)
            {
                foreach (var room in Elements)
                {
                    room.Deactivate();
                }
            }

            if (targetRoom.IsActive)
            {
                return;
            }

            targetRoom.Activate();
        }

        public void Deactivate(Type type)
        {
            if (Elements.IsNullOrEmpty())
            {
                return;
            }

            var canNotFindRoom = !TryFind(type, out var targetRoom);

            if (canNotFindRoom)
            {
                return;
            }

            targetRoom.Deactivate();
        }

        public void Activate<TRoom>(RoomTransitionType transitionType) where TRoom : BaseRoomBehaviour
        {
            Activate(typeof(TRoom), transitionType);
        }

        public void Deactivate<TRoom>() where TRoom : BaseRoomBehaviour
        {
            Deactivate(typeof(TRoom));
        }

        private bool TryFind(Type type, out BaseRoomBehaviour result)
        {
            result = Elements.FirstOrDefault(temp => temp.GetType() == type);
            return result != null;
        }

        public void Add(BaseRoomBehaviour element)
        {
            _register.Add(element);
        }

        public void Remove(BaseRoomBehaviour element)
        {
            _register.Remove(element);
        }

        public bool Contains(BaseRoomBehaviour element)
        {
            return _register.Contains(element);
        }
    }
}