﻿namespace RDK.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RDK.ComponentModel;

    /// <summary>
    /// コマンドベースクラスです。
    /// </summary>
    public abstract class CommandBase : NotifiableObject
    {
        private List<WeakReference<EventHandler>> canExecuteChangedHandlers = new List<WeakReference<EventHandler>>();

        /// <summary>
        /// 実行変更可能かのイベントハンドラーを追加削除します。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                this.canExecuteChangedHandlers.Add(new WeakReference<EventHandler>(value));
            }

            remove
            {
                foreach (var weakReference in this.canExecuteChangedHandlers
                    .Where(r =>
                    {
                        EventHandler result;
                        if (r.TryGetTarget(out result) && result == value)
                        {
                            return true;
                        }

                        return false;
                    }).ToList())
                {
                    this.canExecuteChangedHandlers.Remove(weakReference);
                }
            }
        }

        /// <summary>
        /// 実行変更可能かの状態を通知します。
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            this.RaisePropertyChanged();
            this.OnCanExecuteChanged();
        }
       

        /// <summary>
        /// 実行変更可能化の状態を実行します。
        /// </summary>
        protected void OnCanExecuteChanged()
        {
            foreach (var handlerWeakReference in this.canExecuteChangedHandlers.ToList())
            {
                EventHandler result;

                if (handlerWeakReference.TryGetTarget(out result))
                {
                    this.ExecuteEventHandlerInternal(result);
                }
                else
                {
                    this.canExecuteChangedHandlers.Remove(handlerWeakReference);
                }
            }
        }

        protected abstract void ExecuteEventHandlerInternal(EventHandler eventHandler);
    }
}
