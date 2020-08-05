﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultarCEP.Servico
{
    public sealed class PreserveAttribute : Attribute
    {
        public bool AllMembers;
        public bool Conditional;
        public PreserveAttribute(bool allMembers, bool conditional)
        {
            AllMembers = allMembers;
            Conditional = conditional;
        }
        public PreserveAttribute()
        {
        }
    }
}
