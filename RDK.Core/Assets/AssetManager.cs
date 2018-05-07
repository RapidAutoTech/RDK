namespace RDK.Assets
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using RDK.Managements;

    /// <summary>
    /// アセットマネージャークラスです。
    /// </summary>
    public sealed class AssetManager : Manager, IAssetManager
    {
        private readonly Dictionary<Type, Asset> assets =
            new Dictionary<Type, Asset>();

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public AssetManager()
            : base(Enum.GetName(typeof(ManagerKind), ManagerKind.Asset))
        {
        }

        /// <summary>
        /// アセットを取得します。
        /// </summary>
        /// <typeparam name="TAsset">取得対象のリポジトリの型です。</typeparam>
        /// <returns>指定リポジトリのインスタンスを返します。</returns>
        public TAsset GetAsset<TAsset>() where TAsset : Asset
        {
            lock (this.SyncObj)
            {
                var type = typeof(TAsset);
                Asset asset = null;
                if (this.assets.TryGetValue(type, out asset))
                {
                    return asset as TAsset;
                }

                return null;
            }
        }

        /// <summary>
        /// アセットを追加します。
        /// </summary>
        /// <typeparam name="TAsset">追加するアセットの型です。</typeparam>
        /// <param name="asset">追加するアセットです。</param>
        public void AddAsset<TAsset>(TAsset asset) where TAsset : Asset
        {
            Contract.Assume(asset != null);

            lock (this.SyncObj)
            {
                var type = typeof(TAsset);
                if (!this.assets.ContainsKey(type))
                {
                    this.assets.Add(type, asset);
                }
            }
        }

        /// <summary>
        /// アセットを所有しているか判定します。
        /// </summary>
        /// <typeparam name="TAsset">アセットの型です。</typeparam>
        /// <returns>所有している場合は、真を返します。</returns>
        public bool HasAsset<TAsset>() where TAsset : Asset
        {
            lock (this.SyncObj)
            {
                var type = typeof(TAsset);
                return this.assets.ContainsKey(type);
            }
        }

        internal IEnumerable<Asset> GetAssets()
        {
            lock (this.SyncObj)
            {
                return this.assets.Values;
            }
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
            this.assets.Clear();
        }
    }
}
