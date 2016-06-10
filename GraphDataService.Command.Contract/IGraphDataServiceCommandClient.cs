﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataService.Command.Contract
{
    public interface IGraphDataServiceCommandClient : IGraphDataServiceCommand, IDisposable
    {
    }
}
