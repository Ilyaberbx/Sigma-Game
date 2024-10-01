using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utility;
using Odumbrata.Core;
using Odumbrata.Core.Container;
using Odumbrata.Extensions;
using UnityEngine;

namespace Odumbrata.Services.Commands
{
    public class CommandsSystem : BaseSystem
    {
        public event Action<ICommand> OnCommandStarted;
        public event Action<ICommand> OnCommandDone;

        private const string InvalidExecutionMessage = "You are trying to execute the same command in queue";

        private readonly Queue<ICommand> _commands = new Queue<ICommand>();
        private CancellationTokenSource _disposeCancellationSource;

        private CancellationToken DisposeCancellationToken => _disposeCancellationSource.Token;

        public override void Initialize(ISystemsContainerReadonly container)
        {
            base.Initialize(container);

            _disposeCancellationSource = new CancellationTokenSource();
        }

        public override void Dispose()
        {
            base.Dispose();

            _disposeCancellationSource?.Dispose();
        }

        public void RunInQueue(ICommand command)
        {
            if (command == null)
            {
                DebugUtility.LogException<NullReferenceException>();
                return;
            }

            if (_commands.Contains(command))
            {
                DebugUtility.LogException<InvalidOperationException>(InvalidExecutionMessage);
                return;
            }

            if (_commands.IsEmpty())
            {
                _commands.Enqueue(command);
                RunInternal(command);
            }
        }

        private async void RunInternal(ICommand command)
        {
            Debug.Log("Command " + command.GetType().Name + " started");
            OnCommandStarted.SafeInvoke(command);

            await command.Do(DisposeCancellationToken);

            Debug.Log("Command " + command.GetType().Name + " done");
            OnCommandDone.SafeInvoke(command);

            if (_commands.TryDequeue(out var nextCommand))
            {
                RunInternal(nextCommand);
            }
        }
    }
}