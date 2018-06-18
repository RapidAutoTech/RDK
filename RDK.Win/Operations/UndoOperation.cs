namespace RDK.Operations
{
    using RDK.Applications;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// アンドゥオペレーションクラスです。
    /// </summary>
    public sealed class UndoOperation : Operation
    {
        private bool isUndo = true;
        private bool result = false;

        /// <summary>
        /// オペレーションラベルを取得します。
        /// </summary>
        public override string Label
        {
            get
            {
                return "Undo";
            }
        }

        /// <summary>
        /// オペレーションの処理結果を取得します。
        /// </summary>
        public override bool Result
        {
            get
            {
                return this.result;
            }
        }

        /// <summary>
        /// オペレーションの返り値を取得します。
        /// </summary>
        public override object ReturnValue
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// オペレーションを実行します。
        /// </summary>
        /// <returns>実行後のオペレーションを返します。</returns>
        public override Operation Run()
        {
            var operationManager = GlobalManager.GetOperationManager();
            Contract.Assume(operationManager != null);

            this.result = false;
            if (this.isUndo)
            {
                if (operationManager.CanUndo())
                {
                    operationManager.Undo();
                }
            }
            else
            {
                if (operationManager.CanRedo())
                {
                    operationManager.Redo();
                }
            }

            this.isUndo = !this.isUndo;
            this.result = true;
            return this;
        }

        /// <summary>
        /// アンドゥ可能かどうか判定します。
        /// </summary>
        /// <returns>アンドゥ可能ならば真を返します。</returns>
        public override bool CanUndoable()
        {
            return true;
        }
    }
}
