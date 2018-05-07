namespace RDK.Assets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RDK.Managements;

    /// <summary>
    /// アセットマネージャーのインターフェースです。
    /// </summary>
    public interface IAssetManager : IManager
    {
        /// <summary>
        /// アセットを取得します。
        /// </summary>
        /// <typeparam name="TAsset">取得対象のアセットの型です。</typeparam>
        /// <returns>指定アセットのインスタンスを返します。</returns>
        TAsset GetAsset<TAsset>() where TAsset : Asset;

        /// <summary>
        /// アセットを追加します。
        /// </summary>
        /// <typeparam name="TRepository">追加するアセットの型です。</typeparam>
        /// <param name="repository">追加するアセットです。</param>
        void AddAsset<TAsset>(TAsset repository) where TAsset : Asset;

        /// <summary>
        /// アセットを所有しているか判定します。
        /// </summary>
        /// <typeparam name="TRepository">アセットの型です。</typeparam>
        /// <returns>所有している場合は、真を返します。</returns>
        bool HasAsset<TAsset>() where TAsset : Asset;
    }
}
