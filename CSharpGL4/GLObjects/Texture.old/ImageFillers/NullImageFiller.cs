﻿using System;

namespace CSharpGL
{
    /// <summary>
    /// build texture's content with IntPtr.Zero.
    /// </summary>
    public class NullImageFiller : ImageFiller
    {
        private int width;
        private int height;
        private int internalFormat;
        private uint format;
        private uint type;

        /// <summary>
        /// build texture's content with IntPtr.Zero.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="internalFormat"></param>
        /// <param name="format"></param>
        /// <param name="type"></param>
        public NullImageFiller(int width, int height, int internalFormat, uint format, uint type)
        {
            this.width = width;
            this.height = height;
            this.internalFormat = internalFormat;
            this.format = format;
            this.type = type;
        }

        /// <summary>
        ///
        /// </summary>
        public override void Fill()
        {
            GL.Instance.TexImage2D((uint)TextureTarget.Texture2D, 0,
                internalFormat,// GL.GL_RGBA,
                width, height, 0,
                this.format,// GL.GL_RGBA,
                this.type,// GL.GL_UNSIGNED_BYTE,
                IntPtr.Zero);
        }
    }
}