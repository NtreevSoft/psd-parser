using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public abstract class PsdResolver
    {
        public abstract PsdDocument GetDocument(Uri absoluteUri);

        public virtual Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            if ((baseUri == null) || (!baseUri.IsAbsoluteUri && (baseUri.OriginalString.Length == 0)))
            {
                Uri uri = new Uri(relativeUri, UriKind.RelativeOrAbsolute);
                if (!uri.IsAbsoluteUri && (uri.OriginalString.Length > 0))
                {
                    uri = new Uri(Path.GetFullPath(relativeUri));
                }
                return uri;
            }
            if ((relativeUri == null) || (relativeUri.Length == 0))
            {
                return baseUri;
            }
            if (!baseUri.IsAbsoluteUri)
            {
                throw new NotSupportedException("PSD_RelativeUriNotSupported");
            }
            return new Uri(baseUri, relativeUri);
        }
    }
}
