namespace RDK.Applications
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using RDK.Bridges;
    using RDK.Helpers;
    using RDK.Logs;
    using RDK.Menus;
    using RDK.Operations;
    using RDK.Panels;
    using RDK.Plugins;
    using RDK.Assets;
    using RDK.Managements;
    using RDK.ViewModels;

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

        /*
        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <returns>タスクを返します。</returns>
        public static async Task Initialize()
        {
            var pluginManager = Instance.GetManager<PluginManager>(PluginManagerKey);
            Contract.Assume(pluginManager != null);

            var menuPresenter = Instance.GetManager<MenuManager>(MenuManagerKey);
            Contract.Assume(menuPresenter != null);

            var scriptPresenter = Instance.GetManager<ScriptManager>(ScriptManagerKey);
            Contract.Assume(scriptPresenter != null);

            await pluginManager.Initialize();

            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetMenuFactories())
                {
                    foreach (var menu in factory.GetMenus())
                    {
                        menuPresenter.AddMenu(menu.Kind, menu);
                    }
                }

                foreach (var menu in CreateViewMenus())
                {
                    menuPresenter.AddViewMenu(menu);
                }
            });

            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetOperationFactories())
                {
                    factory.SetCreateMessageDelegate(scriptPresenter.Bridge.CreateMessage);
                }
            });

            await Task.Run(() =>
            {
                scriptPresenter.Script.SetRunScriptOperationDelegate(Instance.CallRunScriptOperation);
                scriptPresenter.Script.SetEchoInfoDelegate(Instance.CallEchoInfo);
                scriptPresenter.Script.SetEchoErrorDelegate(Instance.CallEchoError);
            });

            await Task.Run(() =>
            {
                var assetManager = Instance.GetManager<AssetManager>(AssetManagerKey);
                Contract.Assume(assetManager != null);

                foreach (var asset in assetManager.GetAssets())
                {
                    Instance.AssetActiveDocumentChanged += asset.CallActiveDocumentChanged;
                }
            });

            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetDialogFactories())
                {
                    factory.Owner = Instance.Owner;
                }
            });
        }
        */
    }
}
