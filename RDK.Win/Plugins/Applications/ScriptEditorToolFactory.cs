namespace RDK.Plugins.Applications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using RDK.Managements;
    using RDK.Modules.ScriptEditor;
    using RDK.ViewModels;

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
