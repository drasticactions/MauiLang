// <copyright file="INativeNavigation.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLang;

public interface INativeNavigation
{
    void OpenModal();

    void CloseModal();

    void SetPage(object page);
}
