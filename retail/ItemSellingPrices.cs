#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using Data.Mongo;
using Data.Mongo.Extensions;
using Data.Mongo.Interfaces;
using Retail.Common;
using Retail.Interfaces;
using Utility.Extensions;

namespace Retail
{
    [Serializable]
    public class ItemSellingPrices : IItemSellingPrices
    {
        [NonSerialized]
        private static readonly IMongoService<IItemSellingPrices> _svc
            = new MongoService<IItemSellingPrices>();
        [NonSerialized]
        internal DateTime _created;
        [NonSerialized]
        internal DateTime _currentPriceEffectiveDate;
        [NonSerialized]
        internal DateTime _currentPriceExpirationDate;
        internal PriceType _currentPriceType;
        internal double _currentUnitPrice;
        internal double _currentQuantityPricingPrice;
        internal double _currentSavings;
        internal int _hash;
        internal string _id;
        internal bool _isActive;
        internal bool _isTaxIncluded;
        [NonSerialized]
        internal DateTime _modified;
        [NonSerialized]
        internal DateTime _permanentPriceEffectiveDate;
        internal PriceType _permanentPriceType;
        internal double _permanentUnitPrice;
        internal double _permanentQuantityPricingPrice;
        internal double _permanentSavings;
        internal int _quantityPricingQuantity;
        public ItemSellingPrices()
        {
           _id = string.Empty;
            _isActive = true;
            _created = _modified =
                _permanentPriceEffectiveDate
                 = _currentPriceEffectiveDate = DateTime.UtcNow;
            _currentPriceExpirationDate
                = DateTime.Today.ToNextWed();
            _currentPriceType = _permanentPriceType
                = PriceType.RegularListPrice;
            _currentQuantityPricingPrice = _currentSavings =
                _currentUnitPrice = _permanentUnitPrice
                = _permanentQuantityPricingPrice =
                _permanentSavings = 0;
            _quantityPricingQuantity = _hash = 0;
            _isTaxIncluded = false;
        }
        public virtual string Created
        {
            get => _created.ToLocalTime().ToShortDateString();
        }
        public virtual string CurrentPriceEffectiveDate
        {
            get => _currentPriceEffectiveDate
                .ToLocalTime().ToShortDateString();
        }
        public virtual string CurrentPriceExpirationDate
        {
            get => _currentPriceExpirationDate
                            .ToLocalTime().ToShortDateString();
            set
            {
                _currentPriceExpirationDate = DateTime.TryParse(
                    value ?? DateTime.Today.ToNextWed()
                    .ToLocalTime().ToShortDateString()
                    , CultureInfo.CurrentCulture.DateTimeFormat,
                    DateTimeStyles.AssumeLocal, out DateTime result)
                        ? result.ToUniversalTime()
                        : DateTime.Today.ToNextWed().ToUniversalTime();
            }
        }
        public virtual string CurrentPriceType
        {
            get => _currentPriceType.ToString();
            set => _currentPriceType =
                !string.IsNullOrEmpty(
                        value)
                        ? Enum.TryParse(
                            value
                            , true
                            , out PriceType priceType)
                            ? priceType
                    : _currentPriceType
                : _currentPriceType;
        }
        public virtual string CurrentUnitPrice
        {
            get => _currentUnitPrice.ToString(
               "F2", CultureInfo.CurrentCulture);
            set
            {
                _currentUnitPrice =
                    !string.IsNullOrEmpty(
                        value)
                        ? value.ToDouble()
                    : _currentUnitPrice;
                _currentPriceEffectiveDate =
                    !string.IsNullOrEmpty(
                        value)
                        ? DateTime.UtcNow
                    : _currentPriceEffectiveDate;
                _currentPriceExpirationDate =
                    !string.IsNullOrEmpty(
                        value)
                        ? _currentPriceExpirationDate
                        .ToNextWedIfLater()
                    : _currentPriceExpirationDate;
            }
        }
        public virtual string CurrentQuantityPricingPrice
        {
            get =>
                _currentQuantityPricingPrice.ToString(
               "F2", CultureInfo.CurrentCulture);
            set =>
                _currentQuantityPricingPrice =
                    !string.IsNullOrEmpty(
                        value)
                        ? value.ToDouble()
                : _currentQuantityPricingPrice;
        }
        public virtual string CurrentSavings
        {
            get => _currentSavings.ToString(
                "F2", CultureInfo.CurrentCulture);

            set
            {
                _currentSavings = value.ToDouble();
                _currentPriceType = _currentSavings == 0
                    ? _permanentPriceType
                    : _currentPriceType == _permanentPriceType
                            ? PriceType.PromotionalSaleDiscount
                            : _currentPriceType;
                _currentUnitPrice =
                    _permanentUnitPrice - _currentSavings;
                _currentPriceEffectiveDate = DateTime.UtcNow;
                _currentPriceExpirationDate
                    = _currentPriceExpirationDate.ToNextWedIfLater();

            }
        }
        public virtual bool HasQuantityPricing =>
            _quantityPricingQuantity > 0 ? true : false;
        public virtual string Id
        {
            get => _id;
            set => _id = value ?? string.Empty;
        }
        public virtual bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }
        public virtual bool IsPricePromo =>
            _permanentUnitPrice == _currentUnitPrice
                ? _permanentSavings == 0
                    ? _currentSavings == 0
                        ? false : true : true : true;
        public virtual bool IsTaxIncluded
        {
            get => _isTaxIncluded;
            set => _isTaxIncluded = value;
        }
        public virtual string Modified
        {
            get => _modified.ToLocalTime().ToShortDateString();
            set => _modified = value != null ? DateTime.TryParse(value
                    , CultureInfo.CurrentCulture.DateTimeFormat
                    , DateTimeStyles.AssumeLocal, out DateTime result)
                        ? result.ToUniversalTime()
                        : DateTime.UtcNow
                : DateTime.UtcNow;
        }
        public virtual string PermanentPriceEffectiveDate
        {
            get => _permanentPriceEffectiveDate.ToLocalTime()
                .ToShortDateString();

        }
        public virtual string PermanentPriceType
        {
            get => _permanentPriceType.ToString();
        }
        public virtual string PermanentQuantityPricingPrice
        {
            get => _permanentQuantityPricingPrice.ToString(
               "F2", CultureInfo.CurrentCulture);
            set =>
                _permanentQuantityPricingPrice =
                !string.IsNullOrEmpty(
                        value)
                        ? value.ToDouble()
                : _permanentQuantityPricingPrice;
        }
        public virtual string PermanentUnitPrice
        {
            get => _permanentUnitPrice.ToString(
               "F2", CultureInfo.CurrentCulture);
            set
            {
                _permanentUnitPrice =
                    !string.IsNullOrEmpty(
                        value)
                        ? value.ToDouble()
                    : _permanentUnitPrice;
                _permanentPriceEffectiveDate =
                    !string.IsNullOrEmpty(value)
                        ? DateTime.UtcNow
                    : _permanentPriceEffectiveDate;
            }
        }
        public virtual string PermanentSavings
        {
            get => _permanentSavings.ToString(
                "F2", CultureInfo.CurrentCulture);
            set
            {
                _permanentSavings =
                    !string.IsNullOrEmpty(
                        value)
                        ? value.ToDouble()
                    : _permanentSavings;
                if (_permanentSavings > 0)
                    _permanentPriceType =
                        PriceType.PermanentSaleDiscount;
            }
        }
        public virtual string QuantityPricingQuantity
        {
            get => _quantityPricingQuantity.ToString(
               "D", CultureInfo.CurrentCulture);
            set =>
                _quantityPricingQuantity =
                !string.IsNullOrEmpty(
                        value)
                        ? int.TryParse(
                            value
                            , NumberStyles
                            .Integer
                            , CultureInfo
                            .CurrentCulture
                            , out int result)
                            ? result
                : _quantityPricingQuantity
            : _quantityPricingQuantity;
        }
        public virtual IMongoService<IItemSellingPrices> Svc => _svc;
        public virtual string TotalSavings =>
            (_permanentSavings + _currentSavings)
                .ToString("F2", CultureInfo.CurrentCulture);
        public object Clone()
        {
            return
                new ItemSellingPrices()
                {

                    _currentPriceEffectiveDate =
                        _currentPriceEffectiveDate,
                    _currentPriceExpirationDate =
                        _currentPriceExpirationDate,
                    _currentPriceType =
                        _currentPriceType,
                    _currentQuantityPricingPrice =
                        _currentQuantityPricingPrice,
                    _currentSavings =
                        _currentSavings,
                    _currentUnitPrice =
                        _currentUnitPrice,
                    _isActive =
                        _isActive,
                    _isTaxIncluded =
                        _isTaxIncluded,
                    _permanentPriceEffectiveDate =
                        _permanentPriceEffectiveDate,
                    _permanentPriceType =
                        _permanentPriceType,
                    _permanentQuantityPricingPrice =
                        _permanentQuantityPricingPrice,
                    _permanentSavings =
                        _permanentSavings,
                    _permanentUnitPrice =
                        _permanentUnitPrice,
                    _quantityPricingQuantity =
                        _quantityPricingQuantity,
                };
        }
        public override bool Equals(object? obj)
        {

            if (obj == null || !(obj is IItemSellingPrices))
                return false;
            IItemSellingPrices match = (IItemSellingPrices)obj;
            if (match.CurrentPriceExpirationDate
                    == CurrentPriceExpirationDate
                        && match.CurrentQuantityPricingPrice
                    == CurrentQuantityPricingPrice
                        && match.CurrentSavings
                    == CurrentSavings
                        && match.CurrentUnitPrice
                    == CurrentUnitPrice
                        && match.IsActive
                    == IsActive
                        && match.IsTaxIncluded
                    == IsTaxIncluded
                        && match.PermanentQuantityPricingPrice
                    == PermanentQuantityPricingPrice
                        && match.PermanentSavings
                    == PermanentSavings
                        && match.PermanentUnitPrice
                    == PermanentUnitPrice
                        && match.QuantityPricingQuantity
                    == QuantityPricingQuantity)
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (CurrentQuantityPricingPrice != null
                    ? CurrentQuantityPricingPrice.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                result = (result * 397) ^ (CurrentSavings != null
                    ? CurrentSavings.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                result = (result * 397) ^ (CurrentUnitPrice != null
                    ? CurrentUnitPrice.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                result = (result * 397) ^ (_isActive.ToString(
                    CultureInfo.CurrentCulture).GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase));
                result = (result * 397) ^ (_isTaxIncluded.ToString(
                    CultureInfo.CurrentCulture).GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase));
                result = (result * 397) ^ (PermanentQuantityPricingPrice != null
                    ? PermanentQuantityPricingPrice.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                result = (result * 397) ^ (PermanentSavings != null
                    ? PermanentSavings.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                result = (result * 397) ^ (PermanentUnitPrice != null
                    ? PermanentUnitPrice.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                result = (result * 397) ^ (QuantityPricingQuantity != null
                    ? QuantityPricingQuantity.GetHashCode(
                    StringComparison.CurrentCultureIgnoreCase) : 0);
                return result;
            }
        }
        public override string ToString()
        {
            return
                "ITEM SELLING PRICES" +
                $"{nameof(Id)}: {Id}" + "\r\n" +
                $"{nameof(Created)}: {Created}" + "\r\n" +
                $"{nameof(Modified)}: {Modified}" + "\r\n" +
                $"{nameof(CurrentUnitPrice)}: {CurrentUnitPrice}" + "\r\n" +
                $"{nameof(PermanentUnitPrice)}: {PermanentUnitPrice}" + "\r\n" +
                $"{nameof(CurrentPriceEffectiveDate)}: {CurrentPriceEffectiveDate}" + "\r\n" +
                $"{nameof(CurrentPriceExpirationDate)}: {CurrentPriceExpirationDate}" + "\r\n" +
                $"{nameof(CurrentPriceType)}: {CurrentPriceType}" + "\r\n" +
                $"{nameof(CurrentQuantityPricingPrice)}: {CurrentQuantityPricingPrice}" + "\r\n" +
                $"{nameof(CurrentSavings)}: {CurrentSavings}" + "\r\n" +
                $"{nameof(HasQuantityPricing)}: {HasQuantityPricing.ToString(CultureInfo.CurrentCulture)}" + "\r\n" +
                $"{nameof(IsActive)}: {IsActive.ToString(CultureInfo.CurrentCulture)}" + "\r\n" +
                $"{nameof(IsPricePromo)}: {IsPricePromo.ToString(CultureInfo.CurrentCulture)}" + "\r\n" +
                $"{nameof(IsTaxIncluded)}: {IsTaxIncluded.ToString(CultureInfo.CurrentCulture)}" + "\r\n" +
                $"{nameof(PermanentPriceEffectiveDate)}: {PermanentPriceEffectiveDate}" + "\r\n" +
                $"{nameof(PermanentPriceType)}: {PermanentPriceType}" + "\r\n" +
                $"{nameof(PermanentQuantityPricingPrice)}: {PermanentQuantityPricingPrice}" + "\r\n" +
                $"{nameof(PermanentSavings)}: {PermanentSavings}" + "\r\n" +
                $"{nameof(QuantityPricingQuantity)}: {QuantityPricingQuantity}" + "\r\n" +
                $"{nameof(TotalSavings)}: {TotalSavings}" + "\r\n" + "\r\n";

        }
        public virtual bool TryValidateDoc(
            out dynamic? result)
        {
            if (_hash == 0)
                _hash = GetHashCode();
            IItemSellingPrices? saved = this.GetDoc<IItemSellingPrices>("_hash", _hash);
            _id =
               saved != null
                    ? !_id.Equals(
                            saved.Id
                            , StringComparison
                            .CurrentCultureIgnoreCase)
                            ? saved.Id
                        : _id
                    : _id;
            result =
                string.IsNullOrEmpty(
                    _id)
                    ? saved ?? this
                : this;
            return
                result != null
                    ? result != this
                        ? false
                    : true
                : false;
        }
    }
}
