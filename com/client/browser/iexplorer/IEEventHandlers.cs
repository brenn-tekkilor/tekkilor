#nullable enable
using System;
namespace com.client.browser.iexplorer
{
    public static class IEEventHandlers
    {
        public static void OnBeforeNavigate2(object Sender, ref object URL,
                                      ref object Flags, ref object Target,
                                      ref object PostData, ref object Headers,
                                      ref bool Cancel)
        {
            Console.WriteLine("Sender:" + Sender);
            Console.WriteLine("URL:" + URL);
            Console.WriteLine("Flags:" + Flags);
            Console.WriteLine("Target:" + Target);
            Console.WriteLine("PostData:" + PostData);
            Console.WriteLine("Headers:" + Headers);
            Console.WriteLine("Cancel:" + Cancel);
        }
        public static void OnNavigateComplete2(object Sender, ref object URL)
        {
            Console.WriteLine("Sender:" + Sender);
            Console.WriteLine("URL:" + URL);
        }
    }
}