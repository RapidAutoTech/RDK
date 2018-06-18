namespace RDK.Panels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using RDK.Managements;
    using RDK.ViewModels;

    /// <summary>
    /// パネルプレゼンタークラスです。
    /// </summary>
    public abstract class PanelManagerBase : Manager, IPanelManagerBase
    {
        private readonly ObservableCollection<IToolable> tools =
            new ObservableCollection<IToolable>();

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        protected PanelManagerBase()
            : base(Enum.GetName(typeof(ManagerKind), ManagerKind.Panel))
        {
        }

        /// <summary>
        /// ツールを追加します。
        /// </summary>
        /// <param name="tool">追加するツールです。</param>
        public void AddTool(IToolable tool)
        {
            Contract.Assume(tool != null);
            if (!this.tools.Contains(tool))
            {
                this.tools.Add(tool);
            }
        }

        /// <summary>
        /// ツールを削除します。
        /// </summary>
        /// <param name="tool">削除するツールです。</param>
        public void RemoveTool(IToolable tool)
        {
            Contract.Assume(tool != null);
            if (this.tools.Contains(tool))
            {
                this.tools.Remove(tool);
            }
        }

        /// <summary>
        /// 指定のツールが含まれているか判定します。
        /// </summary>
        /// <param name="tool">判定対象のツールです。</param>
        /// <returns>含まれていれば、真を返します。</returns>
        public bool ContainTool(IToolable tool)
        {
            Contract.Assume(tool != null);
            return this.tools.Contains(tool);
        }

        protected ObservableCollection<IToolable> Tools
        {
            get
            {
                return this.tools;
            }
        }
    }
}
