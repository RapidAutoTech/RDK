namespace RDK.ViewModels
{
    using RDK.Assets;
    using RDK.Commands;
    using RDK.Managements;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// ツールのビューモデルクラスです。
    /// </summary>
    public class ToolViewModel : PanelViewModel, IToolable
    {
        private readonly ICommand closeCommand;

        private bool isVisible = true;
        private bool isSelected = false;
        private string contentId = string.Empty;


        public ToolViewModel()
            : base()
        {
            this.IsVisible = true;
            this.closeCommand = new ViewReceiverCommand<object>(param => this.IsVisible = false, param => { return true; });
        }

        /// <summary>
        /// クローズコマンドを取得します。
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                return this.closeCommand;
            }
        }

        /// <summary>
        /// コンテントIDを取得します。
        /// </summary>
        public string ContentId
        {
            get
            {
                return this.contentId;
            }

            internal set
            {
                this.contentId = value;
            }
        }

        /// <summary>
        /// パネル配置を取得します。
        /// </summary>
        public virtual PanelLocation Location
        {
            get
            {
                return PanelLocation.Right;
            }
        }

        /// <summary>
        /// 幅を取得します。
        /// </summary>
        public virtual double Width
        {
            get
            {
                return 300;
            }
        }

        /// <summary>
        /// 高さを取得します。
        /// </summary>
        public double Height
        {
            get
            {
                return 300;
            }
        }

        /// <summary>
        /// 可視状態かどうか取得設定します。
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                this.SetProperty(ref this.isVisible, value);
            }
        }

        /// <summary>
        /// 選択状態かどうか取得設定します。
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.SetProperty(ref this.isSelected, value);
            }
        }

        internal void CallActiveDocumentChanged(object sender, ActiveDocumentChangedEventArgs e)
        {
            Contract.Assume(e != null);

            this.OnActiveDocumentChanged(e);
        }

        internal void CallAssetChanged(object sender, AssetChangedEventArgs e)
        {
            Contract.Assume(e != null);

            this.OnAssetChanged(e);
        }

        /// <summary>
        /// ドキュメントのアクティブ状態変更を通知します。
        /// </summary>
        /// <param name="e">イベント引数です。</param>
        protected virtual void OnActiveDocumentChanged(ActiveDocumentChangedEventArgs e)
        {
        }

        /// <summary>
        /// リポジトリ変更を通知します。
        /// </summary>
        /// <param name="e">イベント引数です。</param>
        protected virtual void OnAssetChanged(AssetChangedEventArgs e)
        {
        }
    }
}
