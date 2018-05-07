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

            {
                var factory = GetToolFactory(typeof(ScriptEditorToolFactory).FullName);
                AddTool(factory, manager);
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
    }
}
