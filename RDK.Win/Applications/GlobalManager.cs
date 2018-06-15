namespace RDK.Applications
{
    using RDK.Applications;
    using RDK.Assets;
    using RDK.Bridges;
    using RDK.Logs;
    using RDK.Managements;
    using RDK.Menus;
    using RDK.Operations;
    using RDK.Panels;
    using RDK.Plugins;
    using RDK.Plugins.Applications;
    using RDK.Plugins.Generic;
    using RDK.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GlobalManager : GlobalManagerBase
    {
        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <returns>タスクを返します。</returns>
        public static async Task Initialize()
        {
            var pluginManager = GetPluginManager() as PluginManager;
            Contract.Assume(pluginManager != null);

            var menuManager = GetMenuManager();
            Contract.Assume(menuManager != null);

            var scriptManager = GetScriptManager();
            Contract.Assume(scriptManager != null);

            await pluginManager.Initialize();
#if true
            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetMenuFactories())
                {
                    foreach (var menu in factory.GetMenus())
                    {
                        menuManager.AddMenu(menu.Kind, menu);
                    }
                }

                foreach (var menu in CreateViewMenus())
                {
                    menuManager.AddViewMenu(menu);
                }
            });


            await Task.Run(() =>
            {
                foreach (var factory in pluginManager.GetOperationFactories())
                {
                    factory.SetCreateMessageDelegate(scriptManager.Bridge.CreateMessage);
                }
            });

            await Task.Run(() =>
            {
                scriptManager.Script.SetRunScriptOperationDelegate(Instance.CallRunScriptOperation);
                scriptManager.Script.SetEchoInfoDelegate(Instance.CallEchoInfo);
                scriptManager.Script.SetEchoErrorDelegate(Instance.CallEchoError);
            });

            await Task.Run(() =>
            {
                var assetManager = GetAssetManager() as AssetManager;
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
#endif
        }

        /// <summary>
        /// メニューマネージャーを取得します。
        /// </summary>
        /// <returns>メニューマネージャーのインスタンスを返します。</returns>
        public static IMenuManager GetMenuManager()
        {
            return Instance.GetManager<IMenuManager>(MenuManagerKey);
        }

        internal static IPanelManager GetPanelManager()
        {
            return Instance.GetManager<IPanelManager>(PanelManagerKey);
        }

        internal static void SetActiveDocument(IDocumentable document)
        {
            Instance.SetActiveDocument(document);
        }

        internal static ScriptManager GetScriptManager()
        {
            return Instance.GetManager<ScriptManager>(ScriptManagerKey);
        }

        internal static PluginToolFactory GetToolFactory(string factoryFullName)
        {
            return Instance.GetManager<PluginManager>(PluginManagerKey).GetToolFactory(factoryFullName);
        }

        internal static ToolViewModel AddTool(PluginToolFactory factory, IPanelManager panelManager)
        {
            Contract.Requires(factory != null);
            Contract.Requires(panelManager != null);

            var viewModel = factory.CreateTool() as ToolViewModel;
            Contract.Requires(viewModel != null);

            viewModel.IsVisible = true;
            viewModel.IsActive = true;

            if (!panelManager.ContainTool(viewModel))
            {
                Instance.ActiveDocumentChanged += viewModel.CallActiveDocumentChanged;

                foreach (var asset in Instance.GetManager<AssetManager>(AssetManagerKey).GetAssets())
                {
                    asset.AssetChanged += viewModel.CallAssetChanged;
                }

                panelManager.AddTool(viewModel);
            }

            return viewModel;
        }

        /// <summary>
        /// デフォルトツールを作成します。
        /// </summary>
        public static void MakeDefaultTools()
        {
            var manager = GlobalManager.GetPanelManager();
            Contract.Assume(manager != null);

            IToolable outputViewModel = null;
            {
                var factory = GetToolFactory(typeof(OutputToolFactory).FullName);
                outputViewModel = AddTool(factory, manager);
            }

            outputViewModel.IsActive = true;
        }

        private static IEnumerable<Menu> CreateViewMenus()
        {
            var pluginManager = GetPluginManager() as PluginManager;
            Contract.Assume(pluginManager != null);

            var panelManager = GetPanelManager();
            Contract.Assume(panelManager != null);

            foreach (var factory in pluginManager.GetToolFactories())
            {
                Menu menu = new Menu(
                    param =>
                    {
                        AddTool(factory, panelManager);
                    },
                    param =>
                    {
                        return true;
                    });

                menu.Label = factory.Label;
                yield return menu;
            }
        }

        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        /// <typeparam name="TDialog">表示するダイアログの型です。</typeparam>
        /// <returns>表示に問題がない場合は、真を返します。</returns>
        public static bool Show<TDialog>()
                where TDialog : class
        {
            var pluginManager = GetPluginManager() as PluginManager;
            Contract.Assume(pluginManager != null);

            var dialog = pluginManager.GetDialogFactory<TDialog>();
            Contract.Assume(dialog != null);

            return dialog.Show();
        }

        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        /// <typeparam name="TDialog">表示するダイアログの型です。</typeparam>
        /// <typeparam name="TArgs">ダイアログの引数の型です。</typeparam>
        /// <param name="args">表示時に渡す引数です。</param>
        /// <returns>表示に問題がない場合は、真を返します。</returns>
        internal static bool Show<TDialog, TArgs>(TArgs args)
            where TDialog : class
            where TArgs : PluginDialogArgs
        {
            var pluginManager = GetPluginManager() as PluginManager;
            Contract.Assume(pluginManager != null);

            var dialog = pluginManager.GetDialogFactory<TDialog>();
            Contract.Assume(dialog != null);

            return ((PluginDialogFactory<TArgs>)dialog).Show(args);
        }
    }
}
