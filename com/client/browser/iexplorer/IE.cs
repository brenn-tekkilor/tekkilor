#nullable enable
using com.client.dom.interfaces;
using MSHTML;
using SHDocVw;
using System;
using System.Collections.Generic;
namespace com.client.browser.iexplorer
{
    public class IE
    {
        private const object EMPTY = null;
        public InternetExplorer Browser { get; set; }
        public HTMLDocument? Doc { get; set; }
        private static IE? Instance { get; set; }
        private IE()
        {
            Browser = new InternetExplorer();
            Wait();
            // override BeforeNavigate2 event
            Browser.BeforeNavigate2 += new
                 SHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(
                IEEventHandlers.OnBeforeNavigate2);
            Browser.NavigateComplete2 += new
                DWebBrowserEvents2_NavigateComplete2EventHandler(
                IEEventHandlers.OnNavigateComplete2);

        }
        public static IE GetInstance()
        {
            if (Instance == null)
                Instance = new IE();
            return Instance;
        }
        public static IHTMLDOMNode? ToNode(object? o)
        {
            IHTMLDOMNode? n = null;
            if (o != null)
            {
                try
                {
                    n = (IHTMLDOMNode)o;
                }
                catch (InvalidCastException e)
                {
                    Console.Write(e.Message);
                }
            }
            return n;
        }
        public static IHTMLElement6? ToElement(object? o)
        {
            IHTMLElement6? m = null;
            if (o != null)
            {
                try
                {
                    m = (IHTMLElement6)o;
                }
                catch (InvalidCastException e)
                {
                    Console.Write(e.Message);
                }
            }
            return m;
        }

        public void GoTo(string u)
        {
            Browser.Navigate2(u, EMPTY, EMPTY, EMPTY, EMPTY);
            Wait();
            Doc = (HTMLDocument)Browser.Document;
        }
        public void GoTo(Uri u)
        {
            if (u != null)
                GoTo(u.AbsoluteUri);
        }
        public void Browse(string u, List<string?>? l)
        {
            GoTo(u);
            if (l != null)
                ClickSelectorSequence(l);
        }
        public void Browse(Uri u, List<string?>? l)
        {
            if (u != null)
                Browse(u.AbsoluteUri, l);
        }
        public static void Sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }
        public void Wait()
        {
            do
            {
                Sleep(100);
            }
            while (Browser.Busy);
            Sleep(2000);
        }
        public void Quit()
        {
            Browser.Quit();
        }
        public IHTMLElement? QuerySelector(string v)
        {
            return Doc != null ? (IHTMLElement?)
                Doc.querySelector(v) : null;
        }
        public IHTMLDOMChildrenCollection? QuerySelectorAll(string v)
        {
            return Doc != null ? (IHTMLDOMChildrenCollection?)
                Doc.querySelectorAll(v) : null;
        }
        public bool Click(IHTMLElement? e)
        {
            if (e != null)
            {
                if (!(e.onclick is DBNull))
                {
                    Console.WriteLine(e.tagName);
                    Console.WriteLine(e.className);
                    Console.WriteLine(e.innerText);
                    Console.WriteLine(e.innerHTML);
                    e.click();
                    Wait();
                    Doc = (HTMLDocument)Browser.Document;
                    return true;
                }
                else if (((IHTMLDOMNode)e).hasChildNodes())
                    return Click((IHTMLElement)((IHTMLDOMNode)e).firstChild);
                else
                    return false;
            }
            else
                throw new ArgumentNullException(nameof(e));
        }
        public bool ClickSelector(string? v)
        {
            IHTMLElement? e = v != null
                ? QuerySelector(v)
                : throw new ArgumentNullException(nameof(v));
            return e != null ? Click(e) : false;
        }
        public void ClickSelectorSequence(List<string?>? l)
        {
            if (l != null)
            {
                foreach (string? v in l)
                {
                    if (v != null)
                    {
                        IHTMLElement? m = QuerySelector(v);
                        if (m != null)
                        {
                            Click(m);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
