namespace RDK.Plugins.Applications
{
    using RDK.Managements;
    using RDK.Modules.ScriptEditor;
    using RDK.ViewModels;
    using System.Windows.Controls;

    /// <summary>
    /// スクリプトエディタを追加するためのツールファクトリクラスです。
    /// </summary>
    public sealed class ScriptEditorToolFactory : PluginToolFactory
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
        public override Control CreateView()
        {
            return new ScriptEditorView();
        }

        /// <summary>
        /// ツールビューモデルを作成します。
        /// </summary>
        /// <returns>ツールビューモデルのインスタンスを返します。</returns>
        public override ToolViewModel CreateViewModel()
        {
            var viewModel = new ScriptEditorViewModel();
            viewModel.Title = "ScriptEditor";
            return viewModel;
        }
    }
}
