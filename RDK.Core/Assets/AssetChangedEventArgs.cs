namespace RDK.Assets
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// アセット変更イベント引数クラスです。
    /// </summary>
    public sealed class AssetChangedEventArgs : EventArgs
    {
        private readonly Asset target;
        private readonly object changeObject;

        internal AssetChangedEventArgs(Asset target, object changeObject)
        {
            Contract.Requires(target != null);
            Contract.Requires(changeObject != null);

            this.target = target;
            this.changeObject = changeObject;
        }

        /// <summary>
        /// 対象のアセットを取得します。
        /// </summary>
        public Asset Target
        {
            get
            {
                return this.target;
            }
        }

        /// <summary>
        /// 変更オブジェクトを取得します。
        /// </summary>
        public object ChangeObject
        {
            get
            {
                return this.changeObject;
            }
        }
    }
}
