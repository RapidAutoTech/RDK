﻿namespace RDK.Bridges
{
    using System;
    using System.Diagnostics.Contracts;
    using RDK.Managements;

    /// <summary>
    /// スクリプトマネージャークラスです。
    /// </summary>
    public sealed class ScriptManager : Manager, IScriptManager
    {
        private readonly ScriptBridge scriptBridge;
        private readonly Script script;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="scriptBridge">使用するスクリプトブリッジを渡します。</param>
        public ScriptManager(ScriptBridge scriptBridge)
            : base(Enum.GetName(typeof(ManagerKind), ManagerKind.Script))
        {
            Contract.Assume(scriptBridge != null);
            this.scriptBridge = scriptBridge;
            this.script = new Script();
            this.scriptBridge.SetScript(this.script);
        }

        internal ScriptBridge Bridge
        {
            get
            {
                return this.scriptBridge;
            }
        }

        internal Script Script
        {
            get
            {
                return this.script;
            }
        }

        /// <summary>
        /// 文字列からスクリプトを実行します。
        /// </summary>
        /// <param name="expression">実行するスクリプト式です。</param>
        /// <returns>実行成功ならば真を返します。</returns>
        public bool ExecuteFromString(string expression)
        {
            this.script.Logger.Clear();
            return this.scriptBridge.Execute(expression);
        }

        /// <summary>
        /// ファイルからスクリプトを実行します。
        /// </summary>
        /// <param name="fileName">実行するファイル名です。</param>
        /// <returns>実行成功ならば真を返します。</returns>
        public bool ExecuteFromFile(string fileName)
        {
            this.script.Logger.Clear();
            return this.scriptBridge.ExecuteFromFile(fileName);
        }

        /// <summary>
        /// 廃棄します。内部処理
        /// </summary>
        protected override void DisposeInternal()
        {
        }
    }
}
