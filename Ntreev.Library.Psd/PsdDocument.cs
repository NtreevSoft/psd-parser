using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd
{
    public class PsdDocument : IPsdLayer, IDisposable
    {
        private FileHeader fileHeader;
        private ColorModeDataReader colorModeData;
        private ImageResourceReader imageResources;
        private ImageDataReader imageDataReader;
        private LayerAndMaskReader layerAndMaskReader;
       
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
            this.imageResources = new ImageResourceReader(reader);
            this.layerAndMaskReader = new LayerAndMaskReader(reader, this);
            this.imageDataReader = new ImageDataReader(this.reader, this);
            //this.imagePosition = this.reader.Position;
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
            get { return this.fileHeader; }
        }

        public byte[] ColorModeData
        {
            get { return this.colorModeData.Data; }
        }

        public int Width
        {
            get { return this.fileHeader.Width; }
        }

        public int Height
        {
            get { return this.fileHeader.Height; }
        }

        public int Depth
        {
            get { return this.fileHeader.Depth; }
        }

        public IEnumerable<IPsdLayer> Childs
        {
            get { return this.layerAndMaskReader.Layers; }
        }

        public IEnumerable<ILinkedLayer> LinkedLayers
        {
            get { return this.layerAndMaskReader.LinkedLayers; }
        }

        public IProperties Properties
        {
            get { return this.imageResources; }
        }

        public IProperties ImageResources
        {
            get { return this.imageResources; }
        }

        public bool HasImage
        {
            get { return (bool)this.imageResources["0x0421.HasCompatibilityImage"]; }
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
            get { return this.imageDataReader.Value; }
        }

        float IImageSource.Opacity
        {
            get { return 1.0f; }
        }

        #endregion
    }
}
