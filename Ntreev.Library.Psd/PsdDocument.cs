using Ntreev.Library.Psd.Readers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd
{
    public class PsdDocument : IPsdLayer, IDisposable
    {
        private FileHeaderReader fileHeader;
        private ColorModeDataReader colorModeData;
        private ImageResourceReader imageResources;
        private ImageDataReader imageData;
        private LayerAndMaskReader layerAndMask;
       
        private PsdReader reader;
        private Uri baseUri;

        public PsdDocument()
        {

        }

        internal PsdDocument(Uri baseUri)
        {
            this.baseUri = baseUri;
        }

        public void Read(string filename)
        {
            this.Read(filename, new PathResolver());
        }

        public void Read(string filename, PsdResolver resolver)
        {
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            this.baseUri = new Uri(Path.GetDirectoryName(filename) + Path.AltDirectorySeparatorChar, UriKind.RelativeOrAbsolute);
            if (this.baseUri.IsAbsoluteUri == false)
                this.baseUri = new Uri(new Uri(Directory.GetCurrentDirectory() + Path.AltDirectorySeparatorChar), this.baseUri);
            this.Read(stream, resolver);
        }

        public void Read(Stream stream)
        {
            this.Read(stream, null);
        }

        public void Read(Stream stream, PsdResolver resolver)
        {
            this.reader = new PsdReader(stream, resolver);
            this.fileHeader = new FileHeaderReader(this.reader);
            this.colorModeData = new ColorModeDataReader(this.reader);
            this.imageResources = new ImageResourceReader(this.reader);
            this.layerAndMask = new LayerAndMaskReader(this.reader, this);
            this.imageData = new ImageDataReader(this.reader, this);
        }

        public void Dispose()
        {
            if (this.reader == null)
                return;

            this.reader.Dispose();
            this.reader = null;
            this.OnDisposed(EventArgs.Empty);
        }

        public FileHeader FileHeader
        {
            get { return this.fileHeader.Value; }
        }

        public byte[] ColorModeData
        {
            get { return this.colorModeData.Value; }
        }

        public int Width
        {
            get { return this.fileHeader.Value.Width; }
        }

        public int Height
        {
            get { return this.fileHeader.Value.Height; }
        }

        public int Depth
        {
            get { return this.fileHeader.Value.Depth; }
        }

        public IEnumerable<IPsdLayer> Childs
        {
            get { return this.layerAndMask.Layers; }
        }

        public IEnumerable<ILinkedLayer> LinkedLayers
        {
            get { return this.layerAndMask.LinkedLayers; }
        }

        public IProperties Resources
        {
            get { return this.imageResources; }
        }

        public bool HasImage
        {
            get { return this.imageResources.ToBoolean("Version", "HasCompatibilityImage"); }
        }

        public event EventHandler Disposed;

        internal Uri BaseUri
        {
            get
            {
                if (this.baseUri == null)
                    return new Uri(Directory.GetCurrentDirectory());
                return this.baseUri;
            }
        }

        protected virtual void OnDisposed(EventArgs e)
        {
            if (this.Disposed != null)
            {
                this.Disposed(this, e);
            }
        }

        #region IPsdLayer

        IPsdLayer IPsdLayer.Parent
        {
            get { return null; }
        }

        bool IPsdLayer.IsClipping
        {
            get { return false; }
        }

        PsdDocument IPsdLayer.Document
        {
            get { return this; }
        }

        ILinkedLayer IPsdLayer.LinkedLayer
        {
            get { return null; }
        }

        string IPsdLayer.Name
        {
            get { return "Root"; }
        }

        int IPsdLayer.Left
        {
            get { return 0; }
        }

        int IPsdLayer.Top
        {
            get { return 0; }
        }

        int IPsdLayer.Right
        {
            get { return this.Width; }
        }

        int IPsdLayer.Bottom
        {
            get { return this.Height; }
        }

        BlendMode IPsdLayer.BlendMode
        {
            get { return BlendMode.Normal; }
        }

        IChannel[] IImageSource.Channels
        {
            get { return this.imageData.Value; }
        }

        float IImageSource.Opacity
        {
            get { return 1.0f; }
        }

        #endregion
    }
}
