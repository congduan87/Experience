using Microsoft.AspNetCore.Hosting;
using MyWedding.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Common
{
    public class VariableGlobal
    {
        public static string ContentRoot { get; set; }
        public static long IDWedding { get; set; }
        public static long IDWeddingGuest { get; set; }
    }
}
