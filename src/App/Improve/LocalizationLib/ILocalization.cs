using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LocalizationLib
{
    interface ILocalization
    {
        Dictionary<CultureInfo, string> LanguagePacks { get; }

        CultureInfo GetCurrentCulture();

        void SetCulture(CultureInfo culture);

        bool AddLanguagePack();

        bool RemoveLanguagePack();
    }
}
