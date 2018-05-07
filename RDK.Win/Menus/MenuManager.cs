namespace RDK.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Windows.Data;
    using RDK.Managements;

    /// <summary>
    /// メニューマネージャークラスです。
    /// </summary>
    public sealed class MenuManager : MenuManagerBase
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public MenuManager()
            : base()
        {
            foreach (var key in Enum.GetValues(typeof(MenuKind)))
            {
                var kind = (MenuKind)key;
                ObservableCollection<IMenu> value = new ObservableCollection<IMenu>();
                this.KindToMenus.Add(kind, value);
                BindingOperations.EnableCollectionSynchronization(value, this.SyncObj);
            }

            BindingOperations.EnableCollectionSynchronization(this.ViewMenus, this.SyncObj);
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
            foreach (var value in this.KindToMenus.Values)
            {
                value.Clear();
                BindingOperations.DisableCollectionSynchronization(value);
            }

            this.ViewMenus.Clear();
            BindingOperations.DisableCollectionSynchronization(this.ViewMenus);
        }
    }
}
