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
    /// リドゥオペレーションクラスです。
    /// </summary>
    public sealed class RedoOperation : Operation
    {
        private bool isRedo = true;
        private bool result = false;

        /// <summary>
        /// オペレーションラベルを取得します。
        /// </summary>
        public override string Label
        {
            get
            {
                return "Redo";
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
            if (this.isRedo)
            {
                if (operationManager.CanRedo())
                {
                    operationManager.Redo();
                }
            }
            else
            {
                if (operationManager.CanUndo())
                {
                    operationManager.Undo();
                }
            }

            this.isRedo = !this.isRedo;
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
