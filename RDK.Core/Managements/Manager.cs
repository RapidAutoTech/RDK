namespace RDK.Managements
{
    using RDK.ComponentModel;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// 管理者クラスです。
    /// </summary>
    public abstract class Manager : DisposableObject, IManager
    {
        private readonly string managerKey;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="key">管理者キーです。</param>
        protected Manager(string key)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));
            this.managerKey = key;
        }

        /// <summary>
        /// 管理者キーを取得します。
        /// </summary>
        public string ManagerKey
        {
            get
            {
                return this.managerKey;
            }
        }
    }
}
