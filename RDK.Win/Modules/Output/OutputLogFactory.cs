﻿namespace RDK.Modules.Output
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using RDK.Logs;

    /// <summary>
    /// アウトプットビュー用のログファクトリクラスです。
    /// </summary>
    public sealed class OutputLogFactory : LogFactory
    {
        private OutputViewModel viewModel = null;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public OutputLogFactory()
            : base()
        {
        }

        /// <summary>
        /// ログをフラッシュします。
        /// </summary>
        /// <param name="logs">フラッシュ対象のログです。</param>
        /// <returns>フラッシュ成功ならば真を返します。</returns>
        public override bool Flush(IReadOnlyCollection<Log> logs)
        {
            if (this.viewModel == null)
            {
                return false;
            }

            Contract.Assume(logs != null);

            foreach (var log in logs)
            {
                this.viewModel.AddMessage(log.Message);
            }

            return true;
        }

        internal void SetViewModel(OutputViewModel viewModel)
        {
            Contract.Assume(viewModel != null);
            this.viewModel = viewModel;
        }
    }
}
