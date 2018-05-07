namespace RDK.Managements
{
    /// <summary>
    /// 管理者の種類です。
    /// </summary>
    public enum ManagerKind
    {
        /// <summary>
        /// オペレーション（コマンド処理みたいなもの）
        /// </summary>
        Operation,

        /// <summary>
        /// ログ
        /// </summary>
        Log,

        /// <summary>
        /// パネル
        /// </summary>
        Panel,

        /// <summary>
        /// メニュー
        /// </summary>
        Menu,

        /// <summary>
        /// コンフィグ
        /// </summary>
        Config,

        /// <summary>
        /// アセット（プロジェクトのファイル群単位）
        /// </summary>
        Asset,

        /// <summary>
        /// スクリプト
        /// </summary>
        Script,

        /// <summary>
        /// プラグイン
        /// </summary>
        Plugin
    }

    /// <summary>
    /// 管理者のインターフェースです。
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// 管理者キーを取得します。
        /// </summary>
        string ManagerKey
        {
            get;
        }
    }
}
