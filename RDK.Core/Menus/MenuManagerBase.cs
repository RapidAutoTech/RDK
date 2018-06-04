namespace RDK.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using RDK.Managements;

    /// <summary>
    /// メニューマネージャークラスです。
    /// </summary>
    public abstract class MenuManagerBase : Manager, IMenuManagerBase
    {
        private readonly Dictionary<MenuKind, ObservableCollection<IMenu>> kindToMenus =
            new Dictionary<MenuKind, ObservableCollection<IMenu>>();

        private readonly ObservableCollection<IMenu> viewMenus =
            new ObservableCollection<IMenu>();

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        protected MenuManagerBase()
            : base(Enum.GetName(typeof(ManagerKind), ManagerKind.Menu))
        {
        }

        /// <summary>
        /// メニューを追加します。
        /// </summary>
        /// <param name="kind">追加するメニューの種類です。</param>
        /// <param name="menu">追加するメニューです。</param>
        public void AddMenu(MenuKind kind, IMenu menu)
        {
            Contract.Requires(menu != null);
            this.kindToMenus[kind].Add(menu);
        }

        /// <summary>
        /// ビューメニューを追加します。
        /// </summary>
        /// <param name="menu">追加するメニューです。</param>
        public void AddViewMenu(IMenu menu)
        {
            Contract.Requires(menu != null);
            this.viewMenus.Add(menu);
        }

        protected Dictionary<MenuKind, ObservableCollection<IMenu>> KindToMenus
        {
            get
            {
                return this.kindToMenus;
            }
        }

        protected ObservableCollection<IMenu> ViewMenus
        {
            get
            {
                return this.viewMenus;
            }
        }
    }
}
