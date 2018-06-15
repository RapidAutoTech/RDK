namespace RDK.Plugins.Applications
{
    using RDK.Managements;
    using RDK.Modules.ScriptEditor;

    /// <summary>
    /// スクリプトエディタを追加するためのツールファクトリクラスです。
    /// </summary>
    public sealed class ScriptEditorToolFactory : PluginToolFactoryWin
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public ScriptEditorToolFactory()
            : base("ScriptEditor", "ScriptEditor")
        {
        }

        /// <summary>
        /// ビューを作成します。
        /// </summary>
        /// <returns>ビュークラスのインスタンスを返します。</returns>
        public override IControlView CreateView()
        {
            return new ScriptEditorView();
        }

        /// <summary>
        /// ツールビューモデルを作成します。
        /// </summary>
        /// <returns>ツールビューモデルのインスタンスを返します。</returns>
        public override IToolable CreateViewModel()
        {
            var viewModel = new ScriptEditorViewModel();
            viewModel.Title = "ScriptEditor";
            return viewModel;
        }
    }
}
