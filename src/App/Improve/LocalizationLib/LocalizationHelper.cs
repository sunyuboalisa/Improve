using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace LocalizationLib
{
    class LocalizationHelper : ILocalization
    {
        private bool _initialized;

        /// <summary>
        /// The installed application's language packages.
        /// </summary>
        private Dictionary<CultureInfo, string> _languagePacks;
        public Dictionary<CultureInfo, string> LanguagePacks => _languagePacks;

        /// <summary>
        /// Init, load all application language packages.
        /// </summary>
        /// <param name="languageDir">The directory of language packages.</param>
        public void Init(string languageDir)
        {
            if (!_initialized)
            {
                if (Directory.Exists(languageDir))
                {
                    _languagePacks = _languagePacks ?? new Dictionary<CultureInfo, string>();

                    foreach (var langPath in Directory.EnumerateFiles(languageDir))
                    {
                        var cultureName = Path.GetFileNameWithoutExtension(langPath);
                        var culture = new CultureInfo(cultureName);

                        _languagePacks.Add(culture, langPath);
                    }

                    _initialized = true;
                }
            }
        }

        /// <summary>
        /// Set application culture.
        /// </summary>
        /// <param name="culture"></param>
        public void SetCulture(CultureInfo culture)
        {

        }


        /// <summary>
        /// Get current culture info.
        /// </summary>
        /// <returns></returns>
        public CultureInfo GetCurrentCulture()
        {
            return CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// Add language packages.
        /// </summary>
        /// <returns></returns>
        public bool AddLanguagePack()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove language packages.
        /// </summary>
        /// <returns></returns>
        public bool RemoveLanguagePack()
        {
            throw new NotImplementedException();
        }
    }
}
