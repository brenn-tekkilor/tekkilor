#nullable enable
using System;
using Utility.Extensions;
using System.Globalization;
using Retail.Interfaces;

namespace Retail.Common
{
    [Serializable]
    public class HTMLImage : IHTMLImage
    {
        [NonSerialized]
        internal Uri? _src;
        internal string _alt;
        public HTMLImage()
        {
            _alt = string.Empty;
        }
        public virtual string Src
        {
            get => _src?.ToString() ?? string.Empty;
            set =>
                _src = Uri.TryCreate(value
                    ?? throw new ArgumentNullException(nameof(value))
                    , UriKind.RelativeOrAbsolute, out Uri? result)
                ? result
                : null;
        }
        public virtual string Alt
        {
            get => _alt.ToTitleCase();
            set => _alt = value?.Trim().ToLower(
                CultureInfo.CurrentCulture) ?? string.Empty;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            IHTMLImage image = (IHTMLImage)obj;
            if (image.Src == Src
                    && image.Alt == Alt)
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Src != null
                    ? Src.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                result = (result * 397) ^ (Alt != null
                    ? Alt.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                return result;
            }
        }
    }
}
