﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Steamster.Api.Api.Models
{
    public class ListOfPlayerSummaryData
    {
        public IEnumerable<SteamPlayerSummary> players { get; set; }
    }
}
