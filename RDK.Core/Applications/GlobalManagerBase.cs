namespace RDK.Applications
{
    using RDK.Managements;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 拡張側で処理を行う場合の全体を管理するマネージャークラスです。
    /// </summary>
    public abstract class GlobalManagerBase
    {
        /// <summary>
        /// オペレーションマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string OperationManagerKey;

        /// <summary>
        /// ログマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string LogManagerKey;

        /// <summary>
        /// プラグインマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string PluginManagerKey;

        /// <summary>
        /// パネルマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string PanelManagerKey;

        /// <summary>
        /// メニューマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string MenuManagerKey;

        /// <summary>
        /// アセットマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string AssetManagerKey;

        /// <summary>
        /// スクリプトマネージャーのキー文字列です。
        /// </summary>
        protected static readonly string ScriptManagerKey;

        protected static readonly object SyncRoot = new object();
        protected static readonly Dictionary<string, IManager> Managers =
            new Dictionary<string, IManager>();

        private static string WorkRootPath = string.Empty;

        /// <summary>
        /// 静的クラスのコンストラクタです。
        /// </summary>
        static GlobalManagerBase()
        {
            OperationManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Operation);
            LogManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Log);
            PluginManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Plugin);
            PanelManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Panel);
            MenuManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Menu);
            AssetManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Asset);
            ScriptManagerKey = Enum.GetName(typeof(ManagerKind), ManagerKind.Script);
        }

        public static void SetRootPath(string path)
        {
            WorkRootPath = path;
        }

        public static string GetRootPath()
        {
            return WorkRootPath;
        }
    }
}
