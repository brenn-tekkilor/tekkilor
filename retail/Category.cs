#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Data.Mongo;
using Data.Mongo.Extensions;
using Data.Mongo.Interfaces;
using Retail.Common;
using Retail.Interfaces;
using Utility.Extensions;

namespace Retail
{
    [Serializable]
    public class Category : ICategory
    {
        [NonSerialized]
        private static readonly IMongoService<ICategory> _svc
            = new MongoService<ICategory>();
        [NonSerialized]
        internal Dictionary<string, ICategory>? _children;
        internal CategoryFunction _categoryFunction;
        internal CategoryLevel _categoryLevel;
        [NonSerialized]
        internal DateTime _created;
        internal string _description;
        internal bool _hasElectronicCoupon;
        internal string _id;
        internal bool _isActive;
        internal bool _isQuantityEntryRequired;
        internal bool _isWeightEntryRequired;
        [NonSerialized]
        internal DateTime _modified;
        internal string _name;
        internal string? _parent;
        internal Season _season;
        internal SellingRestriction _sellingRestriction;
        internal string _taxonomyId;
        [NonSerialized]
        internal Uri? _uri;
        public Category()
        {
            _categoryFunction
                = Retail.Common
                .CategoryFunction
                .Operating;
            _categoryLevel
                = Retail.Common
                .CategoryLevel
                .Category;
            _created = _modified
                = DateTime.UtcNow;
            _isActive = true;
            _hasElectronicCoupon
                = _isQuantityEntryRequired
                = _isWeightEntryRequired
                = false;
            _season
                = Retail.Common
                .Season.All;
            _sellingRestriction
                = Retail.Common
                .SellingRestriction.None;
            _description
                = _id
                = _name
                = _taxonomyId
                = string.Empty;
        }
        public virtual string CategoryFunction
        {
            get =>
            _categoryFunction.ToString();
            set =>
            _categoryFunction
                = value != null
                    ? Enum.Parse
                        <CategoryFunction>(
                            value
                            , true)
                : Retail.Common
                .CategoryFunction
                .Operating;
        }
        public virtual string CategoryLevel
        {
            get =>
            _categoryLevel.ToString();
            set =>
            _categoryLevel
                = value != null
                    ? Enum.Parse
                        <CategoryLevel>(
                            value
                            , true)
                    : Retail.Common
                    .CategoryLevel.Category;
        }
        public virtual IEnumerable<ICategory>? Children
        {
            get =>
            _children != null
                ? _children.Any()
                    ? _children
                    .Values
                    .ToList()
                : new List<ICategory>()
            : new List<ICategory>();
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
            set =>
                _description = value
                    ?? throw new
                        ArgumentNullException(
                            nameof(
                                value));
        }
        public virtual bool HasElectronicCoupon
        {
            get =>
                _hasElectronicCoupon;
            set =>
                _hasElectronicCoupon = value;
        }
        public virtual string Id
        {
            get =>
                _id;
            set =>
                _id = value
                    ?? string.Empty;
        }
        public virtual bool IsActive
        {
            get =>
                _isActive;
            set =>
                _isActive = value;
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
        public virtual string Link
        {
            get =>
                _uri != null
                    ? _uri.IsAbsoluteUri
                        ? _uri.AbsoluteUri
                    : _uri.ToString()
                : string.Empty;
            set
            {
                _uri =
                    !string.IsNullOrEmpty(
                        value)
                    ? Uri.TryCreate(
                        value
                        , UriKind.RelativeOrAbsolute
                        , out Uri? uri)
                        ? uri != null
                            ? uri.IsAbsoluteUri
                                ? uri
                                : _uri
                            : _uri
                        : _uri
                    : _uri;
            }
        }
        public virtual string Modified
        {
            get =>
                _modified
                    .ToLocalTime()
                        .ToShortDateString();
            set =>
                _modified
                = value != null
                    ? DateTime.TryParse(
                        value
                        , CultureInfo
                            .CurrentCulture
                            .DateTimeFormat
                        , DateTimeStyles
                            .AssumeLocal
                        , out DateTime modified)
                    ? modified
                    .ToUniversalTime()
                 :DateTime.UtcNow
             : DateTime.UtcNow;
        }
        public virtual string Name
        {
            get =>
                _name;
            set =>
                _name = value
                    ?? throw new
                        ArgumentNullException(
                            nameof(
                                value));
        }
        public virtual ICategory? Parent
        {
            get =>
                string.IsNullOrEmpty(
                    _parent)
                    ? null
                : this.GetDoc<ICategory>(
                    _parent);
            set =>
                _parent =
                    value != null
                        ? !string.IsNullOrEmpty(
                            value.Id)
                            ? value.Id
                        : value.Save()?.Id
                        ?? string.Empty
                        : string.Empty;
        }
        public virtual string Season
        {
            get =>
                _season.ToString();
            set =>
                _season
                = value != null
                    ? Enum.Parse<Season>(
                        value, true)
                : Retail.Common.Season.All;
        }
        public virtual string SellingRestriction
        {
            get =>
                _sellingRestriction.ToString();
            set =>
                _sellingRestriction
                = value != null
                    ? Enum.Parse
                    <SellingRestriction>(
                        value, true)
                : Retail.Common
                .SellingRestriction.None;
        }
        public virtual IMongoService<ICategory> Svc => _svc;
        public virtual string TaxonomyId
        {
            get =>
                _taxonomyId;
            set
            {
                _taxonomyId =
                    !string.IsNullOrEmpty(
                        value)
                        ? value
                : _taxonomyId;
                _isWeightEntryRequired =
                    _isWeightEntryRequired
                        ? _isWeightEntryRequired
                    : ParseIsWeightEntryRequried(
                        _taxonomyId);
                _sellingRestriction =
                    _sellingRestriction != Common.SellingRestriction.None
                    ? _sellingRestriction
                    : ParseSellingRestriction(
                        _taxonomyId);
            }
        }
        public object Clone()
        {
            return
                new Category()
                {

                    _categoryFunction =
                        _categoryFunction,
                    _categoryLevel =
                        _categoryLevel,
                    _children =
                        _children,
                    _description =
                        _description,
                    _hasElectronicCoupon =
                        _hasElectronicCoupon,
                    _isActive =
                        _isActive,
                    _isQuantityEntryRequired =
                        _isQuantityEntryRequired,
                    _isWeightEntryRequired =
                        _isWeightEntryRequired,
                    _name =
                        _name,
                    _parent =
                        _parent,
                    _season =
                        _season,
                    _sellingRestriction =
                        _sellingRestriction,
                    _taxonomyId =
                        _taxonomyId,
                    _uri =
                        _uri,
                };
        }
        public static string ParseTaxonomyId(
            string link)
        {
            return
                !(string.IsNullOrEmpty(link))
                    ? link.Split('/')[^1]
                    .Split('?')[0]
                : string.Empty;
        }
        public override string? ToString()
        {
            return
            "CATEGORY DOCUMENT DETAILS" + "\r\n" +
            $"{nameof(Name)}: {Name}" + "\r\n" +
            $"{nameof(Id)}: {Id}" + "\r\n" +
            $"{nameof(TaxonomyId)}: {TaxonomyId}" + "\r\n" +
            $"{nameof(Created)}: {Created}" + "\r\n" +
            $"{nameof(Modified)}: {Modified}" + "\r\n" +
            $"{nameof(IsActive)}: {IsActive}" + "\r\n" +
            $"{nameof(Link)}: {Link}" + "\r\n" +
            $"{nameof(CategoryFunction)}: {CategoryFunction}" + "\r\n" +
            $"{nameof(CategoryLevel)}: {CategoryLevel}" + "\r\n" +
            $"{nameof(Parent)}: {Parent}" + "\r\n" +
            $"{nameof(Children)}: {Children}" + "\r\n" +
            $"{nameof(IsWeightEntryRequired)}: {IsWeightEntryRequired}" + "\r\n" +
            $"{nameof(IsQuantityEntryRequired)}: {IsQuantityEntryRequired}" + "\r\n" +
            $"{nameof(SellingRestriction)}: {SellingRestriction}" + "\r\n" +
            $"{nameof(Season)}: {Season}" + "\r\n" +
            $"{nameof(HasElectronicCoupon)}: {HasElectronicCoupon}" + "\r\n" +
            $"{nameof(Description)}: {Description}" + "\r\n" + "\r\n";
        }
        public virtual bool TryValidateDoc(
            out dynamic? result)
        {
            ICategory? doc =
                this.GetDoc<ICategory>(
                        "_taxonomyId"
                        , _taxonomyId);

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
        public virtual ICategory? AddChild(ICategory child)
        {
            if (child == null)
                throw new
                    ArgumentNullException(
                        nameof(
                            child));
            child.Parent
                ??= (!string.IsNullOrEmpty(
                    this.Id)
                    ? this
                    : this.Save<ICategory>());
            if (string.IsNullOrEmpty(
                child.Id))
                child.Save();
            if (_children == null)
            {
                _children
                    = new Dictionary
                        <string, ICategory>()
                    {
                        { child.Id, child }
                    };
            }
            else if (!_children.ContainsKey(
                child.Id))
                _children.Add(
                    child.Id, child);
            return
                this.Save
            <ICategory>();
        }
        private static bool ParseIsWeightEntryRequried(
            string taxonomyId)
        {
        foreach (string idx
            in new List<string>()
            {
                "05001",
                "05002",
                "0500600004",
                "05012",
                "05069",
                "06111",
                "06112",
                "06120",
                "13156",
            })
        {
            if (taxonomyId.StartsWith(
                idx
                , StringComparison
                .CurrentCultureIgnoreCase))
                return true;
        }
        return false;
    }
        private static SellingRestriction ParseSellingRestriction(
            string taxonomyId)
        {
            return
                !string.IsNullOrEmpty(
                    taxonomyId)
                    ? taxonomyId
                        .Substring(0, 2)
                    switch
                    {
                        "08" =>
                            Retail
                            .Common
                            .SellingRestriction
                                .Age21,
                        _ =>
                            Retail
                            .Common
                            .SellingRestriction
                                .None,
                    }
                : Retail
                .Common
                .SellingRestriction
                    .None;
        }
    }
}
