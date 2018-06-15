namespace RDK.Plugins.Applications
{
    using RDK.Applications;
    using RDK.Operations;
    using System.Diagnostics.Contracts;

    public sealed class RedoOperationFactory : PluginOperationFactory
    {
        public RedoOperationFactory()
            : base("Redo")
        {
        }

        protected override bool CanCreateOperation(params object[] args)
        {
            var manager = GlobalManager.GetOperationManager();
            Contract.Assume(manager != null);

            return manager.CanRedo();
        }

        protected override Operation CreateOperation(params object[] args)
        {
            return new RedoOperation();
        }
    }
}
