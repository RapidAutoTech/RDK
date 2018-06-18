namespace RDK.Plugins
{
    using System.Diagnostics.Contracts;
    using System.Windows.Controls;
    using RDK.Managements;
    using RDK.ViewModels;

    /// <summary>
    /// プラグインツールファクトリクラスです。
    /// </summary>
    public abstract class PluginToolFactory : PluginFactory
    {
        private string label = string.Empty;
        private ToolViewModel viewModelCache = null;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="factoryKey">ファクトリキーです。</param>
        /// <param name="label">ツールのラベル名です。</param>
        protected PluginToolFactory(string factoryKey, string label)
            : base(factoryKey)
        {
            Contract.Requires(label != null);
            this.label = label;
        }

        /// <summary>
        /// ラベル名を取得します。
        /// </summary>
        public string Label
        {
            get
            {
                return this.label;
            }
        }

        /// <summary>
        /// ビューを作成します。
        /// </summary>
        /// <returns>ビュークラスのインスタンスを返します。</returns>
        public abstract Control CreateView();

        /// <summary>
        /// ツールビューモデルを作成します。
        /// </summary>
        /// <returns>ツールビューモデルのインスタンスを返します。</returns>
        public abstract ToolViewModel CreateViewModel();

        /// <summary>
        /// ツールを作成します。
        /// </summary>
        /// <returns>ツールのビューとビューモデルを関連付けてビューモデルのインスタンスを返します。</returns>
        internal ToolViewModel CreateTool()
        {
            if (this.viewModelCache != null)
            {
                return this.viewModelCache;
            }

            var view = this.CreateView();
            var viewModel = this.CreateViewModel();

            viewModel.ContentId = this.GetType().FullName;
            viewModel.PanelView = view;
            view.DataContext = viewModel;

            this.viewModelCache = viewModel;
            return viewModel;
        }
    }
}
