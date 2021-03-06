﻿using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maratona.Services.Interfaces
{
    public interface IConfig
    {
        string SQLiteDirectory { get; }
        ISQLitePlatform Platform { get; }
    }
}
