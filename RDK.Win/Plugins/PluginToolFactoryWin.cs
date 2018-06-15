namespace RDK.Plugins
{
    using RDK.Managements;
    using RDK.ViewModels;

    public abstract class PluginToolFactoryWin : PluginToolFactory
    {
        private IToolable viewModelCache = null;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="factoryKey">ファクトリキーです。</param>
        /// <param name="label">ツールのラベル名です。</param>
        protected PluginToolFactoryWin(string factoryKey, string label)
            : base(factoryKey, label)
        {
        }

        internal override IToolable CreateTool()
        {
            if (this.viewModelCache != null)
            {
                return this.viewModelCache;
            }

            var view = this.CreateView();
            var viewModel = this.CreateViewModel();
            var internalViewModel = viewModel as ToolViewModel;
            if (internalViewModel != null)
            {
                internalViewModel.ContentId = this.GetType().FullName;
                internalViewModel.PanelView = view;
            }

            this.viewModelCache = viewModel;
            return viewModel;
        }
    }
}
