#nullable enable

using Com.DOM;
using Com.DOM.Interfaces;
using MSHTML;
using Retail.Interfaces;
using Com.DOM.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using Scrape.FF.Factories;
using Data.Mongo.Interfaces;
using Data.Mongo.Extensions;
using Data.Mongo;

namespace Scrape.FF.Helpers
{
    public class CategoryHelper
    {
        private readonly IDictionary<IHTMLElement, ICategory?> _elementCategory
            = new Dictionary<IHTMLElement, ICategory?>();

        private const string _categoryRootQuery =
            @"section.CollapsibleFacetContainer--Categories > div.CollapsibleFacetContainer-content > ul.ProductsNavList";

        private static readonly CategoryFactory _factory = new CategoryFactory();
        private static readonly IMongoService<ICategory> _categories = new MongoService<ICategory>();

        public CategoryHelper()
        {
        }

        public static IMongoService<ICategory> Categories => _categories;
        public static CategoryFactory Factory => _factory;
        public ITreeTraverser<IHTMLElement>? Tree { get; private set; }

        private void ResetTree()
        {
            Tree = null;
            Tree =
                CreateTree();
        }

        public void Autorun(IHTMLElement? current)
        {
            ResetTree();

            if (Tree != null)
            {
                Tree.AfterTraversalEvent
                    += Tree_AfterTraversalEvent;
                Tree.AfterTraversalEvent +=
                    Tree_AfterTraversalEvent_Autorun;
                Tree.BeforeTraversalEvent += _tree_BeforeTraversalEvent;
            }
            if (_factory != null)
            {
                _factory.AfterOneEvent += _factory_AfterOneEvent;
                _factory.BeforeOneEvent += _factory_BeforeOneEvent;
            }
            if (current == null)
                Tree?.ToFirst();
            else if (Tree != null)
            {
                Tree.Current = current;
                Tree.Current =
                     Tree.PeekPreviousSibling() ?? Tree.PeekParent();
                Tree.ToNext();
            }
        }

        private void _tree_BeforeTraversalEvent((ITreeTraverser<IHTMLElement>, IHTMLElement?, string?) evtArgs)
        {
            throw new NotImplementedException();
        }

        private void _factory_BeforeOneEvent(IHTMLElement? evtArgs)
        {
        }

        private void _factory_AfterOneEvent((IHTMLElement? input, ICategory? output) evtArgs)
        {
            if (evtArgs.output != null
                && evtArgs.input != null)
            {
                ICategory parent =
                    _categories.GetOne(
                        "_taxonomyId"
                        , evtArgs.output
                        .ParseTaxonomyId(
                            Tree.PeekNextSibling
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
                        .HasCurrentChildren)
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
                if (evtArgs.tree.IsLastChild                    && parent != null)
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
                            ? _elementCategory.TryGetValue(
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
            if (_elementCategory
                   .ContainsKey(key))
            {
                _elementCategory
                    .Remove(key);
                return true;
            }
            return false;
        }

        public void ResumeLastCategory()
        {
            BrowserHelper.IE().Go(
                _categories
                .GetLast()
                ?.Link);
            Autorun(
                BrowserHelper
                    .IE().Query(
                        "li.ProductsNavList-item.is-selected"));
        }
    }
}