﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bsn.GoldParser.Semantic;

namespace Scrappy.Parser
{
    public class Empty : BaseToken
    {
        [Rule("<nl Opt> ::=")]
        public Empty(BaseToken token)
        {
            
        }
    }
}