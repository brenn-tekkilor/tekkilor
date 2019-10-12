#nullable enable
using Com.DOM;
using MSHTML;
using Retail.Interfaces;
using Com.DOM.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using Com.DOM.Interfaces;
using Scrape.FF.Factories;
using Data.Mongo.Interfaces;
using Data.Mongo.Extensions;

namespace Scrape.FF.Helpers
{
    public class CategoryHelper
    {
        private ITreeTraverser<IHTMLElement>? _tree;
        private readonly IDictionary<IHTMLElement, ICategory?> _elementCategory
            = new Dictionary<IHTMLElement, ICategory?>();
        private const string _categoryRootQuery =
            @"section.CollapsibleFacetContainer--Categories > div.CollapsibleFacetContainer-content > ul.ProductsNavList";
        private static readonly CategoryFactory _factory = ResourceHelper.CatFactory;
        public CategoryHelper() { }
        public ITreeTraverser<IHTMLElement>? Tree => _tree;

        public IDictionary<IHTMLElement, ICategory?> ElementCategory => _elementCategory;
        private void ResetTree()
        {
            _tree = null;
            _tree =
                CreateTree();
        }
        public void Autorun(IHTMLElement? current)
        {
            ResetTree();

            if (_tree != null)
            {
                _tree.AfterTraversalEvent
                    += Tree_AfterTraversalEvent;
                _tree.AfterTraversalEvent +=
                    Tree_AfterTraversalEvent_Autorun;
            }
            if (current == null)
                _tree?.ToFirst();
            else if (_tree != null)
            {
                _tree.Current = current;
                _tree.Current =
                     _tree.PeekPreviousSibling() ?? _tree.PeekParent();
                _tree.ToNext();
            }
        }
        private void Tree_AfterTraversalEvent_Autorun(
            (ITreeTraverser<IHTMLElement> tree
            , IHTMLElement? start
            , string? method) evtArgs)
        {
            evtArgs.tree.ToNext();
        }
        private void Tree_AfterTraversalEvent(
            (ITreeTraverser<IHTMLElement> tree
            , IHTMLElement? start
            , string? method) evtArgs)
        {
            if (evtArgs.tree.Current != null)
            {
                // mark current position
                IHTMLElement current =
                    evtArgs.tree.Current;
                // click element
                current.Click();
                // get a category doc
                ICategory? category =
                    CreateCategory(current);
                // define category element
                category =
                    AddElementDefinition(
                        current, category);
                // get parent element
                IHTMLElement? parent =
                    evtArgs.tree.PeekParent();
                // assign parent and child properties
                category = MakeChild(
                    category
                    , parent);
                // if there are no more children
                // scrape items
                if (!evtArgs.tree
                        .HasCurrentChildren())
                    BrowserHelper.IE().ScrapeList(
                        "div.ProductCard"
                        , new StockItemFactory()
                        , "li.Pagination-next"
                        , (_, item) =>
                           {
                               if (item != null)
                               {
                                   item.Category = category;
                                   item.Save();
                               }
                           });
                // if this is the last child
                // remove definition for parent
                if (evtArgs.tree.IsLastChild()
                    && parent != null)
                    _ = RemoveElementDefinition(parent);
            }
        }
        private ICategory? AddElementDefinition(
            IHTMLElement key
            , ICategory? value)
        {
            if (_elementCategory.ContainsKey(
                    key))
                return
                    _elementCategory[key];
            _elementCategory.Add(
                key, value);
            return value;
        }
        private ICategory? CreateCategory(IHTMLElement? input)
        {
            if (input != null)
            {
                IDictionary<IHTMLElement, ICategory?>? makeResult =
                        _factory.Make(input);

                ICategory? result =
                    makeResult != null
                        ? makeResult.TryGetValue(
                        input
                        , out ICategory? c)
                        ? c ?? null
                    : null
                : null;
                return result;
            }
            return null;
        }
        private ITreeTraverser<IHTMLElement>? CreateTree()
        {
            IHTMLElement? rootElement =
                BrowserHelper.Doc?.Query(
                        _categoryRootQuery)
                ?? null;
            IHTMLDOMNode? rootNode
                = rootElement?.Node();
            return
                rootNode != null
                    ? new TreeTraverser<IHTMLElement>(
                        rootNode
                        , WhatToShow.Elements
                        , node =>
                        {
                            if (node != null)
                            {
                                if (node.Tag() == "LI")
                                {
                                    if (node.ClassList().Contains(
                                        "ProductsNavList-item"))
                                    {
                                        return NodeFilterResult.Accept;
                                    }
                                    else
                                        return NodeFilterResult.Skip;
                                }
                                else
                                    return NodeFilterResult.Skip;
                            }
                            else
                                return NodeFilterResult.Reject;
                        })
                    : null;
        }
        private ICategory? MakeChild(
            ICategory? child
            , IHTMLElement? parent)
        {
            if (child != null)
            {
                child.Parent ??=
                    (parent != null
                            ? ElementCategory.TryGetValue(
                                    parent
                                , out ICategory? p)
                                ? p ?? CreateCategory(parent)
                            : CreateCategory(parent)
                        : null);
                if (child.Parent != null)
                {
                    _ = child.Parent.AddChild(
                        child).Save();
                    _ = child.Save();
                }
            }
            return child;
        }
        private bool RemoveElementDefinition(
            IHTMLElement key)
        {
            if (ElementCategory
                   .ContainsKey(key))
            {
                ElementCategory
                    .Remove(key);
                return true;
            }
            return false;
        }
        public void ResumeLastCategory()
        {

            BrowserHelper.IE().Go(
                MongoHelper
                .GetLast<ICategory>()
                ?.Link);
            Autorun(
                BrowserHelper
                    .IE().Query(
                        "li.ProductsNavList-item.is-selected"));


        }
    }
}
