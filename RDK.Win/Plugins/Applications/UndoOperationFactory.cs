namespace RDK.Plugins.Applications
{
    using RDK.Applications;
    using RDK.Operations;
    using System.Diagnostics.Contracts;

    public sealed class UndoOperationFactory : PluginOperationFactory
    {
        public UndoOperationFactory()
            : base("Undo")
        {
        }

        protected override bool CanCreateOperation(params object[] args)
        {
            var manager = GlobalManager.GetOperationManager();
            Contract.Assume(manager != null);

            return manager.CanUndo();
        }

        protected override Operation CreateOperation(params object[] args)
        {
            return new UndoOperation();
        }
    }
}
