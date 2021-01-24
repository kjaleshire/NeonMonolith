using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace NeonMonolith.Pipeline
{
    internal abstract class PipelineOutput<TMessage> : ITargetBlock<TMessage>
    {
        protected ITargetBlock<TMessage> _block = null!;

        public Task Completion => _block.Completion;

        public virtual void Complete() => _block.Complete();

        public void Fault(Exception exception) => _block.Fault(exception);

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TMessage messageValue, ISourceBlock<TMessage> source, bool consumeToAccept) =>
            _block.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
    }
}
