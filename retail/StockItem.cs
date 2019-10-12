#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Data.Mongo;
using Data.Mongo.Extensions;
using Data.Mongo.Interfaces;
using Retail.Interfaces;
using Utility.Extensions;

namespace Retail
{
    [Serializable]
    public class StockItem : IItem
    {
        [NonSerialized]
        private static readonly IMongoService
            <IItem> _svc = new MongoService<IItem>();
        private const string _trademark_pattern =
@"^(?<brand>.*?)[™©®](?:.*?)$";
        private const string _kroger_pattern =
@"^(?<brand>kroger)(?:.*?)$";
        internal string _brand;
        internal string _category;
        internal string _counting;
        [NonSerialized]
        internal DateTime _created;
        internal string _description;
        internal string? _itemSellingPrices;
        internal bool _hasElectronicCoupon;
        internal string _id;
        internal IEnumerable<IHTMLImage>? _images;
        internal bool _isActive;
        internal bool _isQuantityEntryRequired;
        internal bool _isWeightEntryRequired;
        [NonSerialized]
        internal DateTime _modified;
        [NonSerialized]
        internal Uri? _uri;
        internal string _longDescription;
        internal string _name;
        internal string _posName;
        internal string _retailPackageSize;
        internal string _subBrand;
        internal double _unitPriceFactor;
        internal string _upc;
        public StockItem()
        {
            _isActive = true;
            _brand
                = _category
                = _counting
                = _description
                = _id
                = _longDescription
                = _name
                = _posName
                = _retailPackageSize
                = _subBrand
                = _upc
                    = string.Empty;
            _created
                = _modified
                    = DateTime.UtcNow;
            _hasElectronicCoupon
                = _isQuantityEntryRequired
                = _isWeightEntryRequired
                    = false;
            _unitPriceFactor
                = 1;
        }
        public virtual string Brand 
        {
            get => _brand;
            set =>
                _brand = value
               ?? _brand;
        }
        public virtual ICategory? Category
        {
            get =>
                !string.IsNullOrEmpty(_category)
                    ? new Category().GetDoc<ICategory>(
                        _category)
                : null;
            set
            {
                _category =
                    value == null
                        ? _category
                    : !string.IsNullOrEmpty(
                        value.Id)
                        ? value.Id
                    : value.Save()?.Id
                    ?? string.Empty;
                _isWeightEntryRequired =
                    _isWeightEntryRequired
                        ? _isWeightEntryRequired
                    : value?.IsWeightEntryRequired
                    ?? _isWeightEntryRequired;
            }
        }
        public virtual string Counting
        {
            get =>
                _counting;
            set =>
                _counting =
                value ?? _counting;
        }
        public virtual string Created
        {
            get =>
                _created
                    .ToLocalTime()
                        .ToShortDateString();
        }
        public virtual string Description
        {
            get =>
                _description;
            set
            {
                _description =
                    value ?? _description;
                ParseDescriptors(
                    value);
                ParseBranding(value);
            }
        }
        public virtual string Id
        {
            get => _id;
            set =>
                _id =
                value ?? _id;
        }
        public virtual bool IsActive
        {
            get =>
                _isActive;
            set =>
                _isActive = value;
        }
        public virtual IEnumerable <IHTMLImage>? Images
        {
            get =>
                _images?.ToList()
                ?? new List<IHTMLImage>();
        }
        public virtual IItemSellingPrices? ItemSellingPrices
        {
            get =>
            !string.IsNullOrEmpty(
                _itemSellingPrices)
                ? new ItemSellingPrices()
                    .GetDoc<IItemSellingPrices>(
                        _itemSellingPrices)
                        ?? null
                    : null;
            set =>
            _itemSellingPrices
                = value != null
                    ? !string.IsNullOrEmpty(
                        value.Id)
                        ? value.Id
                    : value.Save()?.Id
                    ?? string.Empty
                : string.Empty;
        }
        public virtual bool IsQuantityEntryRequired
        {
            get =>
                _isQuantityEntryRequired;
            set =>
                _isQuantityEntryRequired = value;
        }
        public virtual bool IsWeightEntryRequired
        {
            get =>
                _isWeightEntryRequired;
            set =>
                _isWeightEntryRequired = value;
        }
        public virtual bool HasElectronicCoupon
        {
            get =>
                _hasElectronicCoupon;
            set =>
                _hasElectronicCoupon = value;
        }
        public virtual string Link
        {
            get =>
                _uri != null
                    ? _uri.ToString()
                : string.Empty;
            set =>
               _uri =
                    !string.IsNullOrEmpty(
                        value)
                        ? Uri.TryCreate(
                            value
                            , UriKind.Absolute
                       , out Uri? uri)
                       ? uri ?? null
                : null
            : null;
        }
        public virtual string LongDescription
        {
            get =>
                _longDescription;
            set
            {
                _longDescription =
                   value ?? _longDescription;
                ParseDescriptors(
                    value);
                ParseBranding(value);
            }
        }
        public virtual string Modified
        {
            get =>
                _modified
                    .ToLocalTime()
                    .ToShortDateString();
            set =>
                _modified =
                    !string.IsNullOrEmpty(
                        value)
                        ? DateTime.TryParse(
                            value
                            , CultureInfo
                                .CurrentCulture
                                .DateTimeFormat
                            , DateTimeStyles
                                .AssumeLocal
                            , out DateTime result)
                        ? result.ToUniversalTime()
                        : DateTime.UtcNow
                : DateTime.UtcNow;
        }
        public virtual string Name
        {
            get =>
                _name;
            set
            {
                _name =
                    value ?? _name;
                ParseDescriptors(
                    value);
                (string b, string s) =
                    ParseBranding(value);
                _brand = b;
                _subBrand = s;
            }
        }
        public virtual string POSName
        {
            get =>
                _posName;
            set =>
                _posName =
                value ?? _posName;
        }
        public virtual IMongoService<IItem> Svc => _svc;
        public virtual string SubBrand
        {
            get =>
                _subBrand;
            set =>
                _subBrand =
                value ?? _subBrand;
        }
        public virtual string UnitPriceFactor
        {
            get =>
                _unitPriceFactor
                    .ToString(
                        "F2"
                        , CultureInfo
                        .CurrentCulture);
            set =>
                _unitPriceFactor =
                    !string.IsNullOrEmpty(
                        value)
                        ? double.TryParse(
                            value
                            , NumberStyles
                                .Float
                            , CultureInfo
                                .CurrentCulture
                                .NumberFormat
                            , out double factor)
                            ? factor
                        : _unitPriceFactor
                : _unitPriceFactor;
        }
        public virtual string UPC
        {
            get =>
                _upc.Trim()
                .PadLeft(
                    13, '0');
            set
            {
                _upc =
                    string.IsNullOrEmpty(
                        value)
                        ? _upc
                    : value.Length > 13
                        ? _upc
                        : value.TryMatch(
                            @"^\d{1,13}"
                            , out Match? upc)
                        ? upc != null
                            ? upc.Success
                                ? value
                                .Trim()
                                .TrimStart('0')
                            : !string.IsNullOrEmpty(
                                _upc)
                                ? _upc
                            : _upc
                        : _upc
                    : _upc;
            }
        }
        public virtual string RetailPackageSize
        {
            get =>
                _retailPackageSize;
            set
            {
                if (!string.IsNullOrEmpty(
                        value))
                {
                    string pounds =
@"^(?:.*?)((\d{1,3}\.\d{2}\s?/\s?)?(1\s?)?lb)(?:.*?)$";
                    string size =
@"^((?:.*?)(?<factor>\d{1,}\.?\d?\d?)\s*)?(?<counting>.*?)$";
                    _retailPackageSize = value;
                    _isWeightEntryRequired =
                        _isWeightEntryRequired
                            ? _isWeightEntryRequired
                            : value.TryMatch(
                                pounds
                                , out Match? lbs)
                                ? lbs != null
                                    ? true
                                    : _isWeightEntryRequired
                                : _isWeightEntryRequired;
                    _counting =
                        string.IsNullOrEmpty(
                            _counting)
                            ? value.TryMatch(
                                size
                                , "counting"
                                , out string? units)
                                ? units?.Trim()
                                    .ToLower(
                                    CultureInfo
                                    .CurrentCulture)
                                    ?? _counting
                                : _counting
                        : _counting;
                    UnitPriceFactor =
                        _unitPriceFactor == 1
                            ? value.TryMatch(
                                size
                                , "factor"
                                , out string? factor)
                                ? !string.IsNullOrEmpty(factor)
                                    ? factor
                            : UnitPriceFactor
                        : UnitPriceFactor
                    : UnitPriceFactor;
                }
            }
        }
        public virtual IItem AddImage(
            IHTMLImage image)
        {
            if (image == null)
                throw new
                    ArgumentNullException(
                        nameof(
                            image));
            if (_images == null)
                _images = new List<IHTMLImage>()
                { image };
            else if (
                !(_images.ToList()
                .Contains<IHTMLImage>(
                    image
                    , new
                    IHTMLImageEqualityComparer()
                )))
                _images.ToList().Add(image);
            return
                (IItem)this;
        }
        public virtual object Clone()
        {
            return
                new StockItem()
                {

                    _brand =
                        _brand,
                    _category =
                        _category,
                    _counting =
                        _counting,
                    _description =
                        _description,
                    _hasElectronicCoupon =
                        _hasElectronicCoupon,
                    _images =
                        _images,
                    _isActive =
                        _isActive,
                    _isQuantityEntryRequired =
                        _isQuantityEntryRequired,
                    _isWeightEntryRequired =
                        _isWeightEntryRequired,
                    _itemSellingPrices =
                        _itemSellingPrices,
                    _longDescription =
                        _longDescription,
                    _name =
                        _name,
                    _posName =
                        _posName,
                    _retailPackageSize =
                        _retailPackageSize,
                    _subBrand =
                        _subBrand,
                    _unitPriceFactor =
                        _unitPriceFactor,
                    _upc =
                        _upc,
                    _uri =
                        _uri,
                };
        }
        public override string? ToString()
        {
            return
                "ITEM DETAILS:" +
             $"{nameof(Id)}: {Id}" + "\r\n" +
             $"{nameof(Name)}: {Name}" + "\r\n" +
             $"{nameof(Brand)}: {Brand}" + "\r\n" +
             $"{nameof(SubBrand)}: {SubBrand}" + "\r\n" +
             $"{nameof(Counting)}: {Counting}" + "\r\n" +
             $"{nameof(Created)}: {Created}" + "\r\n" +
             $"{nameof(Modified)}: {Modified}" + "\r\n" +
             $"{nameof(Description)}: {Description}" + "\r\n" +
             $"{nameof(HasElectronicCoupon)}: {HasElectronicCoupon.ToString(CultureInfo.CurrentCulture)}" + "\r\n" +
             $"{nameof(IsActive)}: {IsActive.ToString(CultureInfo.CurrentCulture)}" + "\r\n" +
             $"{nameof(IsQuantityEntryRequired)}: {IsQuantityEntryRequired.ToString(CultureInfo.CurrentCulture)}" + "\r\n" +
             $"{nameof(IsWeightEntryRequired)}: {IsWeightEntryRequired.ToString(CultureInfo.CurrentCulture)}" + "\r\n" +
             $"{nameof(Link)}: {Link}" + "\r\n" +
             $"{nameof(LongDescription)}: {LongDescription}" + "\r\n" +
             $"{nameof(POSName)}: {POSName}" + "\r\n" +
             $"{nameof(RetailPackageSize)}: {RetailPackageSize}" + "\r\n" +
             $"{nameof(UnitPriceFactor)}: {UnitPriceFactor}" + "\r\n" +
             $"{nameof(UPC)}: {UPC}" + "\r\n" +
            ItemSellingPrices?.ToString()
            ?? string.Empty +
            Category?.ToString()
            ?? string.Empty;
            }
        public virtual bool TryValidateDoc(
            out dynamic? result)
        {
            IItem? doc =
                new StockItem()
                .GetDoc<IItem>("_upc", _upc)
                ?? null;
            _id =
                doc != null
            ? !string.IsNullOrEmpty(
                _id)
                ? !_id.Equals(
                    doc.Id
                    , StringComparison
                    .CurrentCultureIgnoreCase)
                    ? doc.Id
                : _id
            : _id
        : _id;
            result =
                string.IsNullOrEmpty(
                    _id)
                    ? doc ?? this
                : this;
            return
                result != null
                    ? result != this
                        ? false
                    : true
                : false;
        }
        private string ParseBrand(
            string? descriptor)
        {
            if (!string.IsNullOrEmpty(
                _brand))
                return _brand;
            return
                !string.IsNullOrEmpty(
                   descriptor)
                        ? descriptor.TryMatch(
                            _kroger_pattern
                            , "brand"
                            , out string? krogerBrand)
                            ? krogerBrand ?? string.Empty
                        : descriptor.TryMatch(
                            _trademark_pattern
                           , "brand"
                           , out string? trademarkBrand)
                            ? trademarkBrand ?? string.Empty
                            : descriptor.Split(' ')[0]
                            .Trim().ToLower(
                            CultureInfo.
                            CurrentCulture)
                    : string.Empty;
        }
        private (string brand, string subBrand) ParseBranding(
            string? descriptor)
        {
            return
                (ParseBrand(
                            descriptor)
                        , ParseSubBrand(
                            descriptor));
        }
        private string? ParseDescriptors(
            string? descriptor)
        {
            if (!string.IsNullOrEmpty(
                descriptor))
            {
                if (string.IsNullOrEmpty(
                    _name))
                    _name = descriptor;
                if (string.IsNullOrEmpty(
                    _description))
                    _description = descriptor;
                if (string.IsNullOrEmpty(
                    _longDescription))
                    _longDescription = descriptor;
            }
            return descriptor;
        }
        private string ParseSubBrand(
            string? descriptor)
        {
            if (!string.IsNullOrEmpty(
                _subBrand))
                return _subBrand;
            if (string.IsNullOrEmpty(
                _brand))
                return string.Empty;
            string subDescriptor =
                        !string.IsNullOrEmpty(
                            descriptor)
                            ? descriptor.Contains(
                                _brand
                                , StringComparison
                                .CurrentCultureIgnoreCase)
                                ? descriptor.Replace(
                                    _brand
                                    , null
                                    , true
                                    , CultureInfo.CurrentCulture)
                            : descriptor
                        : string.Empty;
            return
                !string.IsNullOrEmpty(
                    subDescriptor)
                    ? subDescriptor.TryMatch(
                        _trademark_pattern
                        , "brand"
                        , out string? subBrand)
                        ? subBrand ?? string.Empty
                     : subDescriptor.Split(' ')[0]
                        .Trim().ToLower(
                            CultureInfo.
                            CurrentCulture)
                    : string.Empty;
            } 
    }
}
