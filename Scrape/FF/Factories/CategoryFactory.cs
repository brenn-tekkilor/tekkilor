#nullable enable
using System.Linq;
using Retail.Interfaces;
using Retail.Common;
using Com.DOM.Extensions;
using Utility.Extensions;
using Retail;
using MSHTML;
using Data.Mongo.Interfaces;
using Utility.Interfaces;
using System.Collections.Generic;
using Data.Mongo.Extensions;

namespace Scrape.FF.Factories
{
    public class CategoryFactory 
        : IMongoFactory
            <IHTMLElement, ICategory>
    {
        private static readonly string _categoryNamePattern =
@"(?:\s*)(?<name>(\s?([A-za-z&\-\,\.\'\(\)`\!\?\*\%\#\@\:\;\/_\+\=])*)*)(?:\s\(\d{1,5}\))";

        public event
            IFactoryHelper
            <IHTMLElement, ICategory>
                .AfterMany? AfterManyEvent;
        public event
            IFactoryHelper
            <IHTMLElement, ICategory>
                .AfterOne? AfterOneEvent;
        public event
            IFactoryHelper
            <IHTMLElement, ICategory>
            .BeforeMany? BeforeManyEvent;
        public event
            IFactoryHelper
            <IHTMLElement, ICategory>
            .BeforeOne? BeforeOneEvent;

        public CategoryFactory() { }
        public IDictionary
            <IHTMLElement, ICategory?>?
                Make(
                    IHTMLElement? input)
        {
            BeforeOneEvent?.Invoke(
                input);
            if (input == null)
                return null;
            string link
                = input.Link();
            string taxonomyId
                = Category
                .ParseTaxonomyId(
                    link);
            ICategory? ParseCategory()
            {
                return
                    input != null
                        ? !string.IsNullOrEmpty(
                            taxonomyId)
                            ? new Category
                            {
                                Link
                                    = link,
                                Name
                                    = ParseName(input),
                                TaxonomyId = taxonomyId,
                            }
                            : null
                        : null;
                    }
            ICategory? output = ParseCategory()
                        ?.Save();
            AfterOneEvent?.Invoke((input, output));
            return
            input != null
                    ? new Dictionary
                        <IHTMLElement, ICategory?>()
                        {
                            { input, output }
                        }
                : null;
        }
        public IDictionary
            <IHTMLElement, ICategory?>?
            Make(
                IEnumerable<IHTMLElement>?
                    input)
        {
            BeforeManyEvent?
                .Invoke(
                    input);
            IDictionary
                <IHTMLElement, ICategory?>?
                output
                    = input != null
                        ? input.Any()
                            ? input.ToList()
                            .ToDictionary(
                                i => i
                                , i => Make(i)?[i])
                            : null
                        : null;
            AfterManyEvent?
                .Invoke(
                    output);
            return output;
        }
        public static string ParseName(
        IHTMLElement? input)
        {
            return
                input?.innerText
                .GetMatchValue(
                     _categoryNamePattern
                    , "name")
                    .Trim()
                ?? string.Empty;
        }
    }
}
