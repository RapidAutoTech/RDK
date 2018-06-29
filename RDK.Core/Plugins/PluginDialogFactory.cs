﻿namespace RDK.Plugins
{
    using RDK.Applications;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// プラグインダイアログファクトリクラスです。
    /// </summary>
    public abstract class PluginDialogFactory
    {
        private readonly Type dialogType;
        private readonly bool isModal;
        private object owner = null;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="dialogType">ダイアログタイプです。</param>
        /// <param name="isModal">真の設定の場合、モーダルダイアログとして使用します。</param>
        protected PluginDialogFactory(Type dialogType, bool isModal)
        {
            Contract.Requires(dialogType != null);

            this.dialogType = dialogType;
            this.isModal = isModal;
        }

        /// <summary>
        /// ダイアログタイプを取得します。
        /// </summary>
        public Type DialogType
        {
            get
            {
                return this.dialogType;
            }
        }

        /// <summary>
        /// オーナーを取得します。
        /// </summary>
        internal object Owner
        {
            get
            {
                return this.owner;
            }

            set
            {
                this.owner = value;
            }
        }

        /// <summary>
        /// モーダルであるか状態を取得します。
        /// </summary>
        protected bool IsModal
        {
            get
            {
                return this.isModal;
            }
        }

        /// <summary>
        /// 表示します。
        /// </summary>
        /// <returns>表示処理に問題がなければ、真を返します。</returns>
        public abstract bool Show();
    }
}
