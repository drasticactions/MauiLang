using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLang;

public interface IModalNavigation
{
    void OpenSettingsModal();

    void OpenLanguageSelectionModal();

    void CloseModal();
}
