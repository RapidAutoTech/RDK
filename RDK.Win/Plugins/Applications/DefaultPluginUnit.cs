namespace RDK.Plugins.Applications
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(PluginUnit))]
    public sealed class DefaultPluginUnit : PluginUnit
    {
        public DefaultPluginUnit()
            : base("Default")
        { }

        public override IEnumerable<PluginOperationFactory> GetOperationFactories()
        {
            yield return new UndoOperationFactory();
            yield return new RedoOperationFactory();
        }

        public override IEnumerable<PluginToolFactory> GetToolFactories()
        {
            yield return new OutputToolFactory();
            //yield return new ScriptEditorToolFactory();
        }
    }
}
