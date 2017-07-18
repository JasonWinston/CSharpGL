﻿using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace CSharpGL
{
    /// <summary>
    /// 用OpenGL初始化和渲染一个模型。
    /// <para>Initialize and render something with OpenGL.</para>
    /// </summary>
    public abstract partial class SceneNodeBase : IWorldSpace, IDisposable
    {
        private const string strSceneNodeBase = "SceneNodeBase";

        /// <summary>
        /// 为便于调试而设置的ID值，没有应用意义。
        /// <para>for debugging purpose only.</para>
        /// </summary>
        [Category(strSceneNodeBase)]
        [Description("为便于调试而设置的ID值，没有应用意义。(for debugging purpose only.)")]
        public int Id { get; private set; }

        private static int idCounter = 0;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}]: [{1}]", this.Id, this.GetType().Name);
        }

        /// <summary>
        /// 用OpenGL初始化和渲染一个模型。
        /// <para>Initialize and render something with OpenGL.</para>
        /// </summary>
        public SceneNodeBase()
        {
            this.Id = idCounter++;

            this.Children = new RendererBaseChildren(this);
        }

        #region Tree

        internal SceneNodeBase parent;
        /// <summary>
        /// 
        /// </summary>
        public SceneNodeBase Parent
        {
            get { return this.parent; }
            set
            {
                SceneNodeBase old = this.parent;
                if (old != value)
                {
                    this.parent = value;

                    if (value == null) // parent != null
                    {
                        old.Children.Remove(this);
                    }
                    else // value != null && parent == null
                    {
                        value.Children.Add(this);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RendererBaseChildren Children { get; private set; }

        #endregion

        #region Initialize()

        private static object synObj = new object();

        //private bool initializing = false;

        ///// <summary>
        ///// in initializing process.
        ///// </summary>
        //public bool Initializing
        //{
        //    get { return initializing; }
        //}

        private bool isInitialized = false;

        /// <summary>
        /// Already initialized.
        /// </summary>
        [Category(strSceneNodeBase)]
        [Description("Is this renderer initialized or not?")]
        public bool IsInitialized { get { return isInitialized; } }

        /// <summary>
        /// Initialize all stuff related to OpenGL.
        /// </summary>
        public void Initialize()
        {
            if (!isInitialized)
            {
                lock (synObj)
                {
                    if (!isInitialized)
                    {
                        //initializing = true;
                        DoInitialize();
                        //initializing = false;
                        isInitialized = true;
                    }
                }
            }
        }

        /// <summary>
        /// This method should only be invoked once.
        /// </summary>
        protected virtual void DoInitialize() { }

        #endregion Initialize()
    }
}