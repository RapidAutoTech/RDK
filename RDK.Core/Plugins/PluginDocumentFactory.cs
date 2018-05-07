namespace RDK.Plugins
{
    using RDK.Managements;
    using RDK.ViewModels;

    /// <summary>
    /// プラグインドキュメントファクトリクラスです。
    /// </summary>
    public abstract class PluginDocumentFactory : PluginFactory
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="factoryKey">ファクトリキーです。</param>
        protected PluginDocumentFactory(string factoryKey)
            : base(factoryKey)
        {
        }

        /// <summary>
        /// ビューを作成します。
        /// </summary>
        /// <returns>ビュークラスのインスタンスを返します。</returns>
        public abstract IControlView CreateView();

        /// <summary>
        /// ビューモデルを作成します。
        /// </summary>
        /// <returns>ドキュメントビューモデルのインスタンスを返します。</returns>
        public abstract DocumentViewModelBase CreateViewModel();

        /// <summary>
        /// ドキュメントを作成します。
        /// </summary>
        /// <returns>ビューとビューモデルを関連付けたビューモデルのインスタンスを返します。</returns>
        internal DocumentViewModelBase CreateDocument()
        {
            var view = this.CreateView();
            var viewModel = this.CreateViewModel();
            viewModel.PanelView = view;
            return viewModel;
        }
    }
}
