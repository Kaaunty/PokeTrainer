﻿using Gtk;

namespace NowComesGtk.Utils.WidgetGenerators
{
    public class TextBoxGenerator : Entry
    {
        public TextBoxGenerator(string defaultText, int width, int height) : base()
        {
            Text = defaultText;
            SetSizeRequest(width, height);

            CssProvider cssProvider = new();
            cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);

            FocusInEvent += (sender, e) =>
            {
                Text = "";
                CssProvider cssProvider = new();
                cssProvider.LoadFromData("entry { color: rgb(0, 0, 0); }");
                StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };
            FocusOutEvent += (sender, e) =>
            {

                CssProvider cssProvider = new();
                cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
                StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };
        }
    }
}