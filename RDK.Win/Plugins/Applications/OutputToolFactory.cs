namespace RDK.Plugins.Applications
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using RDK.Applications;
    using RDK.Logs;
    using RDK.Managements;
    using RDK.Modules.Output;
    using RDK.ViewModels;

    /// <summary>
    /// アウトプットツールを追加するためのツールファクトリクラスです。
    /// </summary>
    public sealed class OutputToolFactory : PluginToolFactory
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public OutputToolFactory()
            : base("Output", "Output")
        {
        }

        /// <summary>
        /// ビューを作成します。
        /// </summary>
        /// <returns>ビュークラスのインスタンスを返します。</returns>
        public override IControlView CreateView()
        {
            return new OutputView();
        }

        /// <summary>
        /// ツールビューモデルを作成します。
        /// </summary>
        /// <returns>ツールビューモデルのインスタンスを返します。</returns>
        public override IToolable CreateViewModel()
        {
            var manager = GlobalManager.GetLogManager() as LogManager;
            Contract.Assume(manager != null);

            var factory = manager.Factory as OutputLogFactory;
            Contract.Assume(factory != null);

            var viewModel = new OutputViewModel();
            factory.SetViewModel(viewModel);
            viewModel.Title = "Output";
            return viewModel;
        }
    }
}
